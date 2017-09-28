using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSearch;
using NameSearch.Controllers;
using NameSearch.Models;
using NameSearch.ViewModels;

namespace NameSearch.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            Global.SetDataDirectory();

            EmployeeController ec = new EmployeeController();
            ViewResult rlt = ec.Index() as ViewResult;
            Assert.IsNotNull(rlt);
        }

        [TestMethod]
        public void FilterBy()
        {
            Global.SetDataDirectory();

            string name = "";
            EmployeeController ec = new EmployeeController();
            PartialViewResult rlt = ec.FilterBy(name) as PartialViewResult;
            Assert.IsNotNull(rlt);
        }

        [TestMethod]
        public void AttachDepartmentList()
        {
            Global.SetDataDirectory();

            EmployeeViewModel ev = new EmployeeViewModel();
            EmployeeController ec = new EmployeeController();
            Assert.IsNotNull(ev.Departments);
            Assert.IsTrue(ev.Departments.Count() >= 0);
        }
    }
}
