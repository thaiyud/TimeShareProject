﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeShareProject.Models;
using TimeShareProject.ViewModels;

namespace TimeShareProject.Controllers
{
    public class UserController : Controller
    {
        private readonly _4restContext _dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserController(_4restContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _hostingEnvironment = hostingEnvironment;

        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                return View(await _dbContext.Users.Include(u => u.Account).Where(u => u.Account.Role == 3 && u.Id == id).ToListAsync());
            }
            return View(await _dbContext.Users.Include(u => u.Account).Where(u => u.Account.Role == 3).ToListAsync());
        }

        public IActionResult UserProfile()
        {
            // Get the username of the currently logged-in user
            string username = User.Identity.Name;

            // Retrieve the user details from the database
            var user = _dbContext.Users.FirstOrDefault(u => u.Account.Username == username);

            if (user == null)
            {
                return NotFound(); // User not found
            }

            return View(user);
        }


        public async Task<IActionResult> EditPersonalInfo(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_dbContext.Accounts, "Id", "Id", user.AccountId);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPersonalInfo(int id, [Bind("Id,Name,Sex,DateOfBirth,PhoneNumber,Email,Address")] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing user data from the database
                    var existingUser = await _dbContext.Users.FindAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Update only the fields that are modified in the updatedUser
                    existingUser.Name = updatedUser.Name;
                    existingUser.Sex = updatedUser.Sex;
                    existingUser.DateOfBirth = updatedUser.DateOfBirth;
                    existingUser.PhoneNumber = updatedUser.PhoneNumber;
                    existingUser.Email = updatedUser.Email;
                    existingUser.Address = updatedUser.Address;

                    // Update the user
                    _dbContext.Update(existingUser);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(updatedUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserProfile));
            }
            ViewData["AccountId"] = new SelectList(_dbContext.Accounts, "Id", "Id", updatedUser.AccountId);
            return View(updatedUser);
        }


    


        public IActionResult EditAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditAccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(a => a.UserName == model.CurrentUsername && a.PasswordHash == model.CurrentPassword);
            if (account == null)
            {
                TempData["errorCorrectUsername"] = "Invalid current username or password!";
                return RedirectToAction("EditAccount");
            }

            // Update username and password
            account.UserName = model.NewUsername;
            account.PasswordHash = model.NewPassword;
            _dbContext.Update(account);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(UserProfile));
        }

        private bool UserExists(string id)
        {
            return _dbContext.Users.Any(e => e.Id.ToString() == id);
        }
        public IActionResult EditIdentityCard()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIdentityCard(User user, IFormFile IdfrontImage, IFormFile IdbackImage)
        {
            if (user == null)
            {
                return NotFound();
            }

            user.IdfrontImage = await SaveUserImage(user, IdfrontImage);
            user.IdbackImage = await SaveUserImage(user, IdbackImage);

            _dbContext.Users.Attach(user);
            _dbContext.Entry(user).Property(x => x.IdfrontImage).IsModified = true;
            _dbContext.Entry(user).Property(x => x.IdbackImage).IsModified = true;

            //_dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(UserProfile));
        }

        public async Task<string> SaveUserImage(ApplicationUser user, IFormFile imageFile)
        {
            if (user == null || imageFile == null || imageFile.Length == 0)
            {
                return null;
            }
            var userName = User.Identity.Name;
            var fileName = Path.GetFileName(imageFile.FileName);
            var path = Path.Combine("img", userName);
            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return User.Identity.Name + "/" + fileName;
        }
        public ActionResult GetUserTransactions(string Id)
        {

            string username = User.Identity.Name;

            var user = _dbContext.Users
                                 .Include(u => u.Reservations)
                                 .ThenInclude(r => r.Transactions)
                                 .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return NotFound(); // User not found
            }


            List<Transaction> userTransactions = new List<Transaction>();

            foreach (var reservation in user.Reservations)
            {
                if (reservation.Id == Id)
                {
                    userTransactions.AddRange(reservation.Transactions);
                }
            }

            return View(userTransactions);
        }

        public IActionResult GetUserReservation()
        {
            string username = User.Identity.Name;

            var user = _dbContext.Users
                                 .Include(u => u.Reservations)
                                 .ThenInclude(r => r.Transactions)
                                 .FirstOrDefault(u => u.UserName == username);
            string userId = user.Id.ToString();

            var userReservations = _dbContext.Reservations.Include(r => r.Property)
                .Where(r => r.UserId == userId)
                .ToList();

            return View(userReservations);
        }
    }
}

