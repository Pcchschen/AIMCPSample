
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
    public class EntityRoleFunctionPermission : EntityBase
    {
        public string Role_ID { get; set; }=Guid.NewGuid().ToString();
        public string? Function_ID { get; set; }
        public En_FunctionTypes FunctionType { get; set; }
        public string? FunctionName { get; set; }
        public bool Read { get; set; }
        public bool Add { get; set; }
        public bool Update { get; set; }

        public bool Delete { get; set; }
        public bool Approve { get; set; }

        public bool Permission_1 { get; set; }
        public bool Permission_2 { get; set; }
        public bool Permission_3 { get; set; }
        public bool Permission_4 { get; set; }
        public bool Permission_5 { get; set; }
        public bool Permission_6 { get; set; }

        public string ReadDes => Read ? "Y" : "X";
        public string AddDes => Add ? "Y" : "X";
        public string UpdateDes => Update ? "Y" : "X";
        public string DeleteDes => Delete ? "Y" : "X";
        public string ApproveDes => Approve ? "Y" : "X";
    }

 
}
