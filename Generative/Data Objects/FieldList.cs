using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldList", menuName = "Cardinal/Generative/Field/Fields")]
public class FieldList : ScriptableObject
{
    public List<GameObject> PotentialFields = new List<GameObject>();
}
