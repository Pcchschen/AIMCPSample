using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
 
    public class EntityMember : EntityBase
    {
        public EntityMember()
        {

        }
        public string? UserLoginREFId { get; set; }


        public string? PhotoURL { get; set; }

        public string? MemberId { get; set; }
        

        public string? MemberName { get; set; }

        public string? MemberName_English { get; set; }


        public string? MobileNumber { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime? JoinDate { get; set; }=System.DateTime.Now;

      
        public string? UserLogin_ID { get; set; }


        public string? UserLogin_Password { get; set; }

        public string? IsAdmin { get; set; }


    }


    public class Address
    {
        /// <summary>
        ///  室﹕
        /// </summary>
        public string? Room { get; set; }
        /// <summary>
        /// 樓﹕
        /// </summary>
        public string? Floor { get; set; }

        /// <summary>
        /// 座﹕
        /// </summary>
        public string? Block { get; set; }

        /// <summary>
        /// 大廈﹕
        /// </summary>
        public string? Tower { get; set; }

        /// <summary>
        /// 屋邨﹕
        /// </summary>
        public string? Estate { get; set; }

        /// <summary>
        /// 街道﹕
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// 街號﹕
        /// </summary>
        public string? StreetGroup { get; set; }

        /// <summary>
        /// 區域﹕
        /// </summary>

        public string? Destric { get; set; }

        /// <summary>
        /// 地區﹕香港/九龍/新界/離島
        /// </summary>

        public string? City { get; set; }

    }

}
