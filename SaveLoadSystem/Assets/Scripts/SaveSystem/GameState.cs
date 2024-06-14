using System.Collections.Generic;

namespace SaveSystem
{
    public sealed class GameState
    {
        public Dictionary<string, string> State;

        public GameState()
        {
            State = new Dictionary<string, string>();
        }
    }
}
