using System.Collections.Generic;

namespace Justin._1800Contacts.Logic.Sorting
{

	public class SortItemEqualityComparer : SortItemEqualityComparer<SortItem> { }

	public class SortItemEqualityComparer<T> : EqualityComparer<T> where T : SortItem<T>
	{
		public override bool Equals(T x, T y)
		{
			return (x == null && y == null) || (x != null && y != null && x.Name == y.Name);
		}

		public override int GetHashCode(T obj)
		{
			return obj == null ? 0 : obj.Name.GetHashCode();
		}
	}
}
