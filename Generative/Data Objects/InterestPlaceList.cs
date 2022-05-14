using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "POIsList", menuName = "Cardinal/Generative/Field/POIs")]
public class InterestPlaceList : ScriptableObject
{
    public List<GameObject> PotentialPOIs = new List<GameObject>();
}
