using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using NameSearch.EmployeeEntity;
using NameSearch.Repository;
using NameSearch.ViewModels;

namespace NameSearch.Models
{

    public class EmployeeModel
    {
        private DbContext context;
        private Repository<Employee> repo;
        private const string PHOTO_LOCATION = "~/Content/photos";

        public EmployeeModel(DbContext _context)
        {
            context = _context;
            repo = new Repository<Employee>(context);
        }

        public void Insert(Employee _item)
        {
            repo.Add(_item);
            context.SaveChanges();
        }

        public void Insert(EmployeeViewModel _item)
        {
            Employee emp = new Employee();
            ConvertViewModelToModel(_item, emp);
            Insert(emp);
            _item.Id = emp.Id;
        }

        public void Delete(int _id)
        {
            Employee item = repo.GetItemById(_id);
            repo.Remove(item);
            context.SaveChanges();
        }

        public void Update(Employee _item)
        {
            repo.Update(_item);
            context.SaveChanges();
        }

        public void Update(EmployeeViewModel _item)
        {
            Employee emp = new Employee();
            ConvertViewModelToModel(_item, emp);
            Update(emp);
        }

        public IEnumerable<Employee> GetAllItems()
        {
            return repo.GetAllItems();
        }

        public Employee GetItemById(int _id)
        {
            return repo.GetItemById(_id);
        }

        public EmployeeViewModel GetItemViewModelById(int _id)
        {
            EmployeeViewModel ev = new EmployeeViewModel();
            Employee emp = repo.GetItemById(_id);
            ConvertModelToViewModel(emp, ev);
            return ev;
        }

        public IEnumerable<Employee> FilterItems(Expression<Func<Employee, bool>> _predicate)
        {
            return repo.FilterItems(_predicate);
        }

        public void RemovePicture(int _id)
        {
            var item = repo.GetItemById(_id);
            if (item != null)
            {
                item.PictureUrl = null;
                context.SaveChanges();
            }
        }

        public void AddPicture(int _id, string _fileName)
        {
            var item = repo.GetItemById(_id);
            if (item != null)
            {
                item.PictureUrl = _fileName;
                context.SaveChanges();
            }
        }

        public string PhotoFilePath
        {
            get { return System.Web.HttpContext.Current.Server.MapPath(PHOTO_LOCATION); }
        }

        public void ConvertModelToViewModel(Employee _m, EmployeeViewModel _vm)
        {
            _vm.Id = _m.Id;
            _vm.FirstName = _m.FirstName;
            _vm.LastName = _m.LastName;
            _vm.Age = _m.Age;
            _vm.Address1 = _m.Address1;
            _vm.Address2 = _m.Address2;
            _vm.City = _m.City;
            _vm.State = _m.State;
            _vm.Zip = _m.Zip;
            _vm.Interest = _m.Interest;
            _vm.PictureUrl = _m.PictureUrl;
            _vm.DepartmentId = _m.DepartmentId;
            if (_m.DepartmentId != null)
            {
                DepartmentModel dm = new DepartmentModel(context);
                _vm.DepartmentName = dm.GetItemById((int) _m.DepartmentId).Name;
            }
        }

        public void ConvertViewModelToModel(EmployeeViewModel _vm, Employee _m)
        {
            _m.Id = _vm.Id;
            _m.FirstName = _vm.FirstName;
            _m.LastName = _vm.LastName;
            _m.Age = _vm.Age;
            _m.Address1 = _vm.Address1;
            _m.Address2 = _vm.Address2;
            _m.City = _vm.City;
            _m.State = _vm.State;
            _m.Zip = _vm.Zip;
            _m.Interest = _vm.Interest;
            _m.PictureUrl = _vm.PictureUrl;
            _m.DepartmentId = _vm.DepartmentId;
        }
    }
}