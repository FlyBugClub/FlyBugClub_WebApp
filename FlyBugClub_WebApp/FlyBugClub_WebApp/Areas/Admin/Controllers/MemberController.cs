using FlyBugClub_WebApp.Areas.Identity.Data;
using FlyBugClub_WebApp.Models;
using FlyBugClub_WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlyBugClub_WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class MemberController : Controller
    {
        private readonly FlyBugClubWebApplicationContext _ctx;
        private IMemberRepository _memberRepository;
        private IPositionRepository _positionRepository;

        public MemberController(FlyBugClubWebApplicationContext ctx, 
                                IMemberRepository memberRepository,
                                IPositionRepository positionRepository)
        {
            _ctx = ctx;
            _memberRepository = memberRepository;
            _positionRepository = positionRepository;   
        }

        public IActionResult Member()
        {
            
            List<User> lst = _memberRepository.getAll();

            List<Position> positions = _positionRepository.GetAll();
            Dictionary<string, string> positionNames = new Dictionary<string, string>();
            foreach (var position in positions)
            {
                positionNames[position.PositionId] = position.PositionName;
            }
            ViewBag.PositionNames = positionNames;

            return View("Member", lst);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _ctx.Users == null)
            {
                return NotFound();
            }

            var user = await _ctx.Users
                .Include(u => u.Position)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (user == null)
            {
                return NotFound();
            }

            var roleNameList = _positionRepository.GetAll();
            ViewBag.PositionSelectList = new SelectList(roleNameList, "PositionId", "PositionName");

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            var roleNameList = _positionRepository.GetAll();
            ViewBag.PositionSelectList = new SelectList(roleNameList, "PositionId", "PositionName");

            ViewData["PositionId"] = new SelectList(_ctx.Positions, "PositionId", "PositionId");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,Gender,Phone,Address,Email,Major,Faculty,ImgUser,PositionId,Account")] User user)
        {
            var roleNameList = _positionRepository.GetAll();
            ViewBag.PositionSelectList = new SelectList(roleNameList, "PositionId", "PositionName");

            if (ModelState.IsValid)
            {
                _ctx.Add(user);
                await _ctx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_ctx.Positions, "PositionId", "PositionId", user.PositionId);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _ctx.Users == null)
            {
                return NotFound();
            }

            var user = await _ctx.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleNameList = _positionRepository.GetAll();
            ViewBag.PositionSelectList = new SelectList(roleNameList, "PositionId", "PositionName");

            ViewData["PositionId"] = new SelectList(_ctx.Positions, "PositionId", "PositionId", user.PositionId);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,Name,Gender,Phone,Address,Email,Major,Faculty,ImgUser,PositionId,Account")] User user)
        {
            var roleNameList = _positionRepository.GetAll();
            ViewBag.PositionSelectList = new SelectList(roleNameList, "PositionId", "PositionName");

            if (id != user.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Update(user);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PositionId"] = new SelectList(_ctx.Positions, "PositionId", "PositionId", user.PositionId);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _ctx.Users == null)
            {
                return NotFound();
            }

            var user = await _ctx.Users
                .Include(u => u.Position)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_ctx.Users == null)
            {
                return Problem("Entity set 'FlyBugClubWebApplicationContext.Users'  is null.");
            }
            var user = await _ctx.Users.FindAsync(id);
            if (user != null)
            {
                _ctx.Users.Remove(user);
            }

            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return (_ctx.Users?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }


    }
}
