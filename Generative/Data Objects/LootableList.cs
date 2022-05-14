using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootableList",
    menuName = "Cardinal/Generative/Nodes")]
public class LootableList : ScriptableObject
{
    public List<GameObject> LootNodes = new List<GameObject>();
}
