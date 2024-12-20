using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammasy.Data;
using TestProgrammasy.Models;

namespace TestProgrammasy.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> GetUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault(); // Return first role found
        }

        public async Task<string> GetUserFullName(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? $"{user.FirstName} {user.LastName}" : null;
        }

        //public async Task<List<string>> GetTeacherSubjects(string teacherId)
        //{
        //    return await _context.TeacherSubjects
        //        .Where(ts => ts.TeacherId == teacherId)
        //        .Select(ts => ts.Subject)
        //        .ToListAsync();
        //}

        public async Task<bool> IsTeacher(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, "Teacher");
        }

        public async Task<bool> IsAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<bool> IsStudent(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, "Student");
        }
    }
}
