using FlyBugClub_WebApp.Models;
using Humanizer.Localisation;

namespace FlyBugClub_WebApp.Repository
{
    public interface IGenreRepository
    {
        public bool Create(CategoryDevice category);
        public bool Update(CategoryDevice category);
        public bool Delete(string Id);
        public List<CategoryDevice> GetAll();
        public CategoryDevice FindById(string id);
        public bool CheckNameCategory(string name);
        public bool CheckCategoryId(string Id);
    }
    public class GenreProductRepository : IGenreRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public GenreProductRepository(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public bool CheckCategoryId(string Id)
        {
            CategoryDevice category = _ctx.CategoryDevices.Where(x=>x.CategoryId == Id).FirstOrDefault();
            if(category == null)
                return false;
            else
                return true;
        }

        public bool CheckNameCategory(string name)
        {
            CategoryDevice category =_ctx.CategoryDevices.Where(x=>x.CategoryName== name).FirstOrDefault();
            if(category == null)
                return false;
            else
                return true;
        }

        public bool Create(CategoryDevice category)
        {
            _ctx.CategoryDevices.Add(category);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(string Id)
        {
            CategoryDevice category = _ctx.CategoryDevices.FirstOrDefault(x => x.CategoryId == Id);
            _ctx.CategoryDevices.Remove(category);
            _ctx.SaveChanges();
            return true;
        }

        public CategoryDevice FindById(string id)
        {
            CategoryDevice cd = _ctx.CategoryDevices.FirstOrDefault(x => x.CategoryId == id);
            return cd;
        }

        public List<CategoryDevice> GetAll()
        {
            return _ctx.CategoryDevices.ToList();
        }

        public bool Update(CategoryDevice category)
        {
            CategoryDevice c = _ctx.CategoryDevices.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(category);
                _ctx.SaveChanges();
            }
            return true;
        }
    }
}
