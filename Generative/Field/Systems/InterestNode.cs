using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Field
{
    public class InterestNode : MonoBehaviour
    {
        public FieldNodeSize Size = FieldNodeSize.Medium;
        public bool DebugDrawBounds = false;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (DebugDrawBounds)
            {
                Shapes.Draw.Cube((int)Size);
            }
        }
#endif
    }
}

