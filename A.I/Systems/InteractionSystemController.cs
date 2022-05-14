using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystemController : MonoBehaviour
{
    public float minimumTrustLevel = 0.25f;
    public float MEMORYDECAY = 1000f;
    public float commonFactionBuff = 0.1f;
    [Tooltip("Should be something in the 10+ range")]
    public int repeatedConnectionRatio = 25;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
