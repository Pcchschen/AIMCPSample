using System.ComponentModel;
using System.Reflection;
using System.Text.Json;

namespace Nany.Shared
{
    public static class GlobalcsShared
    { 
        public static string GetDescriptionText(this Enum source)
        {
            FieldInfo? fi = source.GetType().GetField(source.ToString());
            if (fi == null) return String.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static string GetEnumDescriptionText(this object obj)
        {
            Enum source = (Enum)obj;
            FieldInfo? fi = source.GetType().GetField(source.ToString());
            if (fi == null) return String.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static double GetDouble(this decimal? obj)
        {            
            var flV = Convert.ToDouble(obj);
            return flV;
        }


        public static string? ToStringN2(this decimal? obj)
        {
            return ToStringNx(obj, 2);            
        }

        public static string? ToStringN2(this decimal obj)
        {
            return ToStringNx(obj, 2);
        }


        public static string? ToStringN0(this decimal? obj)
        {
            return ToStringNx(obj,0);
        }

        private static string? ToStringNx(this decimal? obj, int length)
        {
            return obj?.ToString($"N{length}");
        }

    }

    public static class ExtandClass
    {
        //public static async Task<T?> GetObjectFromResponse<T>(this HttpResponseMessage responseMessage)
        //{
        //    var res = await responseMessage.Content.ReadFromJsonAsync<T>();

        //    return res;
        //}

        public static bool IsUnauthorized(this HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return true;
            }

            return false;
        }

        public static long TillNowSeconds(this DateTime startDatetime)
        {
            return Convert.ToInt64(DateTime.Now.Subtract(startDatetime).TotalSeconds);
        }

        public static long ToMinutes(this long seconds)
        {
            return seconds / 60;
        }


        public static string GetEnumDescriptionText(this object obj)
        {
            Enum source = (Enum)obj;
            FieldInfo? fi = source.GetType().GetField(source.ToString());
            if (fi == null) return String.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute), false);
            if (attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static string? ToStandardString(this DateTime? value, string formart = "yyyy-MM-dd")
        {
            return value?.ToString(formart);
        }


        public static long ToHours(this long seconds)
        {
            return seconds / 60 / 60;
        }

        public static string? SubStringByLength(this string? iniStr, int length, string? addStr = "...")
        {
            string? result = null;

            if (iniStr != null)
            {
                if (iniStr.Length > length)
                {
                    result = $"{iniStr.Substring(0, length)}{addStr}";

                }
                else
                {
                    result = iniStr;
                }
            }

            return result;

        }

        public static string Serialize(this Object obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false
            };
            var jsonString = JsonSerializer.Serialize(obj, options);

            return jsonString;
        }

        public static T? Deserialize<T>(this string v)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false
            };

            T? result = JsonSerializer.Deserialize<T>(v, options);


            return result;
        }

        public static T? ConvertTo<S, T>(this S obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);


            T? result = JsonSerializer.Deserialize<T>(jsonString, options);


            return result;
        }

        public static decimal GetRate(int rateC, int allC)
        {
            var rate = (decimal)rateC / (decimal)allC;

            return rate;
        }

        public static bool IsBigger(this decimal? rate, int rateC, int allC)
        {
            var t = GetRate(rateC, allC)-0.01m;

            return rate > t;
        }

        public static bool IsBigger(this decimal rate, int rateC, int allC)
        {
            var t = GetRate(rateC, allC)-0.01m;

            return rate > t;
        }

        public static bool IsSmaller(this decimal? rate, int rateC, int allC)
        {
            var t = GetRate(rateC, allC)+0.01m;

            return rate < t;
        }



        public static T? Clone<T>(this T obj)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(obj, options);


                T? result = JsonSerializer.Deserialize<T>(jsonString, options);


                return result;
            }

            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return default(T);
        }

        public static S? Clone<S>(this Object obj)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(obj, options);


                S? result = JsonSerializer.Deserialize<S>(jsonString, options);


                return result;
            }

            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return default(S);
        }

    }

}




