using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_EMS
{
    internal class Doctor:  Person
    {

        public ArrayList courses = new ArrayList();


        public Doctor()
        {

        }
        public Doctor(UInt32 id, string username, string Full_name, string pass) : base(id , username, Full_name, pass) {
       
        }

        public void AddCourse(Course cs) {


            courses.Add(cs); 
        
        }


        public bool ViewCourses() {

            if (this.courses.Count==0) {

                return false; 

            
            }
            foreach (Course cs in courses)
            {

                Console.WriteLine($"ID - {cs.id} Course {cs.name}");
            }

            return true;
        
        }


        public void ViewCourse(int id ) {
        
        
        }

    }
}
