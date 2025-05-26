using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{
    public class APIMSGSend
    {       
        public int SkipCount { get; set; }

        public int TakeCount { get; set; }

        public string? OrderBy { get; set; }

        public string? SelectedNames { get; set; }
        public string? SelectedIDs { get; set; }

        public string? OtherFilter { get; set; }

        public string? VisibleCircleIDs { get; set; }

        public string? UserId { get; set; }

        public string? DirectSQL { get; set; }
        public string? DirectCountSQL { get; set; }

        public bool? ForDeactived { get; set; } = false;

    }

    public class APIMSGReturn<T>
    {
       
        public int TotalCount { get; set; }
        public int StatusCode { get; set; }
        public int SkipCount { get; set; }
        public int TakeCount { get; set; }   
        
        public List<NanyItemBase<T>>? Result { get; set; }

    }

}
