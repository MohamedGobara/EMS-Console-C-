using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_EMS
{
    internal class Soluation
    {

        public string sol;
        public int grade;
        public Student student;


        public Soluation()
        {
            
        }

        public Soluation(string sol , Student stu)
        {

            this.sol = sol;
            student = stu;
            grade = -1; 
            
        }
       


    }
}
