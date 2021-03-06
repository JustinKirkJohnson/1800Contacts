﻿using System;
using System.Collections.Generic;
using Justin._1800Contacts.Logic.Interface;

namespace Justin._1800Contacts.Logic.Sorting
{
	public class TopologicalSorter : ISorter
	{

		public IList<T> Sort<T>(IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null)
		{
			var sorted = new List<T>();
			var visited = (comparer != null) ? new Dictionary<T, bool>(comparer) : new Dictionary<T, bool>();

			foreach (var item in source)
			{
				Visit(item, getDependencies, sorted, visited);
			}

			return sorted;
		}

		public void Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
		{

			bool inProcess;
			var alreadyVisited = visited.TryGetValue(item, out inProcess);

			if (alreadyVisited)
			{
				if (inProcess)
				{
					throw new CircularDependencyException<T>(string.Format("Circular dependency found with '{0}'", item), item);
				}
			}
			else
			{
				visited[item] = true;

				var dependencies = getDependencies(item);
				if (dependencies != null)
				{
					foreach (var dependency in dependencies)
					{
						Visit(dependency, getDependencies, sorted, visited);
					}
				}

				visited[item] = false;
				sorted.Add(item);
			}
		}

	}
}
