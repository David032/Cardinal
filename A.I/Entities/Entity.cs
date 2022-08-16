using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Cardinal.Builder;

namespace Cardinal.AI.Entities
{
    [Serializable]
    public class EntityData
    {
        public string EntityName;
        public PhysicalAbility PhysicalSkill = PhysicalAbility.Normal;
        public MentalAbility MentalSkill = MentalAbility.Normal;
        public Vector2 HomeLocation;
        public BuildingData Home => CardinalBuilder.Instance.GetTiles().Where
                    (x => x.xPos == HomeLocation.x && x.yPos == HomeLocation.y)
                    .FirstOrDefault().construct.GetComponent<BuildingData>();
        public Vector2 WorkLocation;
        public WorkBuilding Work => CardinalBuilder.Instance.GetTiles().Where
                    (x => x.xPos == WorkLocation.x && x.yPos == WorkLocation.y)
                    .FirstOrDefault().construct.GetComponent<WorkBuilding>();
    }

    //Entity is purely for AI DATA, see AI.behaviour for behaviour
    public class Entity : MonoBehaviour
    {
        public EntityData Data;
        string _identifier;
        public string Identifier
        {
            get => _identifier;
            set => _identifier = value;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}

