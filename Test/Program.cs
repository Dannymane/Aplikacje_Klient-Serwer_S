using Microsoft.AspNetCore.Identity;
using System;

class Program
{
    internal class User : IdentityUser<int>
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime registrationDate { get; set; }
        public User(string? firstName_ = null, string? lastName_ = null)
        {
            firstName = firstName_;
            lastName = lastName_;
            registrationDate = DateTime.Now;
        }
    }
    internal class Parent : User
    {
        public string ok;
        public Parent(string? firstName_, string? lastName_, string ok_) : base()
        {
            ok = ok_;
        }
    }
    public enum GradeScale
    {
        NDST = 2,
        DST = 3,
        DB = 4,
        BDB = 5
    }
    static void Main()
    {
        Parent p = new Parent("Daniel", "lastname", "ok");
        Console.WriteLine(p.registrationDate);
        Console.WriteLine(p.firstName);
        Console.WriteLine(p.lastName);
        Console.WriteLine(p.ok);

        GradeScale GradeValue = GradeScale.DB;
        Console.WriteLine((byte)GradeValue);


        List<int> l = new List<int>() { 4, 10, 12 };
        Console.WriteLine(l.Average());
        
    }
}