using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveSystem
{
    public sealed class PlayerPrefsGameStateLoader : IGameStateLoader
    {
        private const string GAME_STATE_KEY = "Lesson/GameState";

        public void LoadState(GameState gameState)
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var serializedState = PlayerPrefs.GetString(GAME_STATE_KEY);
                gameState.State = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
            }
            else
            {
                gameState.State = new Dictionary<string, string>();
            }
        }

        public void SaveState(GameState gameState)
        {
            var serializedState = JsonConvert.SerializeObject(gameState.State);
            PlayerPrefs.SetString(GAME_STATE_KEY, serializedState);
        }
    }
}
