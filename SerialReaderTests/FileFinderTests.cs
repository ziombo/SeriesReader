using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using SerialReader;

namespace SerialReaderTests
{
    [TestFixture]
    public class FileFinderTests
    {
        #region SelectDirectory Tests

        [Test]
        public void SelectDirectoryReturnProperPath()
        {
            // Arrange
            FileFinder sut = new FileFinder();

            var mock = new Mock<IFolderBrowserDialogWrapper>();
            mock.Setup(fbdw => fbdw.GetPathToDirectory()).Returns(@"C:\Program Files\AMD");

            // Act
            string result = sut.SelectDirectory(mock.Object);

            // Assert
            Assert.AreEqual(@"C:\Program Files\AMD", result);
        }

        [Test]
        public void SelectDirectoryThrowsExceptionPathIsEmpty()
        {
            // Arrange
            FileFinder sut = new FileFinder();

            var mock = new Mock<IFolderBrowserDialogWrapper>();
            mock.Setup(fbdw => fbdw.GetPathToDirectory()).Returns(String.Empty);

            // Act/Asssert
            Assert.Throws<InvalidOperationException>(() => sut.SelectDirectory(mock.Object));
        }

        [Test]
        public void SelectDirectoryThrowsExceptionPathIsNotPath()
        {
            // Arrange
            FileFinder sut = new FileFinder();

            var mock = new Mock<IFolderBrowserDialogWrapper>();
            mock.Setup(fbdw => fbdw.GetPathToDirectory()).Returns("not a path");

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => sut.SelectDirectory(mock.Object));
        }

        #endregion

        #region GetFileNames Tests

        [Test]
        public void GetFileNamesShouldReturn3FileNames()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();

            var mock = new Mock<IGetFilesFromDir>();
            mock.Setup(gffd => gffd.GetFiles()).Returns(new List<string> { "1", "2", "3"});

            // Act
            List<string> result = fileFinder.GetFilenames(mock.Object);

            // Assert
            Assert.AreEqual(new List<string> { "1", "2", "3" }, result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void GetFileNamesShouldReturn0FileNames()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();

            var mock = new Mock<IGetFilesFromDir>();
            mock.Setup(gffd => gffd.GetFiles()).Returns(new List<string> { });

            // Act
            var result = fileFinder.GetFilenames(mock.Object);

            // Assert
            Assert.IsEmpty(result);
        }

        #endregion

        #region GetMovieFilesFromAllFiles

        [Test]
        public void ShouldReturnAllFilesFromProvidedList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "test.avi" , "test2.mp4", "test3.mkv" };

            // Act
            var result = fileFinder.GetMovieFilesFromAllFiles(fileNames);

            // Assert
            Assert.AreEqual(fileNames, result);
        }


        #endregion
    }
}
