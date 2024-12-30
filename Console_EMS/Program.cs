using Console_EMS;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using static System.Console;

public class Program
{


    static void BuildCourses(ref ArrayList CoursesDataList)
    {
        // Define the file path for course data.
        string FilePath = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Courses data\\Course data.txt";

        // Open the file for reading course data.
        using (StreamReader reader = File.OpenText(FilePath))
        {
            string line = reader.ReadLine(); // Read the first line of the file.
            int idCourseCounter = 1; // Initialize a counter for course IDs.

            // Check if the file contains any data.
            if (line != null)
            {
                // Split the line into course details (assumes format: name, code).
                string[] CourseData = line.Split(" ");
                // Create a Course object and add it to the list.
                Course c = new Course(CourseData[0] + " " + CourseData[1], CourseData[2], idCourseCounter++);
                CoursesDataList.Add(c);

                line = reader.ReadLine(); // Move to the next line (if any).
            }
        }
    }

    static void BuildDoctors(ref ArrayList DoctorsDataList, ref ArrayList CoursesDataList)
    {

        //public Doctor(UInt32 id, string username, string Full_name, string pass) 
        string path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Doctors data\\Names.txt";
        using (StreamReader reader = File.OpenText(path))
        {
            string line = reader.ReadLine();
            UInt32 DoctorIdCounter = 1;
            while (line != null)
            {
                string[] data = line.Split(" ");
                Doctor D = new Doctor(DoctorIdCounter++, data[0], data[1], data[3]);
                DoctorsDataList.Add(D);
                line = reader.ReadLine();
            }
        }

        // Course
        path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Doctors data\\Courses.txt";

        UInt32 doctor_index = 1;
        using (StreamReader reader = File.OpenText(path))
        {

            string line = reader.ReadLine();



            while (line != null)
            {

                if (line == "0")
                {

                    doctor_index++;
                    line = reader.ReadLine();
                    continue;
                }

                else
                {

                    foreach (Doctor doc in DoctorsDataList)
                    {

                        if (doc.id == doctor_index)
                        {

                            string[] AsssginedCourses = line.Split(" ");

                            for (int i = 0; i < AsssginedCourses.Length; i++)
                            {
                                foreach (Course curCourse in CoursesDataList)
                                {
                                    if (curCourse.code == AsssginedCourses[i]) // Matches course code with the available courses.
                                    {
                                        doc.AddCourse(curCourse); // Adds the course to the doctor.
                                        curCourse.AddDoctor(doc); // Adds the doctor to the course.
                                        break; // Move to the next course code.
                                    }
                                }
                            }
                            break; // Move to the next doctor.

                        }

                    }

                }
                line = reader.ReadLine();
                doctor_index++;
            }

        }

    }

    static void BuildStudents(ref ArrayList StudentsDataList, ref ArrayList CoursesDataList)
    {

        /* public Student(UInt32 id, string username, string fullName, string pass) */

        string path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Students data\\Names.txt";
        UInt32 st_id = 1;
        using (StreamReader reader = File.OpenText(path))
        {

            string line = reader.ReadLine();

            while (line != null)
            {

                string[] data = line.Split(" ");

                Student st = new Student(st_id++, data[0], data[1] + " " + data[2], data[3]);
                StudentsDataList.Add(st);
                line = reader.ReadLine();

            }

        }

        /*  Assign courses to students */

        path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Students data\\Courses.txt";

        using (StreamReader reader = File.OpenText(path))
        {


            string line = reader.ReadLine();

            int student_index_coounter = 1;
            while (line != null)
            {

                string[] data = line.Split(" ");

                for (int i = 0; i < data.Length; i++)
                {

                    foreach (Course course in CoursesDataList)
                    {
                        if (course.code == data[i])
                        {
                            course.AddStudent((Student)StudentsDataList[student_index_coounter]);
                            ((Student)(StudentsDataList[student_index_coounter])).AddCourse(course);
                            break;
                        }

                    }

                }
                student_index_coounter++;
                line = reader.ReadLine();
            }

        }
    }


    static void StoreDoctors(ArrayList doctorsDataList) {

        string path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Doctors data\\Names.txt";

        using (StreamWriter writer = new StreamWriter(path)) 
        {

            foreach (Doctor doctor in doctorsDataList)
            {
                writer.WriteLine(doctor.username + " " + doctor.Full_Name + " " + doctor.password); 

            }
        }

        path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Doctors data\\Courses.txt";

        using (StreamWriter writer = new StreamWriter(path))
        {

            foreach (Doctor doctor in doctorsDataList)
            {

                if (doctor.courses.Count>0) {
                    foreach (Course course in doctor.courses)
                    {
                        writer.Write(course.code);
                        writer.Write(" ");
                    }
                    writer.WriteLine(); 
                }

            }
        }
    }



    static void StoreStudents(ArrayList studentsDataList)
    {

        string path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Students data\\Names.txt";

        using (StreamWriter writer = new StreamWriter(path))
        {

            foreach (Student student in studentsDataList)
            {
                writer.WriteLine(student.username + " " + student.Full_Name + " " + student.password);

            }
        }

        path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Students data\\Courses.txt";

        using (StreamWriter writer = new StreamWriter(path))
        {

            foreach (Student student in studentsDataList)
            {
                
                if (student.RegisterdCourse.Count > 0)
                {
                    foreach (Course course in student.RegisterdCourse)
                    {
                        writer.Write(course.code);
                        writer.Write(" ");
                    }
                    writer.WriteLine();
                }

            }
        }
    }


    static void StoreCourses(ArrayList coursesDataList)
    {

        string path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Courses data\\Course data.txt";

        using (StreamWriter writer = new StreamWriter(path))
        {

            foreach (Course course in coursesDataList)
            {
                writer.WriteLine(course.name + " " +course.code);

            }
        }

        /* will complete in the future */
        //path = "D:\\Projects\\C#\\Console_EMS\\Console_EMS\\Data\\Courses data\\Course data.txt";

        //using (StreamWriter writer = new StreamWriter(path))
        //{

           
        //}
    }


    public static void Main()
    {


        ArrayList DoctorData = new ArrayList();
        ArrayList CoursesData = new ArrayList();
        ArrayList StudentsData = new ArrayList();
        ArrayList AssignmentsData = new ArrayList();

        BuildDoctors(ref DoctorData, ref CoursesData);
        BuildStudents(ref StudentsData, ref CoursesData);
        BuildCourses(ref CoursesData);
        int taughtby_c = 0;
        foreach (Course course in CoursesData)
        {
            course.AddDoctor((Doctor)DoctorData[taughtby_c++]);

        }

    StartPoint:
        Console.Clear();
        WriteLine("Welecome in Goabra Education system");
        WriteLine("Choose a number from 1/2/3");
        WriteLine("1.Login");
        WriteLine("2.SignUp");
        WriteLine("3.Shutdown system");

        char input_latter;
        char Signin_flag = '0';

        while (true)
        {

            input_latter = Convert.ToChar(ReadLine());

            switch (input_latter)
            {

                case '1':
                    Console.Clear();
                    Signin_flag = '0';
                    /* indicate for signin */
                    WriteLine("Welecome in Goabra Education system");
                    WriteLine(@"Login As a 
           1.Doctor
           2.Student
           * If you need to back to previous page press 'b'");
                    input_latter = Convert.ToChar(ReadLine());
                    break;
                case '2':
                    Signin_flag = '1';
                    Console.Clear();
                    WriteLine("Welecome in Goabra Education system");
                    /* indicate for signup */
                    WriteLine(@"SignUp As a 
            1.Doctor
            2.Student
            * If you need to back to previous page press 'b'");
                    input_latter = Convert.ToChar(ReadLine());

                    break;
                case '3':
                    Write("Shutdown");
                    for (int i = 0; i < 5; i++)
                    {
                        Write(".");
                        Thread.Sleep(100);

                    }
                    return;
                default:
                    WriteLine("Please enter a valid choice");
                    break;
            }

            if ((input_latter >= '1' && input_latter <= '3') || input_latter == 'b')
            {
                if (input_latter == 'b')
                {
                    goto StartPoint;
                }
                Console.Clear();
                WriteLine("Welecome in Goabra Education system");
                string username, password;
                bool correct_user_f = false;
                // User need to signin as a doctor 
                if (input_latter == '1' && Signin_flag == '0')
                {

                /*
                 * Check user and password
                 * view all permission if user is correct
                 */
                Doctor_signin:
                    WriteLine("Sign as a doctor\nPlease Enter");
                    Write("Username:");
                    username = ReadLine();
                    Write("password:");
                    password = ReadLine();

                    /* check user */
                    Doctor tempDocotr = new Doctor();
                    foreach (Doctor doc in DoctorData)
                    {

                        if (doc.CheckPass(username, password) == true)
                        {

                            correct_user_f = true;
                            tempDocotr = doc;
                            break;


                        }

                    }
                    /* passed */
                    if (correct_user_f)
                    {
                        WriteLine($"Welcome Dr.{tempDocotr.Full_Name}");


                        while (true)
                        {
                            Console.WriteLine("Please make a choice :");
                            Console.WriteLine("\t 1 - List courses");
                            Console.WriteLine("\t 2 - Create course");
                            Console.WriteLine("\t 3 - View Course");
                            Console.WriteLine("\t 4 - Log out");
                            input_latter = char.Parse(Console.ReadLine());
                            while (input_latter > '4' || input_latter < '1')
                            {
                                Console.Write("please enter a valid choice : ");
                                input_latter = char.Parse(Console.ReadLine());
                            }

                            if (input_latter == '1')
                            {
                                Console.Clear();
                                Console.WriteLine("All availablre courses are : \n");
                                foreach (Course c in CoursesData)
                                {
                                    Console.WriteLine($"Course {c.name}  with code {c.code} is taught by Dr: {c.taught_by.Full_Name} and has {c.students.Count} registered students");
                                }

                                Console.WriteLine("\n\n\t Press any key to go back");
                                Console.ReadLine();
                            }
                            else if (input_latter == '2')
                            {
                                Course NEWCOURSE = new Course();
                                Console.Write("Course name ? (Two words only) ");
                                NEWCOURSE.name = Console.ReadLine();
                                Console.Write("Course code ? ");
                                NEWCOURSE.code = Console.ReadLine();
                                NEWCOURSE.id = CoursesData.Count + 1;
                                NEWCOURSE.taught_by = tempDocotr;
                                tempDocotr.AddCourse(NEWCOURSE);
                                CoursesData.Add(NEWCOURSE);
                                Console.WriteLine("The course is created successfully ");
                                Console.WriteLine("\n\t Press any key to go back");
                                StoreDoctors(DoctorData);
                                StoreCourses(CoursesData);
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else if (input_latter == '3')
                            {
                                Console.Clear();
                                if (!tempDocotr.ViewCourses())
                                {
                                    Console.WriteLine("You are not teaching any courses :( ");
                                    Console.ReadLine();
                                }
                                else
                                {

                                    WriteLine("*This featurs still under development");
                                    ReadLine();
                                }
                            }
                            else if (input_latter == '4')
                            {
                                Console.Write("Logging out");
                                for (int i = 0; i < 7; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(200);
                                }
                                goto StartPoint;
                                break;
                            }
                            Console.Clear();
                        }


                    }
                    /* wrong */
                    else
                    {
                        WriteLine("Incrorrect username or password, please try again.");
                        WriteLine("Try again?press 'y' for yes or 'n'for no");
                    DoctorLogin_try_agian:
                        input_latter = Convert.ToChar(ReadLine());
                        if (input_latter == 'y')
                        {
                            goto Doctor_signin;
                        }
                        else if (input_latter == 'n')
                        {
                            goto StartPoint;
                        }
                        else
                        {
                            WriteLine("Enter a valid choice");
                            goto DoctorLogin_try_agian;

                        }

                    }



                }

                // User need to signin as a student 
                else if (input_latter == '2' && Signin_flag == '0')
                {
                Student_signin:
                    WriteLine("Sign as a student\nPlease Enter");
                    Write("Username:");
                    username = ReadLine();
                    Write("password:");
                    password = ReadLine();
                    Student tempStudent = new Student();
                    foreach (Student student in StudentsData)
                    {
                        if (student.CheckPass(username, password))
                        {
                            correct_user_f = true;
                            tempStudent = student;
                            break;

                        }

                    }
                    /* passed */
                    if (correct_user_f)
                    {
                    StudentPassed:
                        Console.Clear();
                        WriteLine($"Welcome {tempStudent.Full_Name}");

                        while (true)
                        {


                            WriteLine("Please make a choice :");
                            WriteLine("\t 1 - Register in a Course");
                            WriteLine("\t 2 - List my courses");
                            WriteLine("\t 3 - View Course");
                            WriteLine("\t 4 - Grades report");
                            WriteLine("\t 5 - Log out");
                            input_latter = char.Parse(Console.ReadLine());


                            while (input_latter < '1' || input_latter > '5')
                            {

                                WriteLine("Please enter a valid choice");
                                input_latter = char.Parse(Console.ReadLine());
                            }
                            /* Register in a course */
                            if (input_latter == '1')
                            {

                                WriteLine("Enter course id or press 'b' for back");
                                string CourseIdTemp = "";
                                char search_f = '0';



                                while (search_f == '1' || CourseIdTemp != "b")
                                {
                                    CourseIdTemp = ReadLine();
                                    if (CourseIdTemp == "b")
                                    {
                                        goto StudentPassed;
                                    }
                                    foreach (Course c in CoursesData)
                                    {
                                        if (c.id == int.Parse(CourseIdTemp))
                                        {
                                            c.AddStudent(tempStudent);
                                            tempStudent.AddCourse(c);
                                            WriteLine($"You have just registerd in {c.name} course successfully");
                                            search_f = '1';
                                            StoreDoctors(DoctorData);
                                            StoreStudents(StudentsData);
                                            break;
                                        }


                                    }
                                    if (search_f == '0')
                                    {
                                        WriteLine("Invalid Course id");
                                        WriteLine("Enter course id or press 'b' for back");

                                    }
                                }




                            }


                            /* List my course */
                            else if (input_latter == '2')
                            {

                                tempStudent.viewCourses();

                            }
                            /* View course */
                            else if (input_latter == '3')
                            {



                                string CourseIdTemp = "";
                                char search_f = '0';
                                while (CourseIdTemp != "b")
                                {
                                    WriteLine("Enter course id or press 'b' for back:");
                                    CourseIdTemp = ReadLine();
                                    if (CourseIdTemp == "b")
                                    {
                                        goto StudentPassed;
                                    }

                                    if (tempStudent.viewCourse(UInt32.Parse(CourseIdTemp)) == false)
                                    {
                                        WriteLine("Invalid course id");

                                    }


                                }




                            }
                            /* Grade reports */
                            else if (input_latter == '4') { }
                            /* Log out */
                            else
                            {
                                Write("\t Logging out");

                                for (int i = 0; i < 7; i++)
                                {
                                    Write(".");
                                    Thread.Sleep(200);

                                }
                                goto StartPoint;

                            }

                        }


                    }
                    /* wrong */
                    else
                    {
                        WriteLine("Incrorrect username or password, please try again.");
                        WriteLine("Try again?press 'y' for yes or 'n'for no");
                    DoctorLogin_try_agian:
                        input_latter = Convert.ToChar(ReadLine());
                        if (input_latter == 'y')
                        {
                            goto Student_signin;
                        }
                        else if (input_latter == 'n')
                        {
                            goto StartPoint;
                        }
                        else
                        {
                            WriteLine("Enter a valid choice");
                            goto DoctorLogin_try_agian;

                        }

                    }



                }

                // User need to sign up as a doctor 
                else if (input_latter == '1' && Signin_flag == '1')
                {
                    Doctor newDoctor = new Doctor();
                    string temp_buffer;
                    WriteLine("Sign up as a doctor");
                    Write("username:");
                    temp_buffer = ReadLine();
                    newDoctor.username = temp_buffer;
                    Write("Full Name:");
                    temp_buffer = ReadLine();
                    newDoctor.Full_Name = temp_buffer;
                    Write("Password:");
                    temp_buffer = ReadLine();
                    newDoctor.password = temp_buffer;
                    newDoctor.id = (UInt32)(DoctorData.Count) + 1;
                    DoctorData.Add(newDoctor);

                    WriteLine("Account is created successfully");
                    WriteLine("Press any key to come back for home page");
                    StoreDoctors(DoctorData);

                    ReadLine();
                    goto StartPoint;



                }
                // User need to signup as a student 
                else if (input_latter == '2' && Signin_flag == '1')
                {
                    Student newStudent = new Student();
                    string temp_buffer;
                    WriteLine("Sign up as a student");
                    Write("username:");
                    temp_buffer = ReadLine();
                    newStudent.username = temp_buffer;
                    Write("Full Name:");
                    temp_buffer = ReadLine();
                    newStudent.Full_Name = temp_buffer;
                    Write("Password:");
                    temp_buffer = ReadLine();
                    newStudent.password = temp_buffer;
                    newStudent.id = (UInt32)(StudentsData.Count) + 1;
                    StudentsData.Add(newStudent);
                    StoreStudents(StudentsData);
                    WriteLine("Account is created successfully");
                    WriteLine("Press any key to come back for home page");
                    ReadLine();
                    goto StartPoint;

                }

            }

        }

    }

}