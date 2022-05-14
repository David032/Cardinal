using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    public class Doorway : MonoBehaviour
    {
        public GameObject Door;
        public GameObject Wall;
        public GameObject roomPoint;
        public Heading Facing = Heading.North;

        public bool IsUsed = false;
        bool isSet = false;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void DoorCheck() 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * -10, out hit))
            {
                if (hit.collider.GetComponent<Doorway>())
                {
                    Wall.SetActive(false);
                    hit.collider.GetComponent<Doorway>().Wall.SetActive(false);
                }
                if (hit.collider.GetComponentInParent<Doorway>())
                {
                    Wall.SetActive(false);
                    hit.collider.GetComponentInParent<Doorway>().Wall.SetActive(false);
                }
            }
        }

        public void DisableDoor() 
        {
            Wall.SetActive(false);
            Door.SetActive(false);         
        }

        public void ActivateDoorway() 
        {
            Wall.SetActive(false);
            Door.SetActive(true);
        }

        public Transform GetNextRoomPlace() { return roomPoint.transform; }

    #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 midpoint = new Vector3(transform.right.x, transform.position.y,
                transform.right.z);
            Debug.DrawRay(transform.position, transform.forward * -10, Color.red);
        }
    #endif

    }

}
