using System.Collections.Generic;
using System.Linq;
using Justin._1800Contacts.Logic.Class;
using Justin._1800Contacts.Logic.Sorting;
using Xunit;

namespace Justin._1800Contacts.Logic.Tests
{

    public class ClassLogicTests
    {

        [Fact]
		public void SortByPrerequisite_SingleClassNoDependencySpecified_CanSortByPrerequisite()
        {
            
			// Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1"
            };

            // Act
            List<string> sortedArray = logic.SortByPrerequisite(inputStringArray).ToList();

            // Assert
            Assert.Equal(1, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);

        }

        [Fact]
        public void SortByPrerequisite_SingleClassEmptyDependencySpecified_CanSortByPrerequisite()
        {
            
			// Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1:	 "
            };

            // Act
			List<string> sortedArray = logic.SortByPrerequisite(inputStringArray).ToList();

            // Assert
            Assert.Equal(1, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);

        }

        [Fact]
        public void SortByPrerequisite_TwoClassesNoDependenciesSpecified_CanSortByPrerequisite()
        {
            
			// Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1", @"TestClass2"
            };

            // Act
			List<string> sortedArray = logic.SortByPrerequisite(inputStringArray).ToList();

            // Assert
            Assert.Equal(2, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);
            Assert.Equal("TestClass2", sortedArray[1]);

        }

		[Fact]
		public void SortByPrerequisite_TwoClassesSingleDependency_CanSortByPrerequisite()
		{
			
			// Arrange
			ClassLogic logic = new ClassLogic();
			List<string> inputStringArray = new List<string>
            {
                @"TestClass2:TestClass1", @"TestClass1"
            };

			// Act
			List<string> sortedArray = logic.SortByPrerequisite(inputStringArray).ToList();

			// Assert
			Assert.Equal(2, sortedArray.Count);
			Assert.Equal("TestClass1", sortedArray[0]);
			Assert.Equal("TestClass2", sortedArray[1]);

		}

		[Fact]
		public void SortByPrerequisite_InvalidDependency_ThrowsException()
		{

			// Arrange
			ClassLogic logic = new ClassLogic();
			List<string> inputStringArray = new List<string>
            {
                @"TestClass2:TestClass3", @"TestClass1"
            };

			// Act and Assert
			DependencyException<ClassItem> exception = Assert.Throws<DependencyException<ClassItem>>(() => logic.SortByPrerequisite(inputStringArray));
			Assert.Equal(1, exception.AffectedItems.Count);
			Assert.Equal("TestClass3", exception.AffectedItems[0].Name);

		}

		[Fact]
		public void SortByPrerequisite_EmptyLine_CanSortByPrerequisite()
		{

			// Arrange
			ClassLogic logic = new ClassLogic();
			List<string> inputStringArray = new List<string>
            {
                @"TestClass2:TestClass1", "		 ", @"TestClass1"
            };

			// Act
			List<string> sortedArray = logic.SortByPrerequisite(inputStringArray).ToList();

			// Assert
			Assert.Equal(2, sortedArray.Count);
			Assert.Equal("TestClass1", sortedArray[0]);
			Assert.Equal("TestClass2", sortedArray[1]);

		}

	}

}
