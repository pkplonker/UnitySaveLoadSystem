using System;
using UnityEngine;

namespace Save
{
    
    /// <summary>
    /// Class <c>SerializableVector</c> container to serialize vector data. Constructed with Vector3 or X, Y, Z floats
    /// </summary>
    
    [Serializable]
    public class SerializableVector
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        public SerializableVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 GetVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
