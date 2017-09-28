using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSearch;
using NameSearch.Controllers;

namespace NameSearch.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController hc = new HomeController();

            // Act
            ViewResult rlt = hc.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(rlt);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController hc = new HomeController();

            // Act
            ViewResult rlt = hc.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", rlt.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController hc = new HomeController();

            // Act
            ViewResult rlt = hc.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(rlt);
        }
    }
}
