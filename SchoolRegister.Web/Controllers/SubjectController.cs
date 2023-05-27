﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
namespace SchoolRegister.Web.Controllers;
[Authorize(Roles = "Teacher, Admin, Student")]
public class SubjectController : BaseController
{
    private readonly ISubjectService _subjectService;
    private readonly ITeacherService _teacherService;
    private readonly UserManager<User> _userManager;
    public SubjectController(ISubjectService subjectService,
    ITeacherService teacherService,
    UserManager<User> userManager,
    IStringLocalizer localizer,
    ILogger logger,
    IMapper mapper) : base(logger, mapper, localizer)
    {
        _subjectService = subjectService;
        _teacherService = teacherService;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (await _userManager.IsInRoleAsync(user, "Admin"))
            return View(_subjectService.GetSubjects());
        else if (await _userManager.IsInRoleAsync(user, "Teacher") && user is Teacher teacher)
        {
            return View(_subjectService.GetSubjects(x => x.TeacherId == teacher.Id));
        }
        //else if (await _userManager.IsInRoleAsync(user, "Student"))
        //    return RedirectToAction("Details", "Student", new { studentId = user.Id });
        else
            return View("Error");
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditSubject(int? id = null)
    {
        var teachersVm = _teacherService.GetTeachers();
        ViewBag.TeachersSelectList = new SelectList(teachersVm.Select(t => new {
            Text = $"{t.FirstName} {t.LastName}",
            Value = t.Id
        }), "Value", "Text");
        if (id.HasValue)
        {
            var subjectVm = _subjectService.GetSubject(x => x.Id == id);
            ViewBag.ActionType = "Edit";
            return View(Mapper.Map<AddOrUpdateSubjectVm>(subjectVm));
        }
        ViewBag.ActionType = "Add";
        return View();
    }
    //default attribute is [HttpGet]
    public IActionResult Details(int id)
    {
        var subjectVm = _subjectService.GetSubject(x => x.Id == id);
        return View(subjectVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult AddOrEditSubject(AddOrUpdateSubjectVm addOrUpdateSubjectVm)
    {
        if (ModelState.IsValid)
        {
            _subjectService.AddOrUpdateSubject(addOrUpdateSubjectVm);
            return RedirectToAction("Index");
        }
        return View();
    }
}
