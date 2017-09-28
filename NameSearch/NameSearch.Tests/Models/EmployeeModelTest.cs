using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSearch;
using NameSearch.Models;

namespace NameSearch.Tests.Models
{
    [TestClass]
    public class EmployeeModelTest
    {
        [TestMethod]
        public void GetAllItems()
        {
            Global.SetDataDirectory();

            EmployeeModel emp = new EmployeeModel();
            var employees = emp.GetAllItems();
            Assert.IsTrue(employees.Count() >= 0);
        }
    }
}
