
using Newtonsoft.Json;

namespace NewsLetterBoy.Repository.Test
{
    public class ObjectCloner
    {
        public static T Clone<T>(T obj)
        {
            string temp = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(temp);
        }
    }
}