using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<User> _userManager;

        public GroupController(IGroupService groupService, ISubjectService subjectService, UserManager<User> userManager,
            IStringLocalizer localizer, ILogger logger, IMapper mapper) : base(logger, mapper, localizer)
        {
            _groupService = groupService;
            _subjectService = subjectService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _groupService.GetGroupsAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOrEditGroup(int? id = null)
        {
            if (id.HasValue)
            {
                var groupVm = await _groupService.GetGroupAsync(g => g.Id == id);
                ViewBag.ActionType = Localizer["Edit"];

                return View(Mapper.Map<AddOrUpdateGroupVm>(groupVm));
            }
            ViewBag.ActionType = Localizer["Add"];

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEditGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            if (ModelState.IsValid)
            {
                await _groupService.AddOrUpdateGroupAsync(addOrUpdateGroupVm);

                return RedirectToAction("Index");
            }
            return View();
        }



    }
}
