using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
    public class EntityBase
    {
        public bool? Deleted { get; set; }
        public string? ID { get; set; } = Guid.NewGuid().ToString();

        public virtual En_FunctionTypes? FunctionType { get; set; } = En_FunctionTypes.NA;

        public string? ExtandContractId { get; set; }

        public int? Version { get; set; } = 1;

        public string? SystemVersion { get; set; }

        public string? BaseName { get; set; }
        public string? BaseDescription { get; set; }

        public DateTime? CreatedDatetime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDatetime { get; set; } = DateTime.Now;

        /// <summary>
        /// 员工ID，建立者ID
        /// </summary>
        public string? CreatedById { get; set; }

        /// <summary>
        /// 员工的组别与员工的姓名
        /// </summary>
        public string? CreatedByName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? UpdatedById { get; set; }
        public string? UpdatedByName { get; set; }

        public string? EntityID { get; set; }

        public virtual void ValidateCheck()
        {

        }

    }

    public class EntityAi : EntityBase
    {

    }
}
