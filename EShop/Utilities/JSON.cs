using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Utilities
{
    public class JSON
    {
        /// <summary>
        ///  تبدیل کلاس به رشته جیسون 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public async static Task<string> ToJson<T>(T Value)
        {
            var Result = JsonConvert.SerializeObject(Value, Formatting.Indented);

            return Result;
        }
        /// <summary>
        /// تبدیل به آبجکت
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public async static Task<T> ToObjet<T>(string Value)
        {
            var Result = JsonConvert.DeserializeObject<T>(Value, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return Result;
        }
        /// <summary>
        /// رفرسن رو قطع میکند
        /// کپی کننده کلاس بدون رفنسن
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
        public async static Task<T> ToClone<T>(T Value)
        {
            var Result = JsonConvert.SerializeObject(Value, Formatting.Indented);

            return await ToObjet<T>(Result);
        }
    }
}
