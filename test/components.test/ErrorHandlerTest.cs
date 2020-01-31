using System;
using System.IO;
using Components.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Components.Test
{
    [TestCategory("GithubAction")]
    [TestClass]
    public class ErrorHandlerTest
    {
        [TestMethod]
        public void RegisterErrorAsText()
        {
            var logger = new ErrorLogger(false);
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception error)
            {
                logger.LogEvent(error);
            }
            var text = File.ReadAllText($"logs/error/error.txt");
            Assert.IsTrue(text.Contains("The method or operation is not implemented."));
        }

        [TestMethod]
        public void RegisterErrorAsJson()
        {
            var logger = new ErrorLogger(true);
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception error)
            {
                logger.LogEvent(error);
            }
            var text = File.ReadAllText($"logs/error/error.txt");
            Assert.IsTrue(text.Contains("\"E_Message\":\"The method or operation is not implemented.\""));
        }
    }
}