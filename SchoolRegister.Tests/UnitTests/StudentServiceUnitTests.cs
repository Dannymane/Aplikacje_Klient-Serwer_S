using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolRegister.Tests.UnitTests;
public class StudentServiceUnitTests : BaseUnitTests
{
    private readonly IStudentService _studentService;
    public StudentServiceUnitTests(ApplicationDbContext dbContext, IStudentService studentService) : base(dbContext)
    {
        _studentService = studentService;
    }
    [Fact]
    public void GetStudent()
    {
        var student = _studentService.GetStudentAsync(s => s.Id == 8).Result;
        Assert.NotNull(student);
    }
    [Fact]
    public void GetStudents()
    {
        var students = _studentService.GetStudentsAsync(s => s.Id >= 5 && s.Id <= 7).Result
        .ToList();
        Assert.NotNull(students);
        Assert.NotEmpty(students);
        Assert.Equal(3, students.Count());
    }
    [Fact]
    public void GetAllStudents()
    {
        var students = _studentService.GetStudentsAsync().Result
        .ToList();
        Assert.NotNull(students);
        Assert.NotEmpty(students);
        Assert.Equal(6, students.Count());
    }
}
