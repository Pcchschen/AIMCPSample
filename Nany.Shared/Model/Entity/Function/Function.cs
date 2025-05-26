using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared.Model.Entity.Function
{


    public class RecordIDInfo
    {        
        public string ID { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;
    }

    public class EntryInfo: RecordIDInfo
    {       
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// 字段定义
    /// </summary>
    public class Field: RecordIDInfo
    {       
        public string Type { get; set; } = string.Empty; // 如 "string", "int", "bool" 等
        public object? Value { get; set; }
    }

    /// <summary>
    /// 模块定义
    /// </summary>
    public class Module : RecordIDInfo   
    {        
        public List<Field> Fields { get; set; } = new();
    }

    /// <summary>
    /// 功能定义
    /// </summary>
    public class Function : EntryInfo
    {
        public string FunctionID { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Function Name is required")]
        public string FunctionName { get; set; } = string.Empty;

        /// <summary>
        /// 基本资料
        /// </summary>
        public List<Field> BasicInfo { get; set; } = new();

        /// <summary>
        /// 其他资料
        /// </summary>
        public List<Module> OtherInfo { get; set; } = new();
    }
}
