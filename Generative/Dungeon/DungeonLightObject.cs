using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLightObject : MonoBehaviour
{
    [SerializeField]
    GameObject LightSource;
    [SerializeField]
    GameObject ParticleSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //LightSource.SetActive(true);
            ParticleSource.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //LightSource.SetActive(false);
            ParticleSource.SetActive(false);
        }
    }
}
