using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EmployeeEntity;
using NameSearch.Models;
using NameSearch.Helpers;

namespace NameSearch.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Departments = new List<SelectListItem>();

            DepartmentModel dm = new DepartmentModel();
            var departments = dm.GetAllItems().OrderBy(e => e.Name);
            foreach (Department d in departments)
            {
                Departments.Add(new SelectListItem() { Value = d.Id.ToString(), Text = d.Name });
            }

            PageInfo = new PageInfo();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address
        {
            get { return Address1 + " " + Address2; }
        }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Interests")]
        public string Interest { get; set; }

        [DisplayName("Photo")]
        public string PictureUrl { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        [DisplayName("Department")]
        public string DepartmentName { get; set; }

        public List<SelectListItem> Departments { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}