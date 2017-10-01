using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using NameSearch.EmployeeEntity;
using NameSearch.Repository;
using NameSearch.Models;
using NameSearch.ViewModels;
using NameSearch.Helpers;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;

namespace NameSearch.Controllers
{
    public class EmployeeController : Controller
    {
        private int itemsPerPage = 5;

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilterBy(string _name, int _page = 1)
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                List<EmployeeViewModel> list = new List<EmployeeViewModel>();
                IEnumerable<Employee> employees;
                if ((_name ?? "") == "")
                {
                    employees = em.GetAllItems().OrderBy(e => e.FirstName).ThenBy(e => e.LastName).Skip((_page - 1) * itemsPerPage).Take(itemsPerPage);
                    foreach (Employee emp in employees)
                    {
                        EmployeeViewModel ev = new EmployeeViewModel();
                        em.ConvertModelToViewModel(emp, ev);
                        list.Add(ev);
                    }
                    ViewBag.Name = _name;
                    ViewBag.PageInfo = new PageInfo() { CurrPage = _page, ItemsPerPage = itemsPerPage, TotalItems = em.GetAllItems().Count() };
                    return PartialView(list);
                }
                else
                {
                    //Delay simulation
                    Thread.Sleep(2000);

                    Expression<Func<Employee, bool>> exp = Employee => Employee.FirstName.Contains(_name) ||
                                                           Employee.LastName.Contains(_name);
                    employees = em.FilterItems(exp).OrderBy(e => e.FirstName).ThenBy(e => e.LastName).Skip((_page - 1) * itemsPerPage).Take(itemsPerPage);
                    foreach (Employee emp in employees)
                    {
                        EmployeeViewModel ev = new EmployeeViewModel();
                        em.ConvertModelToViewModel(emp, ev);
                        list.Add(ev);
                    }
                    ViewBag.Name = _name;
                    ViewBag.PageInfo = new PageInfo() { CurrPage = _page, ItemsPerPage = itemsPerPage, TotalItems = em.FilterItems(exp).Count() };
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string output = js.Serialize(list);
                    return Json(output);
                }
            }
        }

        public ActionResult PageInfo(string _name, int _page) 
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                Expression<Func<Employee, bool>> exp = Employee => Employee.FirstName.Contains(_name) ||
                                                       Employee.LastName.Contains(_name);
                ViewBag.Name = _name;
                ViewBag.PageInfo = new PageInfo() { CurrPage = _page, ItemsPerPage = itemsPerPage, TotalItems = em.FilterItems(exp).Count() };
                return PartialView();
            }
        }

        public ActionResult GetAll()
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                var employees = em.GetAllItems();
                return View(employees);
            }
        }

        public ActionResult Create()
        {
            EmployeeViewModel ev = new EmployeeViewModel();
            return View(ev);
        }

        [HttpPost]
        public ActionResult Insert(EmployeeViewModel _ev)
        {
            if (ModelState.IsValid)
            {
                using (EmployeeEntities context = new EmployeeEntities())
                {
                    EmployeeModel em = new EmployeeModel(context);
                    em.Insert(_ev);
                    return RedirectToAction("Edit", "Employee", new { _id = _ev.Id });
                }
            }
            else
            {
                return View("Create", _ev);
            }
        }

        public ActionResult Details(int _id)
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                EmployeeViewModel ev = em.GetItemViewModelById(_id);
                return View(ev);
            }
        }

        public ActionResult Edit(int _id)
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                EmployeeViewModel ev = em.GetItemViewModelById(_id);
                return View(ev);
            }
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel _ev)
        {
            if (ModelState.IsValid)
            {
                using (EmployeeEntities context = new EmployeeEntities())
                {
                    EmployeeModel em = new EmployeeModel(context);
                    em.Update(_ev);
                }
            }
            return View("Edit", _ev);
        }

        [HttpPost]
        public void Delete(int _id)
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                var item = em.GetItemById(_id);
                if (item != null && item.PictureUrl != null)
                {
                    RemovePhoto(item.Id, item.PictureUrl);
                }
                em.Delete(_id);
            }
        }

        public void RemovePhoto(int _id, string _pictureUrl)
        {
            using (EmployeeEntities context = new EmployeeEntities())
            {
                EmployeeModel em = new EmployeeModel(context);
                em.RemovePicture(_id);

                string filePath = em.PhotoFilePath + "\\" + _pictureUrl;
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
        }

        public void UploadPhoto()
        {
            if (Request.Files.Count > 0)
            {
                var upload = Request.Files[0];
                if (upload != null)
                {
                    int id = Convert.ToInt32(Request.Form.GetValues("Id")[0]);
                    string fileName = Path.GetFileNameWithoutExtension(upload.FileName) + DateTime.Now.ToString("_yyyy-MM-dd hh-mm-ss") + Path.GetExtension(upload.FileName);
                    byte[] content;

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        content = reader.ReadBytes(upload.ContentLength);
                    }

                    using (EmployeeEntities context = new EmployeeEntities())
                    {
                        EmployeeModel em = new EmployeeModel(context);
                        System.IO.File.WriteAllBytes(em.PhotoFilePath + "\\" + fileName, content);

                        em.AddPicture(id, fileName);
                    }
                }
            }
        }
    }
}