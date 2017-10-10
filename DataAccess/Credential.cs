using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public struct Credential
    {
        public Credential(string user,string pass)
        {
            User = user;
            Pass = pass;
        }
        public string User;
        public string Pass;
    }
}
