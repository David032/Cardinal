using Runic.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cardinal.AI.NPC
{
    public enum Behaviour
    {
        Traveller, //Travel between 2 geographical different regions
        Trader, //Spends their day at a shop location, and night at home
        Wanderer, //Wander around the location they were placed
        Guard, //Stationary
        Patroller //Move between a specified list of points
    }

    public class NPCBehaviour : MonoBehaviour
    {
        public Behaviour NPCBehaviourModel = Behaviour.Guard;
        NavMeshAgent agent;

        public Transform[] PatrollerPoints;
        private int PatrollerDestPoint = 0;

        public Transform[] TravellerPoints;
        int TravellerDestPoint = 0;
        float TravellerTimeAtPlace;
        float DesiredTimeAtPlace = 30f;

        public Transform[] WandererPoints;
        int WandererDestinationPoints = 0;
        float WandererTimeAtPlace;
        float DesiredTimeAtWanderedPlace = 15f;

        public Transform workplace;
        public Transform homeplace;
        TimeManager timeManager;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            timeManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>();

            switch (NPCBehaviourModel)
            {
                case Behaviour.Traveller:
                    TravelToNextPoint();
                    break;
                case Behaviour.Trader:
                    break;
                case Behaviour.Wanderer:
                    WanderSomewhere();
                    break;
                case Behaviour.Guard:
                    break;
                case Behaviour.Patroller:
                    GotoNextPoint();
                    break;
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (NPCBehaviourModel)
            {
                case Behaviour.Traveller:
                    TravellerBehaviour();
                    break;

                case Behaviour.Trader:
                    if (timeManager.timeCycle == Cycle.Day)
                    {
                        agent.SetDestination(workplace.transform.position);
                    }
                    else if (timeManager.timeCycle == Cycle.Night)
                    {
                        agent.SetDestination(homeplace.transform.position);
                    }

                    if (Vector3.Distance(this.transform.position, homeplace.transform.position) < 5f)
                    {
                        GetComponent<CapsuleCollider>().enabled = false;
                        GetComponent<NPCHearing>().enabled = false;
                        GetComponent<NPCPerspective>().enabled = false;
                    }
                    else if (Vector3.Distance(this.transform.position, homeplace.transform.position) > 5f)
                    {
                        GetComponent<CapsuleCollider>().enabled = true;
                        GetComponent<NPCHearing>().enabled = true;
                        GetComponent<NPCPerspective>().enabled = true;
                    }

                    break;

                case Behaviour.Wanderer:
                    WandererBehaviour();
                    break;
                case Behaviour.Guard:
                    //There's no behaviour here intentionally
                    break;
                case Behaviour.Patroller:
                    PatrollerBehaviour();
                    break;
                default:
                    break;
            }
        }

        private void TravellerBehaviour()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                TravellerTimeAtPlace += Time.deltaTime;
                if (TravellerTimeAtPlace > DesiredTimeAtPlace)
                {
                    TravelToNextPoint();
                }
                else if (TravellerTimeAtPlace < DesiredTimeAtPlace)
                {
                    //Have him wander inbetween here?
                }
            }
        }

        private void PatrollerBehaviour()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

        private void WandererBehaviour()
        {
            if (!agent.pathPending && agent.remainingDistance < 2.5f)
            {
                WandererTimeAtPlace += Time.deltaTime;
                if (WandererTimeAtPlace > DesiredTimeAtWanderedPlace)
                {
                    WanderSomewhere();
                }
            }
        }

        void GotoNextPoint()
        {
            if (PatrollerPoints.Length == 0)
                return;
            agent.destination = PatrollerPoints[PatrollerDestPoint].position;
            PatrollerDestPoint = (PatrollerDestPoint + 1) % PatrollerPoints.Length;
        }

        void TravelToNextPoint()
        {
            TravellerTimeAtPlace = 0;
            if (TravellerPoints.Length == 0)
            {
                return;
            }
            agent.destination = TravellerPoints[TravellerDestPoint].position;
            TravellerDestPoint = (TravellerDestPoint + 1) % TravellerPoints.Length;
        }

        void WanderSomewhere()
        {
            WandererTimeAtPlace = 0;
            if (WandererPoints.Length == 0)
            {
                return;
            }
            agent.destination = WandererPoints[Random.Range(0, WandererPoints.Length)].position;
        }
    }
}
