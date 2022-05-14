using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative.Dungeon
{
    [RequireComponent(typeof(BoxCollider))]
    public class Room : MonoBehaviour
    {
        public List<Doorway> doorways;
        public List<RoomFlags> RoomFlags;
        public RoomType Type;
        public bool isDone = false;
        public bool isBroken = false;
        public bool firstEntry = true;
        public bool isMainRoute = false;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<BoxCollider>().size = new Vector3(20, 10, 20);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (firstEntry)
            {
                Appraiser.RoomEnteredEvent @event = ScriptableObject.CreateInstance<Appraiser.RoomEnteredEvent>();
                @event.Name = "Player entered " + gameObject;
                @event.Time = Time.realtimeSinceStartup.ToString();
                @event.EventPriority = Priority.Medium;
                @event.RoomType = Type;
                @event.IsFirstEntry = true;
                if (isMainRoute)
                {
                    @event.Correleation = new Appraiser.HexadCorrelation(HexadTypes.Players, 100);
                }
                else
                {
                    @event.Correleation = new Appraiser.HexadCorrelation(HexadTypes.FreeSpirits, 200);
                }
                //Analyser.Analyser.Instance.RegisterEvent(@event);
                Runic.Tasks.TaskManager.Instance.IncrementProgressJobs(Runic.ProgressCriteria.RoomEntered);
                firstEntry = false;
            }
            else
            {
                if (isMainRoute)
                {
                    Appraiser.RoomEnteredEvent @event = ScriptableObject.CreateInstance<Appraiser.RoomEnteredEvent>();
                    @event.Name = "Player reentered " + gameObject;
                    @event.Time = Time.realtimeSinceStartup.ToString();
                    @event.RoomType = Type;
                    @event.IsFirstEntry = false;
                    @event.Correleation = new Appraiser.HexadCorrelation(HexadTypes.Players, 50);
                    Analyser.Analyser.Instance.RegisterEvent(@event);
                    Runic.Tasks.TaskManager.Instance.IncrementProgressJobs(Runic.ProgressCriteria.RoomEntered);
                }
                else
                {
                    Appraiser.RoomEnteredEvent @event = ScriptableObject.CreateInstance<Appraiser.RoomEnteredEvent>();
                    @event.Name = "Player reentered " + gameObject;
                    @event.Time = Time.realtimeSinceStartup.ToString();
                    @event.RoomType = Type;
                    @event.IsFirstEntry = false;
                    @event.Correleation = new Appraiser.HexadCorrelation(HexadTypes.FreeSpirits, 100);
                    Analyser.Analyser.Instance.RegisterEvent(@event);
                    Runic.Tasks.TaskManager.Instance.IncrementProgressJobs(Runic.ProgressCriteria.RoomEntered);
                }
            }
            //other.GetComponent<PlayerDeath>().CurrentRoom = gameObject;
        }
    }

}
