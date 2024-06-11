using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveSystem
{
    public sealed class EncryptedGameRepository : IGameRepository
    {
        private const string SAVE_FILE_NAME = "MyGameState.bin";
        private const string KEY = "private const string KEY = \"private const string KEY = \"private const string KEY = \"what\";\";\";";

        private Dictionary<string, string> gameState = new();

        public void LoadState()
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, SAVE_FILE_NAME);
            if (File.Exists(fullPath))
            {
                FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fileStream);
                char[] dataArray = reader.ReadChars((int)fileStream.Length);
                reader.Close();
                fileStream.Close();

                var serializedState = StringCipher.Decrypt(new string(dataArray), KEY);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
            {
                this.gameState = new Dictionary<string, string>();
            }
        }

        public void SaveState()
        {
            var serializedState = StringCipher.Encrypt(JsonConvert.SerializeObject(this.gameState), KEY);
            char[] dataArray = serializedState.ToCharArray(0, serializedState.Length);
            string fullPath = Path.Combine(Application.streamingAssetsPath, SAVE_FILE_NAME);

            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(dataArray);
            writer.Close();
            fileStream.Close();

            Debug.Log($"Data saved to: {fullPath}");
        }

        public T GetData<T>()
        {
            var serializedData = this.gameState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value)
        {
            if (this.gameState.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            this.gameState[typeof(T).Name] = serializedData;
        }
    }
}
