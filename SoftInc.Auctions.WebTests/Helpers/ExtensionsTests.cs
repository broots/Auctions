using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftInc.Auctions.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftInc.Auctions.Web.Helpers.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void ImgToBase64StringTest()
        {
            var fs = File.OpenRead(@"C:\Users\jbro\Documents\P\toyotaC_int.jpg");
            var str = fs.ImgToBase64String();
            Assert.IsTrue(!string.IsNullOrEmpty(str));
        }

        [TestMethod()]
        public void ImgToThumbBase64StringTest()
        {
            var fs = File.OpenRead(@"C:\Users\jbro\Documents\P\toyotaC_int.jpg");
            var str = fs.ImgToThumbBase64String();
            Assert.IsTrue(!string.IsNullOrEmpty(str));
        }
    }
}