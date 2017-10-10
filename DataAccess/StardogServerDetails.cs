using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class StardogServerDetails
    {
        public StardogServerDetails()
        { }

        public StardogServerDetails(
         string _URI,
         string _KnowledgeBase,
         string _UserName,
         string _Password)
        {
            URI = _URI;
            KnowledgeBase = _KnowledgeBase;
            UserName = _UserName;
            Password = _Password;
        }

        private string uri;
        public string URI
        {
            get
            {
                if (string.IsNullOrEmpty(uri)) 
                    return string.Empty;
                if (uri.Contains("//"))
                    return uri;
                else
                    return @"http://" + uri;
            }
            set
            {
                uri = value;
            }
        }
        public string KnowledgeBase;
        public string UserName;
        public string Password;//Yes. In plain text

        public bool Valid(out List<string> ErrorControl)
        {
            ErrorControl = new List<string>();
            if (URI.Length < 8)
                ErrorControl.Add("txtServer");
            Uri notUsed = null;
            if (!Uri.TryCreate(URI, UriKind.RelativeOrAbsolute, out notUsed))
                ErrorControl.Add("txtServer");
            if (string.IsNullOrEmpty(KnowledgeBase))
                ErrorControl.Add("txtOnt");
            if (string.IsNullOrEmpty(UserName))
                ErrorControl.Add("txtUser");
            if (string.IsNullOrEmpty(Password))
                ErrorControl.Add("txtPassword");
            if (ErrorControl.Count == 0)
                return true;
            else
                return false;
        }
    }
}
