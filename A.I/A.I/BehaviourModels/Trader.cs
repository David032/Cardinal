using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Behaviour
{
    public class Trader : BehaviourBase
    {
        public Transform HomeLocation;
        public Transform WorkLocation;

        void OnTriggerStay(Collider other)
        {
            //if (!other.CompareTag("Player"))
            //{
            //    return;
            //}
            //if (other.GetComponent<PlayerControls>().isInteracting)
            //{
            //    other.GetComponent<PlayerControls>().Interact
            //        (InteractionTypes.Person);
            //    transform.LookAt(other.transform);
            //    //Do dialogue things here?
            //    other.GetComponentInChildren<Runic.UI.TradeDisplayController>()
            //        .ShowTradeDisplay(Entity, other.gameObject.GetComponent<Entities.Player.Player>());
            //}
        }
    }
}

