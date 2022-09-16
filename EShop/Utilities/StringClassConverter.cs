using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Utilities
{

        public class StringClassConverter
        {
            /// <summary>
            /// تبدیل ی و ک و فاصله عربی به فارسی
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public async static Task<string> GetPersianWord(string text)
            {
                if (String.IsNullOrEmpty(text))
                {
                    return text;
                }
                //text = text.Replace("ي", "ی").Replace("ك", "ک");
                text = text.Replace((char)160, (char)32).Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
                return text;
            }
            /// <summary>
            /// گرفتن شماره انگلیسی از رشته
            /// </summary>
            /// <param name="persianNumber"></param>
            /// <returns></returns>
            public async static Task<string> GetEnglishNumber(string persianNumber)
            {
                // اعداد فارسی
                string englishNumber = persianNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
                // اعداد عربی
                englishNumber = persianNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("٤", "4").Replace("٥", "5").Replace("٦", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
                // ثبت شده ها
                englishNumber = persianNumber.Replace("۴", "4").Replace("۵", "5").Replace("۶", "6");
                return englishNumber;
            }
            /// <summary>
            /// گرفتن یک رشته رندوم
            /// </summary>
            /// <param name="size"></param>
            /// <param name="lowerCase"></param>
            /// <returns></returns>
            public async static Task<string> RandomString(int size, bool lowerCase = false)
            {
                Random _random = new Random();
                var builder = new StringBuilder(size);

                // Unicode/ASCII Letters are divided into two blocks
                // (Letters 65–90 / 97–122):   
                // The first group containing the uppercase letters and
                // the second group containing the lowercase.  

                // char is a single Unicode character  
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // A...Z or a..z: length = 26  

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }

            /// <summary>
            /// چک کردن صحیح بودن شماره موبایل
            /// </summary>
            /// <param name="Mobile"></param>
            /// <returns></returns>
            public async static Task<bool> CheckMobileNumber(string Mobile)
            {
                if (String.IsNullOrEmpty(Mobile))
                    return false;
                if (Mobile.Length != 11 || !Mobile.StartsWith("09"))
                    return false;
                if (!(Mobile.All(char.IsDigit)))// note ; Empty String return True
                    return false;
                return true;
            }

            /// <summary>
            /// چک کردن آدرس ایمیل
            /// </summary>
            /// <param name="Email"></param>
            /// <returns></returns>
            public async static Task<bool> CheckEmailAddress(string Email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Email);
                    return addr.Address == Email;
                }
                catch
                {
                    return false;
                }
            }

        }
    
}
