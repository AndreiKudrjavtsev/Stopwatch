using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStopwatch;

namespace StopwatchTest
{
    [TestClass]
    public class LapsTest
    {
        [TestMethod]
        public void InitializationTest()
        {
            // Arrange & Act
            ViewModel vm = new ViewModel();
            // Assert
            Assert.AreEqual(0, vm.Laps.Count);
        }

        [TestMethod]
        public void LapAdditionTest()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act
            vm.AddLap.Execute("00:00:01.000");
            // Assert
            Assert.AreEqual("1. 00:00:01.000", vm.Laps[0].LapEntry);
        }

        [TestMethod]
        public void AdditionAvailabilityTest()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act
            vm.AddLap.Execute("00:00:01.000");
            // Assert
            Assert.IsFalse(vm.AddLap.CanExecute("00:00:01.000"));
        }

        [TestMethod]
        public void AdditionAvailabilityTest2()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act

            // Assert
            Assert.IsTrue(vm.AddLap.CanExecute("00:00:01.000"));
        }

        [TestMethod]
        public void StopWatchStartTest()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act
            vm.StartCommand.Execute(true);
            // Assert
            Assert.IsTrue(vm.StartPauseButtonText == "Pause");
        }

        [TestMethod]
        public void StopWatchPauseTest()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act
            vm.StartCommand.Execute(true);
            vm.StartCommand.Execute(true);
            // Assert
            Assert.IsTrue(vm.StartPauseButtonText == "Resume");
        }

        [TestMethod]
        public void ResetTest()
        {
            // Arrange
            ViewModel vm = new ViewModel();
            // Act
            vm.AddLap.Execute("00:00:01.000");
            vm.AddLap.Execute("00:00:02.000");
            vm.ResetCommand.Execute(true);
            // Assert
            Assert.IsTrue(vm.Laps.Count == 0);
        }
    }
}
