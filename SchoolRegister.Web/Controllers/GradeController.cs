using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.Services.Interfaces;

namespace SchoolRegister.Web.Controllers
{
    public class GradeController : BaseController
    {
        private readonly IGradeService _gradeService;
        private readonly ITeacherService _teacherService;
        private readonly UserManager<User> _userManager;

        public GradeController(IGradeService gradeService, ITeacherService teacherService,
            UserManager<User> userManager, IStringLocalizer localizer, ILogger logger, IMapper mapper) : base(logger, mapper, localizer)
        {
            _gradeService = gradeService;
            _teacherService = teacherService;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    var grades;
        //    foreach (var item in _gradeService.GetGradesReportForStudent()
        //    {
                
        //    }

        //    var user = _userManager.GetUserAsync(User).Result;
        //    if (_userManager.IsInRoleAsync(user, "Admin").Result)
        //        return View();
        //    else if (_userManager.IsInRoleAsync(user, "Teacher").Result && user is Teacher teacher)
        //    {
        //        return View(_subjectService.GetSubjects(x => x.TeacherId == teacher.Id));
        //    }
        //    else if (_userManager.IsInRoleAsync(user, "Student").Result)
        //        return RedirectToAction("Details", "Student", new { studentId = user.Id });
        //    else
        //        return View("Error");
        //}

    }
}
