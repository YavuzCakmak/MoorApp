using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moor.Service.Extensions
{
    public static class SessionExtension
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (BinaryWriter writer = new(m))
                {
                    writer.Write(JsonConvert.SerializeObject(value));
                }

                session.Set(key, m.ToArray());
            }
        }
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            byte[] valueArray;
            session.TryGetValue(key, out valueArray);

            var value = Convert.ToBase64String(valueArray);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
