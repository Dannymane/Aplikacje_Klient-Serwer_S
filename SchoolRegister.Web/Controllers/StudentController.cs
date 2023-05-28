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
        private readonly IGroupService _groupService;
        private readonly ISubjectService _subjectService;
        private readonly IGradeService _gradeService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly UserManager<User> _userManager;
        public StudentController(IGroupService groupService, ISubjectService subjectService, IGradeService gradeService ,ITeacherService teacherService, IStudentService studentService, UserManager<User> userManager, IStringLocalizer localizer, ILogger logger, IMapper mapper) : base(logger, mapper, localizer)
        {
            _gradeService = gradeService;
            _teacherService = teacherService;
            _studentService = studentService;
            _userManager = userManager;
            _subjectService = subjectService;
            _groupService = groupService;
        }

        [Authorize(Roles = "Admin, Teacher, Parent, Student")]
        public async Task<IActionResult> Index() //IndexAsync(string? filter = null)
        {
            var user = await _userManager.GetUserAsync(User);
            Expression<Func<Student, bool>>? filterExpression = null;

            //if (!string.IsNullOrEmpty(filter))
            //    filterExpression = s => s.FirstName.Contains(filter);
            if (await _userManager.IsInRoleAsync(user, "Student"))
                filterExpression = s => s.Id == user.Id;

            if (await _userManager.IsInRoleAsync(user, "Parent"))
                filterExpression = s => s.ParentId == user.Id;
                
                return View(await _studentService.GetStudentsAsync(filterExpression));
        }

        public async Task<IActionResult> Details(int id)
        {
            var studentVm = await _studentService.GetStudentAsync(s => s.Id == id);
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


        public async Task<IActionResult> GetGradesReport(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            GetGradesReportVm getGradesVm = new GetGradesReportVm()
            {
                GetterUserId = user.Id,
                StudentId = id
            };
            var gradesReportVm = _gradeService.GetGradesReportForStudent(getGradesVm);

            return View(gradesReportVm);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AttachStudentToGroup(int id)
        {
            var studentVm = await _studentService.GetStudentAsync(s => s.Id == id);
            ViewBag.StudentName = $"{studentVm.FirstName} {studentVm.LastName}";

            var groupsVm = await _groupService.GetGroupsAsync(g => g.Students.All(s => s.Id != id));
            ViewBag.GroupSelectList = new SelectList(groupsVm.Select(g => new
            {
                Text = g.Name,
                Value = g.Id
            }), "Value", "Text");

            return View(Mapper.Map<AttachDetachStudentToGroupVm>(studentVm));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroupVm)
        {
            if (ModelState.IsValid)
            {
                await _groupService.AttachStudentToGroupAsync(attachDetachStudentToGroupVm);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DetachStudentFromGroup(int id)
        {
            var studentVm = await _studentService.GetStudentAsync(s => s.Id == id);
            AttachDetachStudentToGroupVm attachDetachStudentToGroupVm = Mapper.Map<AttachDetachStudentToGroupVm>(studentVm);

            await _groupService.DetachStudentFromGroupAsync(attachDetachStudentToGroupVm);

            return RedirectToAction("Index");
        }

    }
}
