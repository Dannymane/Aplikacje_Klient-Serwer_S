using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;

class Program
{

    internal class Test
    {
        public string Name { get; set; }
        public Test(string name = "no")
        {
            Name = name;
        }
    }
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
        //Parent p = new Parent("Daniel", "lastname", "ok");
        //Console.WriteLine(p.registrationDate);
        //Console.WriteLine(p.firstName);
        //Console.WriteLine(p.lastName);
        //Console.WriteLine(p.ok);

        //GradeScale GradeValue = GradeScale.DB;
        //Console.WriteLine((byte)GradeValue);


        //List<int> l = new List<int>() { 4, 10, 12 };
        //Console.WriteLine(l.Average());
        //int sum = 0;
        //List<int> l2 = new List<int>() { 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1,
        //    1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4,
        //    10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12,
        //    1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1,
        //    1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4,
        //    10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12,
        //    1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1, 4, 10, 12, 1, 1, 1,
        //4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,
        //4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,
        //4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,
        //4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,
        //4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,4, 10, 12, 1, 1, 1,};
        

        //Parallel.ForEach(l2, numbr =>
        //{
        //    sum += 1; //same rules as with parallelism in c++ (bad result)
        //});

        //int sum2 = l2.Sum();
        //Console.WriteLine($"{sum} {sum2}");
        Console.WriteLine(typeof(Program).Assembly);
        Test t = new Test() { Name = "test" };
        Console.WriteLine(t.Name);


    }

}