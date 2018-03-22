using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicShapefileService.EmailClient.Models;
using System;

namespace PublicShapefileService.Tests
{
    [TestClass]
    public class EmailClientTests
    {
        //FAIL
        [TestMethod]
        public void ReadInfo()
        {
            BaseMail baseMail = new BaseMail();
            
            Assert.IsNotNull(baseMail._Host);
            Assert.IsNotNull(baseMail._Operators);
            Assert.IsNotNull(baseMail._Password);
            Assert.IsNotNull(baseMail._Port);
            Assert.IsNotNull(baseMail._SmtpClient);
            Assert.IsNotNull(baseMail._UserName);
        }


    }
}
