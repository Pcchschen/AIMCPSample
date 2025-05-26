using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
 
    public class EntityFund : EntityBase
    {
        public EntityFund()
        {

        }

        public string FundName { get; set; }

        public string ShortName { get; set; }

        public string LegalName { get; set; }

        public string FundStatus { get; set; }

        public DateTime FirstClosingDate { get; set; }
        
    }

}
