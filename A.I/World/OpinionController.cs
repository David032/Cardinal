using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.AI.NPC;
using Runic.Managers;

namespace Cardinal.AI.World
{
    public enum Requirement
    {
        GreaterThan,
        LessThan
    }
    public enum UpdateMethod
    {
        OnUpdate,
        OnDayChange,
    }
    public enum Behaviour 
    {
        ToggleActive
    }

    public class OpinionController : MonoBehaviour
    {
        public NPCMentalModel ControllerNPC;
        public GameObject[] ObjectsToActOn;
        public Requirement RequiredState = Requirement.GreaterThan;
        public UpdateMethod WhenToUpdate = UpdateMethod.OnUpdate;
        public Behaviour WhatToDo = Behaviour.ToggleActive;
        [Range(0f,1f)]
        public float OpinionLevelRequired = 0.5f;
        public bool OneShot = false;
        bool hasFired = false;

        void Start()
        {
            if (WhenToUpdate == UpdateMethod.OnDayChange)
            {
                TimeManager.Instance.DayChange.AddListener(() => CheckForStateChange());
            }
        }

        void Update()
        {
            if (WhenToUpdate == UpdateMethod.OnUpdate)
            {
                CheckForStateChange();
            }
        }

        void CheckForStateChange() 
        {
            if (OneShot && !hasFired)
            {
                switch (RequiredState)
                {
                    case Requirement.GreaterThan:
                        if (OpinionLevelRequired <= ControllerNPC.opinion)
                        {
                            ExecuteChange();
                            hasFired = true;
                        }
                        break;
                    case Requirement.LessThan:
                        if (OpinionLevelRequired >= ControllerNPC.opinion)
                        {
                            ExecuteChange();
                            hasFired = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (RequiredState)
                {
                    case Requirement.GreaterThan:
                        if (OpinionLevelRequired >= ControllerNPC.opinion)
                        {
                            ExecuteChange();
                        }
                        break;
                    case Requirement.LessThan:
                        if (OpinionLevelRequired <= ControllerNPC.opinion)
                        {
                            ExecuteChange();
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        void ExecuteChange() 
        {
            switch (WhatToDo)
            {
                case Behaviour.ToggleActive:
                    foreach (GameObject item in ObjectsToActOn)
                    {
                        item.SetActive(!item.activeSelf);
                    }
                    break;
                default:
                    break;
            }
        }

    }
}

