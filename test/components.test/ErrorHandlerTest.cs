using System;
using System.IO;
using Components.Errorhandler;
using Components.Errorhandler.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace components.test
{
    // Each file or folder that is created can be
    // found in the bin/debug folder under home.
    [TestClass]
    public class ErrorHandlerTest
    {
        private IErrorLogger logger { get; }

        public ErrorHandlerTest()
            => logger = new ErrorLogger();

        [TestMethod]
        public void RegisterError()
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            logger.SetWorkingDirectory($"{workingDirectory}/test_folder/");
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception error)
            {
                 logger.LogError(error);
            }
            var text = File.ReadAllText($"{workingDirectory}/test_folder/logs/error/error.txt");
            Assert.IsTrue(text.Contains("The method or operation is not implemented."));
        }
    }
}