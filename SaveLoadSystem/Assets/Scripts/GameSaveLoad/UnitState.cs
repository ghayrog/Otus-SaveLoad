using System;

namespace GameSaveLoad
{
    [Serializable]
    public struct UnitState
    {
        public string type;
        public int hitPoints;
        public Vector3 position;
        public Vector3 rotation;
    }
}
