﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProgrammasy.Services.UserService
{
    public interface IUserService
    {
        Task<string> GetUserRole(string userId);
        Task<string> GetUserFullName(string userId);
        //Task<List<string>> GetTeacherSubjects(string teacherId);
        Task<bool> IsTeacher(string userId);
        Task<bool> IsAdmin(string userId);
        Task<bool> IsStudent(string userId);
    }
}