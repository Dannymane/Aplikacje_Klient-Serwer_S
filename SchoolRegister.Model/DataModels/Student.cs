using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public List<Grade> Grades { get; set; }
        public Parent Parent { get; set; }
        public int ParentId { get; set; }
    //    private double _averageGrade;    Do i need this field?
        public double AverageGrade
        {
            get { 
                double sum = 0;
                foreach (var grade in Grades) { sum += (byte)grade.GradeValue;}
                return sum;
            }
        }

        public Dictionary<string, double> AverageGradePerSubject
        {
            get
            {
                Dictionary<string, double> avgDic = new Dictionary<string, double>();
                Dictionary<string, List<GradeScale>> dic = GradesPerSubject;
                foreach (KeyValuePair<string, List<GradeScale>> subject in dic)
                {
                    int sum = 0;
                    int count = 0;
                    foreach(GradeScale g in subject.Value)
                    {
                        sum += (byte)g;
                        count++;
                    }
                    avgDic.Add(subject.Key, sum/count);
                }
                return avgDic;
            }
        }
        public Dictionary<string, List<GradeScale>> GradesPerSubject
        {
            get
            {
                Dictionary<string, List<GradeScale>> dic = new Dictionary<string, List<GradeScale>>();
                foreach (var g in Grades) 
                {   
                    if (!(dic.ContainsKey(g.Subject.Name))){
                        List<GradeScale> l = new List<GradeScale>();
                        l.Add(g.GradeValue);
                        dic.Add(g.Subject.Name, l);
                    }
                    else
                    {
                        dic[g.Subject.Name].Add(g.GradeValue);
                    }
                }
                //return dic;
                return Grades.GroupBy(g => g.Subject.Name).ToDictionary(g => g.Key, g => g.Select(x => x.GradeValue).ToList());
            }
        }


    }
}
