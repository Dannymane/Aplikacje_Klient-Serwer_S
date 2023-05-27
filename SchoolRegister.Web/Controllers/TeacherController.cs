using AutoMapper;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRegister.Web.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;
        
        public TeacherController(ITeacherService teacherService, IStudentService studentService, UserManager<User> userManager, IStringLocalizer localizer, ILogger logger, IMapper mapper) : base(logger, mapper, localizer)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //var user = _userManager.GetUserAsync(User).Result;
            //if(_userManager.IsInRoleAsync(user, "Admin").Result)
            //    return View(_teacherService.GetTeachers());
            //if(_userManager.IsInRoleAsync(user, "Teacher").Result)
            //    return View(_teacherService.GetTeacher(x => x.Id == user.Id));

            //if(_userManager.IsInRoleAsync(user, "Student").Result)
            //    return RedirectToAction("Details", "Student", new { studentId = user.Id });

            return View();
        }

        //public IActionResult getStudents

        //public IActionResult PostGradeForStudent()
        //{

        //}
    }
}
