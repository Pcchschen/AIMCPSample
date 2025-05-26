using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared.Model
{
    public class CustomFieldInfo
    {
        public string? CollectionType { get; set; }

        public bool? OfficalName { get; set; } = false;
        public bool? Body { get; set; } = false;

        public bool? ShowAtMainPage { get; set; } = false;

        public bool? SelectOption { get; set; } = false;


        public int? Index { get; set; } = 99;

        public string? ID { get; set; } = Guid.NewGuid().ToString();

        public string? FieldName { get; set; }

        public string? YourAnswer { get; set; }

        public string? FieldValue { get; set; }

        public DateTime? FieldDatetimeValue { get; set; }

        public FieldTypes? fieldType { get; set; } = FieldTypes.ShortString;

        public List<string> SelectOptions
        {

            get
            {

                var list = new List<string>();

                if (!string.IsNullOrEmpty(StrSelectOptions))
                {
                    list = StrSelectOptions.Split(',').ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// For Select, "a,b,c"
        /// </summary>
        public string? StrSelectOptions { get; set; }

        /// <summary>
        /// For LinkFuncitonSource
        /// </summary>
        public string? FuncitonSource { get; set; }

        public string? FuncitonFilterSource { get; set; }


        public string? FilterName { get; set; }

        public string? FilterFieldName { get; set; }

        public List<string>? FunctionFilterSourceValue { get; set; } = new List<string>();


        public List<string>? FunctionSourceIds { get; set; } = new List<string>();


        public List<string>? FunctionSourceValue { get; set; } = new List<string>();


        public string? FieldLevel { get; set; }

        public string? FieldDescription { get; set; }

        public string? FieldSubType { get; set; }

        public bool? Activated { get; set; } = true;

        public decimal? NumberValue { get; set; } = 1M;
        public decimal? AmountValue
        {
            get
            {
                decimal? decimalValue = null;
                if (this.fieldType == FieldTypes.Number && !string.IsNullOrEmpty(this.FieldValue))
                {
                    decimal price = Convert.ToDecimal(this.FieldValue??"0");

                    decimalValue = price * NumberValue;
                }


                return decimalValue;
            }

        }
    }
}

public enum FieldTypes
{
    ShortString,
    Text,
    Number,
    BoolYN,
    Select,
    Date,
    Time,
    DateTime,
    /// <summary>
    /// 目标模块
    /// </summary>
    LinkFuncitonSource,

    /// <summary>
    /// 目标模块列表
    /// </summary>
    ListFunctionSource,

    /// <summary>
    /// 目标模块列表中的列表
    /// </summary>
    ListFunctionSourceWithFilter,
    Int,

}
