using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public virtual Group? Group { get; set; }
        public int? GroupId { get; set; }
        public virtual IList<Grade> Grades { get; set; } //here can be  pasted any collection which implement IList
        // but this collections can call methods only from IList interface
        public virtual Parent Parent { get; set; }
        public int? ParentId { get; set; }
        //public double AverageGrade
        //{
        //    get { 
        //        double sum = 0;
        //        foreach (var grade in Grades) { sum += (byte)grade.GradeValue;}
        //        return sum;
        //    }
        //}
        public double AverageGrade => Grades == null ? 0.0d : AverageGradePerSubject.Values.Average();
        public Dictionary<string, double> AverageGradePerSubject
        {
            get
            {
                Dictionary<string, double> avgDic = new Dictionary<string, double>();
                Dictionary<string, List<GradeScale>> dic = GradesPerSubject;
                if (dic != null)
                {
                    foreach (KeyValuePair<string, List<GradeScale>> subject in dic)
                    {
                        int sum = 0;
                        int count = 0;
                        foreach (GradeScale g in subject.Value)
                        {
                            sum += (byte)g;
                            count++;
                        }
                        avgDic.Add(subject.Key, sum / count);
                    }
                }
                return avgDic;
                //return GradesPerSubject.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Average(g => (byte)g));
            }
        }
        public Dictionary<string, List<GradeScale>> GradesPerSubject
        {
            get
            {
                Dictionary<string, List<GradeScale>> dic = new Dictionary<string, List<GradeScale>>();
                if (Grades.Count > 0)
                {
                    foreach (var g in Grades)
                    {
                        if ((dic.ContainsKey(g.Subject.Name)))
                        {
                            dic[g.Subject.Name].Add(g.GradeValue);
                        }
                        else
                        {
                            List<GradeScale> l = new List<GradeScale>();
                            l.Add(g.GradeValue);
                            dic.Add(g.Subject.Name, l);
                        }
                    }
                }
                return dic; 
                //return Grades.GroupBy(g => g.Subject.Name).ToDictionary(g => g.Key, g => g.Select(x => x.GradeValue).ToList());
            }

        }
        public Student(string firstName = "", string lastName = "", int? groupId = null, int? parentId = null) : base(firstName, lastName)
        {
            GroupId = groupId;
            ParentId = parentId;
        }
    }
}
