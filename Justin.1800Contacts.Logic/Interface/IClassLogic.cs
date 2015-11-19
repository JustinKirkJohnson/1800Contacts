using System.Collections.Generic;

namespace Justin._1800Contacts.Logic.Interface
{
    public interface IClassLogic
    {

		IEnumerable<string> SortByPrerequisite(IList<string> classDependencyList);

    }
}
