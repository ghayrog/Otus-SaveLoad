using UnityEngine;

namespace GameSaveLoad
{
    public static class Utility
    {
        public static Vector3 Vector3ToStruct(UnityEngine.Vector3 unityVector3)
        { 
            return new Vector3
            { 
                x = unityVector3.x,
                y = unityVector3.y,
                z = unityVector3.z,
            };
        }

        public static UnityEngine.Vector3 StructToVector3(Vector3 structVector3)
        {
            return new UnityEngine.Vector3
            (
                structVector3.x,
                structVector3.y,
                structVector3.z
            );
        }
    }
}
