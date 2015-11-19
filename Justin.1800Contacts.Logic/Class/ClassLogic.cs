using System.Collections.Generic;
using System.Linq;
using Justin._1800Contacts.Logic.Interface;
using Justin._1800Contacts.Logic.Sorting;

namespace Justin._1800Contacts.Logic.Class
{
    public class ClassLogic : IClassLogic
    {

	    private readonly ISorter _sorter;

	    public ClassLogic() : this(new TopologicalSorter()) { }

	    public ClassLogic(ISorter sorter)
	    {
		    _sorter = sorter;
	    }

        public IEnumerable<string> SortByPrerequisite(IList<string> classDependencyList)
        {

			List<ClassItem> primaryClassItems = new List<ClassItem>();
			List<ClassItem> dependentClassItems = new List<ClassItem>();

			Dictionary<string, ClassItem> uniqueClassItemsDictionary =
				new Dictionary<string, ClassItem>();

			// Parse the list that was provided into ClassItem's
	        foreach (string classDependencyMapping in classDependencyList)
	        {
		        if (string.IsNullOrEmpty(classDependencyMapping) ||
					string.IsNullOrWhiteSpace(classDependencyMapping))
		        {
			        continue;
		        }

		        string[] classDependencyMappingComponents =
			        classDependencyMapping.Split(':');

				ClassItem classItem = ResolveClassItem(classDependencyMappingComponents[0], uniqueClassItemsDictionary);
				primaryClassItems.Add(classItem);

		        if (classDependencyMappingComponents.Count() > 1 &&
					!string.IsNullOrEmpty(classDependencyMappingComponents[1]) &&
					!string.IsNullOrWhiteSpace(classDependencyMappingComponents[1]))
		        {
					ClassItem dependentClassItem = ResolveClassItem(classDependencyMappingComponents[1], uniqueClassItemsDictionary);
					dependentClassItems.Add(dependentClassItem);
					classItem.SetDependencies(dependentClassItem);
		        }
	        }

			// Validate that no prerequisites were specified that weren't provided as base classes
			IEnumerable<ClassItem> orphanedClassItems = dependentClassItems.Except(primaryClassItems, new SortItemEqualityComparer<ClassItem>()).ToList();
	        if (orphanedClassItems.Any())
	        {
				throw new DependencyException<ClassItem>(string.Format("Invalid class prerequisites: {0}", 
					string.Join(", ", orphanedClassItems.Select(item => item.Name))), orphanedClassItems.ToArray());
	        }

			// Sort ClassItem's based on the dependencies provided
			return _sorter.Sort(uniqueClassItemsDictionary.Values, item => item.Dependencies).Select(item => item.Name);

        }

		private ClassItem ResolveClassItem(string classTitle, Dictionary<string, ClassItem> uniqueSortItemsDictionary)
	    {
			ClassItem sortItem;
			string targetClassTitle = classTitle.Trim();
			if (uniqueSortItemsDictionary.ContainsKey(targetClassTitle))
			{
				sortItem = uniqueSortItemsDictionary[targetClassTitle];
			}
			else
			{
				sortItem = new ClassItem(targetClassTitle);
				uniqueSortItemsDictionary[targetClassTitle] = sortItem;
			}

			return sortItem;
	    }

    }
}
