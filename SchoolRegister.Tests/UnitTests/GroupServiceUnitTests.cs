using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolRegister.Tests.UnitTests;
public class GroupServiceUnitTests : BaseUnitTests
{
    private readonly IGroupService _groupService;
    public GroupServiceUnitTests(ApplicationDbContext dbContext, IGroupService groupService) : base(dbContext)
    {
        _groupService = groupService;
    }
    [Fact]
    public void GetGroupAsync()
    {
        var addedGroup = _groupService.GetGroupAsync(x => x.Name == "PAI").Result;
        Assert.NotNull(addedGroup);
    }
    [Fact]
    public void GetGroupsAsync()
    {
        var groups = _groupService.GetGroupsAsync(x => x.Id >= 1 && x.Id <= 2).Result
        .ToList();
        Assert.NotNull(groups);
        Assert.NotEmpty(groups);
        Assert.Equal(2, groups.Count());
    }
    [Fact]
    public void GetAllGroups()
    {
        var groups = _groupService.GetGroupsAsync().Result.ToList();
        Assert.NotNull(groups);
        Assert.NotEmpty(groups);
        Assert.Equal(3, groups.Count());
    }
    [Fact]
    public void AddGroup()
    {
        var addOrUpdateGroupVm = new AddOrUpdateGroupVm
        {
            Name = "SK"
        };
        _groupService.AddOrUpdateGroupAsync(addOrUpdateGroupVm);
        Assert.Equal(4, DbContext.Groups.Count());
        var addedGroup = _groupService.GetGroupAsync(x => x.Name == "SK").Result;
        Assert.NotNull(addedGroup);
    }
    [Fact]
    public void UpdateGroup()
    {
        var addOrUpdateGroupVm = new AddOrUpdateGroupVm
        {
            Name = "SIiDM",
            Id = 3
        };
        _groupService.AddOrUpdateGroupAsync(addOrUpdateGroupVm);
        var addedGroup = _groupService.GetGroupAsync(x => x.Name == "SIiDM").Result;
        Assert.NotNull(addedGroup);
    }
    [Fact]
    public void AttachStudentToGroup()
    {
        var attachStudentToGroupVm = new AttachDetachStudentToGroupVm()
        {
            GroupId = 1,
            StudentId = 7
        };
        var student = _groupService.AttachStudentToGroupAsync(attachStudentToGroupVm).Result;
        Assert.True(student.GroupName == "IO");
        var group = _groupService.GetGroupAsync(g => g.Id == attachStudentToGroupVm.GroupId).Result;
        Assert.NotNull(group);
        Assert.NotNull(group.Students.FirstOrDefault(x => x.Id == 7));
    }
    //[Fact]
    //public void DetachStudentFromGroup()
    //{
    //    var detachStudentToGroupVm = new AttachDetachStudentToGroupVm()
    //    {
    //        GroupId = 1,
    //        StudentId = 7
    //    };
    //    var student = _groupService.DetachStudentFromGroupAsync(detachStudentToGroupVm).Result;
    //    Assert.NotNull(student);
    //    Assert.Null(student.GroupName);
    //}
    [Fact]
    public void AttachSubjectToGroup()
    {
        var attachSubjectGroupVm = new AttachDetachSubjectGroupVm()
        {
            GroupId = 1,
            SubjectId = 4
        };
        _groupService.AttachSubjectToGroupAsync(attachSubjectGroupVm);
        var group = _groupService.GetGroupAsync(g => g.Id == attachSubjectGroupVm.GroupId).Result;
        Assert.NotNull(group);
        Assert.NotNull(group.Subjects.FirstOrDefault(s => s.Name == "Administracja Intenetowymi Systemami Baz Danych"));
    }
    [Fact]
    public void DetachSubjectFromGroup()
    {
        var detachSubjectGroupVm = new AttachDetachSubjectGroupVm()
        {
            GroupId = 2,
            SubjectId = 4
        };
        var group = _groupService.DetachSubjectFromGroupAsync(detachSubjectGroupVm).Result;
        Assert.NotNull(group);
        Assert.Null(group.Subjects.FirstOrDefault(s => s.Name == "Administracja Intenetowymi Systemami Baz Danych"));
    }
    [Fact]
    public void AttachTeacherToSubject()
    {
        var attachSubjectTeacher = new AttachDetachSubjectToTeacherVm()
        {
            SubjectId = 5,
            TeacherId = 2
        };
        var subject = _groupService.AttachTeacherToSubjectAsync(attachSubjectTeacher).Result;
        Assert.NotNull(subject);
        Assert.True(subject.TeacherId == attachSubjectTeacher.TeacherId);
    }
    [Fact]
    public void DetachTeacherToSubject()
    {
        var detachSubjectTeacher = new AttachDetachSubjectToTeacherVm()
        {
            SubjectId = 3,
            TeacherId = 2
        };
        var subject = _groupService.DetachTeacherFromSubjectAsync(detachSubjectTeacher).Result;
        Assert.NotNull(subject);
        Assert.Null(subject.TeacherId);
        Assert.Null(subject.TeacherName);
    }
}
