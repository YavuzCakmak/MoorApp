using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Core.Extension.String
{
    public static class StringExtensions
    {
        public static string UppercaseFirstLetter(this string value)
        {
            if (value.Length > 0)
            {
                char[] array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);
                for (int i = 1; i < array.Length; i++)
                {
                    array[i] = char.ToLower(array[i]);
                }
                return new string(array);
            }
            return value;
        }
        public static string GetSpecialCharacter(this string value)
        {
            int length = 1;
            Random Random = new Random();
            const string chars = ".*!-_";
            IEnumerable<string> rndStr = Enumerable.Repeat(chars, length);
            value += new string(rndStr.Select(s => s[Random.Next(s.Length)]).ToArray());
            return value;
        }

        public static bool IsInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            Int32 tmp;
            return Int32.TryParse(value, out tmp);
        }
        public static bool IsInt64(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            Int64 tmp;
            return Int64.TryParse(value, out tmp);
        }
        public static bool IsDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            DateTime tmp;
            return DateTime.TryParse(value, out tmp);
        }

        public static bool IsBoolean(this string value)
        {
            var val = value.ToLower().Trim();

            if (val == "1" || val == "0") return true;

            if (val == "true" || val == "false")
                return true;

            return false;
        }

        public static string RemoveRepeatedWhiteSpace(this string value)
        {
            if (value.IsNull()) return string.Empty;

            string[] tmp = value.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (string word in tmp)
            {
                sb.Append(word.Replace("\r", "").Replace("\n", "").Replace("\t", "")/*.Replace("\\" , "")*/ + " ");
            }

            return sb.ToString().TrimEnd();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return !value.IsNullOrEmpty();
        }
        public static bool IsNullOrEmpty(this string text)
        {
            return text == null || text.Trim().Length == 0;
        }


        public static bool IsNotNullOrEmpty(this string text)
        {
            return !IsNullOrEmpty(text);
        }

        public static bool IsNotNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value)
        {
            return value.IsNull() || !value.Any();
        }

        public static bool IsNull(this object objectToCall)
        {
            return objectToCall == null || Convert.IsDBNull(objectToCall);
        }

        public static bool IsNotNull(this object objectToCall)
        {
            return !(objectToCall == null || Convert.IsDBNull(objectToCall));
        }
        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }

            decimal convert;
            return decimal.TryParse(value, out convert);
        }

        public static string HexString2B64String(this string input)
        {
            return System.Convert.ToBase64String(input.HexStringToHex());
        }

        public static byte[] HexStringToHex(this string inputHex)
        {
            try
            {
                var resultantArray = new byte[inputHex.Length / 2];
                for (var i = 0; i < resultantArray.Length; i++)
                {
                    resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
                }
                return resultantArray;
            }
            catch (Exception ex)
            {

                throw new Exception($"Şifreli atılan istek doğru formatta değildir. -> {ex.Message}");
            }
        }
    }
}
