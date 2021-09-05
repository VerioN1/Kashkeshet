using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KashkeshetCommon.Serializer
{
    public static class JsonSerialization
    {
        public static T Desrialize<T>(string jsonData)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static string Serialize<T>(T MessageToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(MessageToSerialize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
