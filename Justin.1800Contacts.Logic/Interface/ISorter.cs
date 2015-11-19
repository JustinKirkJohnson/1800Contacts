using System;
using System.Collections.Generic;

namespace Justin._1800Contacts.Logic.Interface
{
	public interface ISorter
	{

		IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null);

	}
}
