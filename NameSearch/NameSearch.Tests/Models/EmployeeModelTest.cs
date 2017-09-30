using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NameSearch;
using NameSearch.Models;
using NameSearch.EmployeeEntity;

namespace NameSearch.Tests.Models
{
    [TestClass]
    public class EmployeeModelTest
    {
        [TestMethod]
        public void GetAllItems()
        {
            Global.SetDataDirectory();

            using(EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel emp = new EmployeeModel(context);
                var employees = emp.GetAllItems();
                Assert.IsTrue(employees.Count() >= 0);
            }
        }

        [TestMethod]
        public void Filter()
        {
            Global.SetDataDirectory();

            using (EmployeeEntities context = new EmployeeEntities())
            {
                string name = "lee";
                EmployeeModel emp = new EmployeeModel(context);
                Expression<Func<Employee, bool>> exp = Employee => Employee.FirstName.Contains(name) ||
                                                                   Employee.LastName.Contains(name);
                var employees = emp.FilterItems(exp);
                Assert.IsTrue(employees.Count() >= 1);
            }

        }
    }
}
