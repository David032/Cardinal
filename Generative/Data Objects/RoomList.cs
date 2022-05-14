using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomList",menuName = "Cardinal/Generative/Dungeon")]
public class RoomList : ScriptableObject
{
    public List<GameObject> RoomsToUse = new List<GameObject>();
}
