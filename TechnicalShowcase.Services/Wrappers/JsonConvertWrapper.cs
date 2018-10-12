using Newtonsoft.Json;

namespace TechnicalShowcase.Services.Wrappers
{
    public interface IJsonConvertWrapper
    {
        T Deserialize<T>(string value);
    }

    public class JsonConvertWrapper : IJsonConvertWrapper
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
