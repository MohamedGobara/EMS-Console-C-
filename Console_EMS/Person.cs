using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_EMS
{
    internal class Person
    {
        public UInt32 id;
        public string username;
        public string Full_Name;
        public string password;



        public Person()
        {

        }

        public Person(UInt32 id, string username, string Full_name, string pass)
        {

            this.id = id;
            this.username = username;
            Full_Name = Full_name;
            password = pass;

        }

        public bool CheckPass(string user, string pass)
        {

            return this.username == user && this.password == pass ? true : false;

        }

    }
}
