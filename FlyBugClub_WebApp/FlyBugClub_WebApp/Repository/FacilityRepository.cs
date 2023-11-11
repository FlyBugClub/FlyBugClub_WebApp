using FlyBugClub_WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlyBugClub_WebApp.Repository
{
    public interface IFacilityRepository
    {
        public List<ReceivingFacility> GetAll();
    }
    public class FacilityRepository : IFacilityRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public FacilityRepository(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public List<ReceivingFacility> GetAll()
        {
            return _ctx.ReceivingFacilities.ToList();
        }
    }
}
