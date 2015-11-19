using System;
using System.Collections.Generic;

namespace Justin._1800Contacts.Logic.Sorting
{

	public class SortingException : Exception
	{
		public SortingException(string message) : base(message) { }
	}

	public class DependencyException<T> : SortingException
	{
		public IList<T> AffectedItems { get; private set; }

		public DependencyException(string message, params T[] affectedItems) : base(message)
		{
			AffectedItems = new List<T>(affectedItems);
		}
	}

	public class CircularDependencyException<T> : DependencyException<T>
	{
		public CircularDependencyException(string message, params T[] affectedItems) : base(message, affectedItems) { }
	}

}
