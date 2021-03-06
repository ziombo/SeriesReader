﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using SerialReaderLibrary;
using SerialReaderLibrary.Utils.Files;
using SerialReaderLibrary.Utils.Files.Interfaces;

namespace SerialReaderTests
{
    [TestFixture]
    public class FileFinderTests
    {
        #region SelectDirectory Tests

        [Test]
        [Category("SelectDirectory")]
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
        [Category("SelectDirectory")]
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
        [Category("SelectDirectory")]
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
        [Category("GetFileNames")]
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
        [Category("GetFileNames")]
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

        #region GetMovieFilesFromAllFiles Tests

        [Test]
        [Category("GetMovieFilesFromAllFiles")]
        public void ShouldReturnAllFilesFromProvidedList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "test.avi" , "test2.mp4", "test3.mkv" };

            // Act
            List<string> result = fileFinder.GetMovieFilesFromAllFiles(fileNames);

            // Assert
            Assert.AreEqual(fileNames, result);
        }

        [Test]
        [Category("GetMovieFilesFromAllFiles")]
        public void ShouldReturnNoFilesFromProvidedList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "qwerty", "asdfg.mp3", "foobar.jfcmsb" };

            // Act
            List<string> result = fileFinder.GetMovieFilesFromAllFiles(fileNames);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        [Category("GetMovieFilesFromAllFiles")]
        public void ShouldReturnOneFileFromProvidedList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "tetris", "mario.mkv", "at.avi.ena" };

            // Act
            List<string> result = fileFinder.GetMovieFilesFromAllFiles(fileNames);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.Contains("mario.mkv", result);
        }
        #endregion tests

        #region ExtractSeriesFromFileNames Tests

        [Test]
        [Category("ExtractSeriesFromFileNames")]
        public void ShouldReturnOneElementInList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "mamba.S02", "bamba" };

            // Act
            List<string> result = fileFinder.ExtractSeriesFromFileNames(fileNames);

            // Assert
            Assert.That(result.Contains("mamba"));
            Assert.That(!result.Contains("bamba"));
        }

        [Test]
        [Category("ExtractSeriesFromFileNames")]
        public void ShouldReturnEmptyList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string> { "testS02", "S02", ".S02" };

            // Act
            List<string> result = fileFinder.ExtractSeriesFromFileNames(fileNames);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        [Category("ExtractSeriesFromFileNames")]
        public void ShouldReturnEmptyListFromEmptyList()
        {
            // Assign
            FileFinder fileFinder = new FileFinder();
            List<string> fileNames = new List<string>();

            // Act
            List<string> result = fileFinder.ExtractSeriesFromFileNames(fileNames);

            // Assert
            Assert.IsEmpty(result);
        }

        #endregion
    }
}
