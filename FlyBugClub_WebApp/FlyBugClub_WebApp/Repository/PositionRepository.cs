using FlyBugClub_WebApp.Models;

namespace FlyBugClub_WebApp.Repository
{
    public interface IPositionRepository
    {
        List<Position> GetAll();
    }
    public class PositionRepository : IPositionRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public PositionRepository(FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        } 
        public List<Position> GetAll()
        {
            return _ctx.Positions.ToList();
        }
    }
}
