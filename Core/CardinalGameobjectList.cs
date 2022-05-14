using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal
{
    [CreateAssetMenu(fileName = "GameObjList",
   menuName = "Cardinal/Structures/Gameobject List")]
    public class CardinalGameobjectList : ScriptableObject
    {
        public List<GameObject> Objects;

        public GameObject GetRandomObject()
        {
            int randomSelection = Random.Range(0, Objects.Count);
            return Objects[randomSelection];
        }
    }

}
