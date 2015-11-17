using System;
using System.Collections.Generic;
using Justin._1800Contacts.Api.Logic;
using Xunit;

namespace Justin._1800Contacts.Api.Test
{

    public class ClassLogicTests
    {
        [Fact]
        public void ClassLogic_SingleClassNoDependencySpecified_CanSortByPrerequisite()
        {
            // Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1"
            };

            // Act
            List<string> sortedArray = logic.SortByPrerequisite(inputStringArray);

            // Assert
            Assert.Equal(1, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);
        }

        [Fact]
        public void ClassLogic_SingleClassEmptyDependencySpecified_CanSortByPrerequisite()
        {
            // Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1:"
            };

            // Act
            List<string> sortedArray = logic.SortByPrerequisite(inputStringArray);

            // Assert
            Assert.Equal(1, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);
        }

        [Fact]
        public void ClassLogic_TwoClassesNoDependenciesSpecified_CanSortByPrerequisite()
        {
            // Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass1", @"TestClass2"
            };

            // Act
            List<string> sortedArray = logic.SortByPrerequisite(inputStringArray);

            // Assert
            Assert.Equal(2, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);
            Assert.Equal("TestClass2", sortedArray[1]);
        }

        [Fact]
        public void ClassLogic_TwoClassesSingleDependency_CanSortByPrerequisite()
        {
            // Arrange
            ClassLogic logic = new ClassLogic();
            List<string> inputStringArray = new List<string>
            {
                @"TestClass2:TestClass1", @"TestClass1"
            };

            // Act
            List<string> sortedArray = logic.SortByPrerequisite(inputStringArray);

            // Assert
            Assert.Equal(2, sortedArray.Count);
            Assert.Equal("TestClass1", sortedArray[0]);
            Assert.Equal("TestClass2", sortedArray[1]);
        }

    }

}
