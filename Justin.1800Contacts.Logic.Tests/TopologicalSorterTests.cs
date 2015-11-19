using System.Collections.Generic;
using Justin._1800Contacts.Logic.Sorting;
using Xunit;

namespace Justin._1800Contacts.Logic.Tests
{
	public class TopologicalSorterTests
	{

		[Fact]
		public void Sort_TwoElementsOneDependencyDeep_CanSortByDependency()
		{

			// Arrange
			SortItem a = new SortItem("A");
			SortItem c = new SortItem("C");

			c.SetDependencies(a);

			SortItem[] unsorted = new[] { c, a };

			// Act
			TopologicalSorter topologicalSorter = new TopologicalSorter();
			IList<SortItem> sorted = topologicalSorter.Sort(unsorted, x => x.Dependencies, new SortItemEqualityComparer());

			// Assert
			Assert.Equal(2, sorted.Count);
			Assert.Equal("A", sorted[0].Name);
			Assert.Equal("C", sorted[1].Name);

		}

		[Fact]
		public void Sort_MultipleElementsOneDependencyDeep_CanSortByDependency()
		{

			// Arrange
			var a = new SortItem("A");
			var c = new SortItem("C");
			var f = new SortItem("F");
			var h = new SortItem("H");
			var d = new SortItem("D");
			var g = new SortItem("G");
			var e = new SortItem("E");
			var b = new SortItem("B");

			d.SetDependencies(a);
			g.SetDependencies(f);
			e.SetDependencies(d);
			b.SetDependencies(c);

			var unsorted = new[] { a, b, c, d, e, f, g, h };

			// Act
			TopologicalSorter topologicalSorter = new TopologicalSorter();
			var sorted = topologicalSorter.Sort(unsorted, x => x.Dependencies, new SortItemEqualityComparer());

			// Assert
			Assert.Equal(8, sorted.Count);
			Assert.Equal("A", sorted[0].Name);
			Assert.Equal("C", sorted[1].Name);
			Assert.Equal("B", sorted[2].Name);
			Assert.Equal("D", sorted[3].Name);
			Assert.Equal("E", sorted[4].Name);
			Assert.Equal("F", sorted[5].Name);
			Assert.Equal("G", sorted[6].Name);
			Assert.Equal("H", sorted[7].Name);

		}

		[Fact]
		public void Sort_MultipleElementsTwoDependencyDeep_CanSortByDependency()
		{

			// Arrange
			var a = new SortItem("A");
			var c = new SortItem("C");
			var f = new SortItem("F");
			var h = new SortItem("H");
			var d = new SortItem("D");
			var g = new SortItem("G");
			var e = new SortItem("E");
			var b = new SortItem("B");

			d.SetDependencies(a);
			g.SetDependencies(f, h);
			e.SetDependencies(d, g);
			b.SetDependencies(c, e);

			var unsorted = new[] { a, b, c, d, e, f, g, h };

			// Act
			TopologicalSorter topologicalSorter = new TopologicalSorter();
			var sorted = topologicalSorter.Sort(unsorted, x => x.Dependencies, new SortItemEqualityComparer());

			// Assert
			Assert.Equal(8, sorted.Count);
			Assert.Equal("A", sorted[0].Name);
			Assert.Equal("C", sorted[1].Name);
			Assert.Equal("D", sorted[2].Name);
			Assert.Equal("F", sorted[3].Name);
			Assert.Equal("H", sorted[4].Name);
			Assert.Equal("G", sorted[5].Name);
			Assert.Equal("E", sorted[6].Name);
			Assert.Equal("B", sorted[7].Name);

		}

		[Fact]
		public void Sort_FirstOrderCircularDependency_ThrowsSortException()
		{

			// Arrange
			var a = new SortItem("A");
			var c = new SortItem("C");

			a.SetDependencies(c);
			c.SetDependencies(a);

			var unsorted = new[] { a, c };

			// Act and Assert
			TopologicalSorter topologicalSorter = new TopologicalSorter();
			CircularDependencyException<SortItem> exception = Assert.Throws<CircularDependencyException<SortItem>>(
				() => topologicalSorter.Sort(unsorted, x => x.Dependencies, new SortItemEqualityComparer()));

			Assert.Equal(1, exception.AffectedItems.Count);
			Assert.Equal("A", exception.AffectedItems[0].Name);

		}

		[Fact]
		public void Sort_SecondOrderCircularDependency_ThrowsSortException()
		{

			// Arrange
			var a = new SortItem("A");
			var b = new SortItem("B");
			var c = new SortItem("C");

			a.SetDependencies(c);
			b.SetDependencies(a);
			c.SetDependencies(b);

			var unsorted = new[] { a, b, c };

			// Act and Assert
			TopologicalSorter topologicalSorter = new TopologicalSorter();
			CircularDependencyException<SortItem> exception = Assert.Throws<CircularDependencyException<SortItem>>(
				() => topologicalSorter.Sort(unsorted, x => x.Dependencies, new SortItemEqualityComparer()));

			Assert.Equal(1, exception.AffectedItems.Count);
			Assert.Equal("A", exception.AffectedItems[0].Name);

		}

	}
}
