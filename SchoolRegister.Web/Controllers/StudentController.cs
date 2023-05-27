using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.ConcreteServices;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System.Linq.Expressions;

namespace SchoolRegister.Web.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ISubjectService _subjectService;
        private readonly IGradeService _gradeService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;
        public StudentController(ISubjectService subjectService, IGradeService gradeService ,ITeacherService teacherService, IStudentService studentService, UserManager<User> userManager, IStringLocalizer localizer, ILogger logger, IMapper mapper) : base(logger, mapper, localizer)
        {
            _gradeService = gradeService;
            _teacherService = teacherService;
            _studentService = studentService;
            _userManager = userManager;
            _subjectService = subjectService;
        }

        [Authorize(Roles = "Admin, Teacher, Parent")]
        public async Task<IActionResult> IndexAsync(string? filter = null)
        {
            var user = await _userManager.GetUserAsync(User);
            Expression<Func<Student, bool>>? filterExpression = null;

            if (!string.IsNullOrEmpty(filter))
                filterExpression = s => s.FirstName.Contains(filter);

            //if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Teacher"))
            return View(await _studentService.GetStudentsAsync(filterExpression));
        }

        public async Task<IActionResult> Details(int id)
        {
            var studentVm = await _studentService.GetStudentAsync(x => x.Id == id);
            return View(studentVm);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddGradeToStudent(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
                return RedirectToAction("Login", "Account"); //?

            Expression<Func<Subject, bool>>?  filterExpression = s => s.TeacherId == user.Id;
            var subjectVms = _subjectService.GetSubjects(filterExpression);

            ViewBag.SubjectsSelectList = new SelectList(subjectVms.Select(s => new
            {
                Text = $"{s.Name}",
                Value = s.Id
            }), "Value", "Text");

            var studentVm = await _studentService.GetStudentAsync(s => s.Id == id);
            ViewBag.StudentName = $"{studentVm.FirstName} {studentVm.LastName}";

            var addGradeToStudentVm = new AddGradeToStudentVm()
            {
                StudentId = id,
                TeacherId = user.Id,
                DateOfIssue = DateTime.Now
            };

            return View(addGradeToStudentVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Teacher")]
        public IActionResult AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.AddGradeToStudent(addGradeToStudentVm);
                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
