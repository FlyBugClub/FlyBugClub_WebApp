using FlyBugClub_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyBugClub_WebApp.Repository
{
    public interface IDashboardRepository
    {

    }
    public class DashboardRepository : IDashboardRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public DashboardRepository(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }

    }
}
