using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;

namespace SaveSystem
{


    public sealed class FileSystemGameRepository : IGameRepository
    {
        private const string SAVE_FILE_NAME = "MyGameState.bin";

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

                var serializedState = new string(dataArray);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
            {
                this.gameState = new Dictionary<string, string>();
            }
        }

        public void SaveState()
        {
            var serializedState = JsonConvert.SerializeObject(this.gameState);
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
