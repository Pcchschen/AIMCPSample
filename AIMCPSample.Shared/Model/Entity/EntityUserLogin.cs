using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
    /// <summary>
    /// MainInfo for userLogin
    /// </summary>
    public class EntityUserLogin : EntityMember
    {
        public int? UserLevel { get; set; } = 0;  

        public string? LoginID { get; set; }
        public string? LoginPassword { get; set; }

        public string? LoginPasswordConfirm { get; set; }
        public string? LoginPasswordHash { get; set; }

        public string? AuthCode { get; set; }

        public string? PermissionGroupID { get;set; }=String.Empty;

        public string? UserDisplayName { get; set; }

        public string? TableHeight { get; set; } = "60%";

        public string? background_color { get; set; } = "azure";

        public int? PopupWindowWidth { get; set; } = 900;

        public bool? TabMode { get; set; } = false;

        public bool? DepartmentLeader { get; set; }

        /// <summary>
        /// FK to MF_USER.ID
        /// </summary>
        public string? User_ID { get; set; }

        public string? message { get; set; }
        public string? jwtBearer { get; set; }
        public DateTime? LoginDatetime { get; set; }

        public bool? success { get; set; }

        public List<KeyValuePair<string, string>>? VisibleCircles { get; set; } = new List<KeyValuePair<string, string>>();

        
        public Student? StudentInfo { get; set; } = new();
    }

    public class EntityCurrentUser : EntityUserLogin
    {
       
    }



    public class LoginModel
    {
        [Required(ErrorMessage = "LoginID is required.")]
        [EmailAddress(ErrorMessage = "LoginID address is not valid.")]
        public string? LoginID { get; set; } 

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? password { get; set; }


        public string? AuthCode { get; set; }
        
    }
    public class RegModel : LoginModel
    {
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password and confirm password do not match.")]
        public string? confirmpwd { get; set; }
    }

    public class Student
    {
        public int YearOfBirth { get; set; }

        public string? ClassName { get; set; }

        public string? Description { get; set; }

    }

 
}
