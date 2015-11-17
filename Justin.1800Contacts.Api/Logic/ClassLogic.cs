using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justin._1800Contacts.Api.Interface;

namespace Justin._1800Contacts.Api.Logic
{
    public class ClassLogic : IClassLogic
    {

        public List<string> SortByPrerequisite(List<string> classDependencyList)
        {
            List<string> sortedClassDependencyList = new List<string>();
            Dictionary<string, string> classDependencyMappingDictionary = 
                new Dictionary<string, string>();

            foreach (string classDependencyMapping in classDependencyList)
            {
                string[] classDependencyMappingComponents = 
                    classDependencyMapping.Split(':');

                string targetClass = classDependencyMappingComponents[0].Trim();
                string prerequisiteClass = null;
                if (classDependencyMappingComponents.Count() > 1)
                {
                    prerequisiteClass = classDependencyMappingComponents[1].Trim();
                }

                classDependencyMappingDictionary.Add(targetClass, prerequisiteClass);
            }

            // TODO: validate that all indicated dependencies have been specified as target classes

            foreach (string targetClass in classDependencyMappingDictionary.Keys)
            {
                // TODO: pull into recursive method
                string prerequisiteClass = classDependencyMappingDictionary[targetClass];
                if (prerequisiteClass == null)
                {
                    sortedClassDependencyList.Insert(0, targetClass);
                    continue;
                }

                int prerequisiteIndex = 
                    sortedClassDependencyList.FindIndex(
                        classItem => classItem == prerequisiteClass);

                if (prerequisiteIndex == -1)
                {
                    sortedClassDependencyList.Add(targetClass);
                }
                else
                {
                    sortedClassDependencyList.Insert(prerequisiteIndex, targetClass);
                }

            }

            return sortedClassDependencyList;
        }

    }
}
