﻿using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles;
public class MainProfile : Profile
{
    public MainProfile()
    {
        //AutoMapper maps
        CreateMap<Subject, SubjectVm>() // map from Subject(src) to SubjectVm(dst)
                                        // custom mapping: FirstName and LastName concat string to TeacherName
        .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => src.Teacher == null ? null :
        $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
        // custom mapping: IList<Group> to IList<GroupVm>
        .ForMember(dest => dest.Groups, x => x.MapFrom(src => src.SubjectGroups.Select(y => y.Group)))
        .ForMember(dest => dest.Grades, x => x.MapFrom(src => src.Grades)); //dodałem do kodu prowadzącego

        CreateMap<AddOrUpdateSubjectVm, Subject>();
        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();


        CreateMap<Group, GroupVm>()
        .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students))
        .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.SubjectGroups.Select(s => s.Subject)));


        CreateMap<User, UserVm>();
        CreateMap<UserVm, User>();

        CreateMap<Student, StudentVm>()
        .ForMember(dest => dest.GroupName, x => x.MapFrom(src => src.Group == null ? null : src.Group.Name))
        .ForMember(dest => dest.ParentName,
        x => x.MapFrom(src => src.Parent == null ? null : $"{src.Parent.FirstName} {src.Parent.LastName}"))
        .ForMember(dest => dest.Grades, x => x.MapFrom(src => src.Grades));

        CreateMap<StudentVm, Student>()
            .ForMember(dest => dest.Grades, x => x.MapFrom(src => src.Grades));

        CreateMap<Teacher, TeacherVm>()
        .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.Subjects));

        CreateMap<TeacherVm, Teacher>()
            .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.Subjects));

        CreateMap<Grade, GradeVm>()
            .ForMember(dest => dest.SubjectName,
            x => x.MapFrom(src => src.Subject == null ? null : src.Subject.Name))
            .ForMember(dest => dest.StudentName,
            x => x.MapFrom(src => src.Student == null ? null : $"{src.Student.FirstName} {src.Student.FirstName}"));

        CreateMap<GradeVm, Grade>();
            //.ForMember(dest => dest.Subject, x => x.Ignore())   not needed - there are no Subject or
            //.ForMember(dest => dest.Student, x => x.Ignore());  Student property in GradeVm

        CreateMap<Parent, ParentVm>()
            .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students));

        CreateMap<Teacher, TeachersGroupsVm>()  //Select in this place will return a collection of collections of groups
            .ForMember(dest => dest.Groups,     //SelectMany returns a single collection of all groups
            x => x.MapFrom(src => src.Subjects.SelectMany(s => s.SubjectGroups.Select(g => g.Group))));

        CreateMap<AddGradeToStudentVm, GradeVm>();
        CreateMap<AddGradeToStudentVm, Grade>();

        CreateMap<AddOrUpdateGroupVm, Group>();
        CreateMap<AddOrUpdateGroupVm, GroupVm>();
        CreateMap<GroupVm, AddOrUpdateGroupVm>();

        CreateMap<AttachDetachSubjectGroupVm, SubjectGroup>();

        CreateMap<RegisterNewUserVm, User>()
        .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
        .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Parent>()
        .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
        .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Student>()
        .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
        .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now));
        CreateMap<RegisterNewUserVm, Teacher>()
        .ForMember(dest => dest.UserName, y => y.MapFrom(src => src.Email))
        .ForMember(dest => dest.RegistrationDate, y => y.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.Title, y => y.MapFrom(src => src.TeacherTitles));

        CreateMap<StudentVm, AttachDetachStudentToGroupVm>()
            .ForMember(dest => dest.StudentId, y => y.MapFrom(src => src.Id));

    }
}
