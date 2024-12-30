using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Console_EMS
{
    internal class Student : Person
    {

        public ArrayList RegisterdCourse = new ArrayList();

        public Student()
        {

        }

        public Student(UInt32 id, string username, string fullName, string pass) : base(id, username, fullName, pass)
        {

        }

        public void AddCourse(Course c)
        {

            RegisterdCourse.Add(c);

        }

        public void viewCourses()
        {
            foreach (Course c in RegisterdCourse)
            {
                Console.WriteLine("ID : " + c.id + " - Course " + c.name + "  With code : " + c.code);
            }
        }

        public bool viewCourse(UInt32 id)
        {

            foreach (Course c in RegisterdCourse)
            {
                if (id == c.id)
                {

                    WriteLine($"course name:{c.name}");
                    WriteLine($"course Code:{c.code}");
                    WriteLine($"Taught by Dr:{c.taught_by}");
                    WriteLine($"This Course has:{c.assignments.Count}");

                }

                foreach (Assignment ass in c.assignments)
                {
                    Console.Write("Assignments " + ass.id + " - ");
                    bool f = false;
                    foreach (Soluation Sol in ass.soluations)
                    {
                        if (Sol.student == this)
                        {
                            Console.Write("Submitted - ");
                            if (Sol.grade == -1)
                            {
                                Console.Write("Under Review...");
                            }
                            else
                            {
                                Console.Write(Sol.grade + " / 100");
                            }
                            f = true;
                            Console.WriteLine();
                            break;
                        }
                    }

                    if (!f)
                    {
                        Console.WriteLine("Not submitted yet.");
                    }
                }
                return true;
            }
            return false;
        }

        public void report()
        {

            foreach (Course c in RegisterdCourse)
            { 
            
            
            
                WriteLine($"Course ( {c.name} ) With code ( {c.code} ) hase {c.assignments.Count} Assignments ");

                int sum = 0, solua_count = 0;

                foreach (Assignment ass in c.assignments)
                {
                   
                    WriteLine($"Assignment id:{ass.id}");

                    bool subimtted_f =false;

                    foreach (Soluation sol in ass.soluations)
                    {
                        if (sol.student==this) {


                            Write("Submitted- ");
                            if (sol.grade == -1)
                            {

                                WriteLine("Under rewiew...");
                            }
                            else {

                                WriteLine($"Grade:{sol.grade}/100");
                                sum += sol.grade;
                                solua_count++;
                            }
                            subimtted_f = true;
                            break; 
                        }

                    }
                    if (subimtted_f==false) {

                        WriteLine("Not submitted yet."); 
                    }

                }

                WriteLine($"Total grade in {c.name} course:{(double)((double)sum / ((double)solua_count * 100) * 100)}%");
                sum = 0;
                solua_count = 0;
            }


        }

        public Course unRegister(UInt32 id)
        {
            Course unRegCoirse = new Course();
            foreach (Course c in RegisterdCourse)
            {
                if (c.id == id)
                {
                    unRegCoirse = c;
                    RegisterdCourse.Remove(c);
                    break;
                }
            }
            return unRegCoirse;
        }

        public bool subSol(string sol, int id, int i)
        {
            foreach (Course c in RegisterdCourse)
            {
                if (c.id == id)
                {
                    foreach (Assignment ass in c.assignments)
                    {
                        if (ass.id == i)
                        {
                            Soluation s = new Soluation(sol, this);
                            int choice;
                            foreach (Soluation SOL in ass.soluations)
                            {
                                if (SOL.student == this)
                                {
                                    Console.WriteLine("You actually have submitted a solution to this assignment ");
                                    Console.WriteLine("Do you want to replace it ? ");
                                    Console.WriteLine("1 - YES");
                                    Console.WriteLine("2 - NO");
                                    choice = int.Parse(Console.ReadLine());
                                    while (choice != 1 && choice != 2)
                                    {
                                        Console.Write("Please enter a valid choice : ");
                                        choice = int.Parse(Console.ReadLine());
                                    }
                                    if (choice == 1)
                                    {
                                        ass.soluations.Remove(SOL);
                                        break;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                            }
                            ass.soluations.Add(s);
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }



}
