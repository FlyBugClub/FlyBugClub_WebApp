using FlyBugClub_WebApp.Models;

namespace FlyBugClub_WebApp.Repository
{
    public interface IMemberRepository
    {
        List<User> getAll();
        
    }
    public class MemberRepository : IMemberRepository
    {
        private FlyBugClubWebApplicationContext _ctx;
        public MemberRepository (FlyBugClubWebApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public List<User> getAll()
        {
            return _ctx.Users.ToList();
        }
    }
}
