using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList",
    menuName = "Cardinal/Generative/Enemies")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> AvailableEnemies = new List<GameObject>();
}
