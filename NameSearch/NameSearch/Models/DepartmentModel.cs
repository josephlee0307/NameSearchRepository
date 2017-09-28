using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using EmployeeEntity;
using Repository;
using NameSearch.ViewModels;
namespace NameSearch.Models
{
    public class DepartmentModel
    {
        private DbContext context;
        private Repository<Department> repo;

        public DepartmentModel()
        {
            context = new EmployeeEntities();
            repo = new Repository<Department>(context);
        }

        public void Insert(Department _item)
        {
            repo.Add(_item);
            context.SaveChanges();
        }

        public void Delete(int _id)
        {
            Department item = repo.GetItemById(_id);
            repo.Remove(item);
            context.SaveChanges();
        }

        public void Update(Department _item)
        {
            repo.Update(_item);
            context.SaveChanges();
        }

        public IEnumerable<Department> GetAllItems()
        {
            return repo.GetAllItems();
        }

        public Department GetItemById(int _id)
        {
            return repo.GetItemById(_id);
        }
    }
}