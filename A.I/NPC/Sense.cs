using UnityEngine;

namespace Cardinal.AI.NPC
{
    public class Sense : MonoBehaviour
    {
        public bool enableDebug = true;
        public float detectionRate = 1.0f;
        protected float elapsedTime = 0.0f;

        protected NPCMentalModel MentalModel;
        protected virtual void Initialize() { }
        protected virtual void UpdateSense() { }

        void Start()
        {
            MentalModel = GetComponent<NPCMentalModel>();
            elapsedTime = 0.0f;
            Initialize();
            InvokeRepeating("UpdateSense", 1, 1);
        }

    }

}
