using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeShareProject.Models;

namespace TimeShareProject.Controllers
{
    public class AccountsController : Controller
    {
        private readonly _4restContext _context;

        public AccountsController(_4restContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Users
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser account, string newUsername, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var existedUsername = _context.Users.Where(u => u.UserName == newUsername);
                if (existedUsername.Any())
                {
                    TempData["errorExistUsername"] = "Username already exists!";
                    return RedirectToAction("Create");

                }
                var newAccount = new ApplicationUser
                {
                    UserName = newUsername,
                    PasswordHash = newPassword,
                    //Role = account.Role
                };
                _context.Add(newAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Users.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Username,Password,Role")] ApplicationUser account)
        {
            if (id != account.Id.ToString())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id.ToString()))
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
            return View(account);
        }

   
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Users
                .FirstOrDefaultAsync(m => m.Id.ToString() == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Users.FindAsync(id);
            if (account != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
                if (user != null)
                {
                   
                    user.Status = false;
                    _context.Update(user);
                }

                _context.Users.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return _context.Users.Any(e => e.Id.ToString() == id);
        }
    }
}
