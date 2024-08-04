using System.IO;
using Newtonsoft.Json;

namespace Services.JsonSave
{
    public class JsonSaveService
    {
        private readonly string _saveFilePath;

        public JsonSaveService(string saveFilePath)
        {
            _saveFilePath = saveFilePath;
        }

        public void Save<T>(T dataToSave)
        {
            var json = JsonConvert.SerializeObject(dataToSave);
            File.WriteAllText(_saveFilePath, json);
        }

        public bool TryToLoad<T>(out T dataToLoad)
        {
            if (File.Exists(_saveFilePath))
            {
                var textJson = File.ReadAllText(_saveFilePath);
                dataToLoad = JsonConvert.DeserializeObject<T>(textJson);
                return true;
            }

            dataToLoad = default;
            return false;
        }
    }
}