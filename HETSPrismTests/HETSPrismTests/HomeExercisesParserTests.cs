using DataBuilders;
using HETSPrism.Services;
using System;
using Xunit;
using System.IO;
namespace HETSPrismTests
{
    public class HomeExercisesParserTests
      { 
        [Fact]

        public void TraverseTreeArgumentExceptionTest()
        {
            // Arrange
            var parser = new HomeExercisesParser("Folder");

            // Asert and Act
            Assert.Throws<ArgumentException>(() => parser.TraverseTree());
 
        }

        [Fact]
        public void TraverseTreeSuccessfullImportTest()
        {
            // Arrange
            var parser = new HomeExercisesParser(@"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\495398");
            int expected = 1;
            // Act
            parser.TraverseTree();

            //Assert
            Assert.Equal(expected, parser.HomeExercises.Count);
        }

    }
}



