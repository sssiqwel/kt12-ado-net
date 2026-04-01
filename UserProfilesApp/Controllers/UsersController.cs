using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfilesApp.Data;
using UserProfilesApp.Models;
using UserProfilesApp.ViewModels;

namespace UserProfilesApp.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _db.Users
            .Include(x => x.Profile)
            .OrderBy(x => x.Id)
            .ToListAsync();

        return View(users);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var user = await _db.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (user is null) return NotFound();

        return View(user);
    }

    public IActionResult Create()
    {
        return View(new UserUpsertViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserUpsertViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = new User
        {
            UserName = vm.UserName.Trim(),
            Email = vm.Email.Trim(),
            Profile = new UserProfile
            {
                FirstName = vm.FirstName.Trim(),
                LastName = vm.LastName.Trim(),
                BirthDate = vm.BirthDate,
                Bio = string.IsNullOrWhiteSpace(vm.Bio) ? null : vm.Bio.Trim()
            }
        };

        _db.Users.Add(user);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Не удалось сохранить. Возможно, username/email уже заняты.");
            return View(vm);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var user = await _db.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (user is null) return NotFound();

        var vm = new UserUpsertViewModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.Profile.FirstName,
            LastName = user.Profile.LastName,
            BirthDate = user.Profile.BirthDate,
            Bio = user.Profile.Bio
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserUpsertViewModel vm)
    {
        if (vm.Id is null || vm.Id.Value != id) return NotFound();
        if (!ModelState.IsValid) return View(vm);

        var user = await _db.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return NotFound();

        user.UserName = vm.UserName.Trim();
        user.Email = vm.Email.Trim();

        user.Profile.FirstName = vm.FirstName.Trim();
        user.Profile.LastName = vm.LastName.Trim();
        user.Profile.BirthDate = vm.BirthDate;
        user.Profile.Bio = string.IsNullOrWhiteSpace(vm.Bio) ? null : vm.Bio.Trim();

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Не удалось сохранить. Возможно, username/email уже заняты.");
            return View(vm);
        }

        return RedirectToAction(nameof(Details), new { id = user.Id });
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var user = await _db.Users
            .Include(x => x.Profile)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (user is null) return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null) return RedirectToAction(nameof(Index));

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}

