using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationExcercise.Models
{
    public class Credential
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class ErrorMessages
    {
        public string loginErrorMessage { get; set; }
    }

    public class CredentialSet
    {
        public Credential validCredentials { get; set; }
        public Credential inValidCredentials { get; set; }
        public ErrorMessages errorMessages { get; set; }
    }
}
