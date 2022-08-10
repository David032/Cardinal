using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Events
{
    public class EventTrigger : BaseEvent
    {
        // Start is called before the first frame update
        void Start()
        {
            AssignElements();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                CreateEvent(ObjectType.Visual);
                Destroy(this.gameObject.GetComponent<BoxCollider>(), 5f);
            }
        }
    }
}
