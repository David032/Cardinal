using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Field
{
    //Used in the field to mark where an Interest Node can be spawned
    public class FieldNode : MonoBehaviour
    {
        public FieldNodeSize Size = FieldNodeSize.Medium;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Shapes.Draw.Cube((int)Size);
            Debug.DrawRay
                (transform.position, transform.forward * (int)Size / 2,Color.red);
            Debug.DrawRay
                (transform.position, transform.right * (int)Size / 2, Color.red);
        }
#endif
    }
}

