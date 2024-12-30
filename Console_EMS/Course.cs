using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Console_EMS
{
    internal class Course
    {

        public string name;
        public string code;
        public int id;
        public Doctor taught_by;

        public ArrayList students = new ArrayList();
        public ArrayList assignments = new ArrayList();

        public Course()
        {
            
        }
        public Course(string name, string code, int id)
        {


            this.name = name;
            this.code = code;
            this.id = id;

            Assignment ass1 = new Assignment(1);
            Assignment ass2 = new Assignment(2);
            assignments.Add(ass1);
            assignments.Add(ass2);
        }

        public void AddDoctor(Doctor d)
        {
            this.taught_by = d;
        }
        public void AddStudent(Student S)
        {
            students.Add(S);
        }

        public void RemoveStudent(Student st) {


            students.Remove(st);
            foreach (Assignment ass in assignments)
            { 
                foreach (Soluation sol in ass.soluations )
                {

                    if (sol.student == st) {

                        ass.soluations.Remove(st); 
                    
                    }

                }

            }
        
        
        }


        public void view()
        {
            Console.WriteLine("ID : " + this.id + " - Course " + this.name + "  With code : " + this.code + " is taught by : " + this.taught_by.Full_Name);
        }

    }
}
