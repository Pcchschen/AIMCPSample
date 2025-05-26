using Microsoft.JSInterop;
using Nany.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EN_PageStatus
{
    NotInited = -1,
    Initing = 0,
    Inited = 1,
}

public enum EN_PageActions
{
    ToShowUpdate,
    ToShowDelete,
    ToShowAdd,
    ToShowReadOnly,
    ToShowAll,
    ToShowOneInDetail
}

public struct SelectListItem
{
    public string Text { get; set; }
    public string Value { get; set; }

}


public class UploadResult
{
    public bool Uploaded { get; set; }
    public string StoredFileName { get; set; }
    public int ErrorCode { get; set; }
    public string FileName { get; set; }

     public List<EntityMember> Datatables { get; set; } = new ();
}
