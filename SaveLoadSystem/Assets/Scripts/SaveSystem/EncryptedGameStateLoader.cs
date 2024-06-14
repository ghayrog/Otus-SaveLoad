using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace SaveSystem
{
    public sealed class EncryptedGameStateLoader : IGameStateLoader
    {
        private const string SAVE_FILE_NAME = "MyGameState.bin";
        private const string KEY = "private const string KEY = \"private const string KEY = \"private const string KEY = \"what\";\";\";";

        public void LoadState(GameState gameState)
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
                gameState.State = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
            {
                gameState.State = new Dictionary<string, string>();
            }
        }

        public void SaveState(GameState gameState)
        {
            var serializedState = StringCipher.Encrypt(JsonConvert.SerializeObject(gameState.State), KEY);
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
    }
}
