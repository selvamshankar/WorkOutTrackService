using System;
using System.Collections.Generic;
using System.Linq;
using WorkOutDBModel.Model;

namespace WorkOutDBLayer
{
    public class CateogryDb
    {
        WorkOutDBLayer.MyContext db = null;
        public CateogryDb()
        {
            db = new WorkOutDBLayer.MyContext();
        }

        public List<Category> GetAll()
        {
            try
            {
                return db.Categories.ToList();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }


        public Category GetById(int Id)
        {
            try
            {
                return db.Categories.Find(Id);
            }
            catch (Exception ex)
            {
                return new Category();
            }
        }

        public bool Add(Category model)
        {
            try
            {
                if (model.CategoryId > 0)
                {
                    model.Updated = DateTime.Now;
                    Category editCategory = db.Categories.Find(model.CategoryId);
                    db.Entry(editCategory).CurrentValues.SetValues(model);
                }
                else
                {
                    model.Created = DateTime.Now;
                    db.Categories.Add(model);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(int Id)
        {
            try
            {
                Category category = db.Categories.Find(Id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<Category> GetByName(string categoryName)
        {
            try
            {
                return db.Categories.Where(a => a.CategoryName.ToLower().Contains(categoryName.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                return new List<Category>();
            }
        }


    }
}
