using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Cardinal.AI.NPC
{
    public class NPCOpinionRenderer : MonoBehaviour
    {
        public TextMeshProUGUI opinionDiskNumber;
        SpriteRenderer opinionDisk;

        Transform playerTransform;
        void Start()
        {
            Invoke("SetUp", 1.5f);
        }

        void SetUp() 
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            opinionDisk = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            //This will NPE on transitioning between scenes but doesn't break anything
            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }

        public void UpdateDisplay(float opinion)
        {
            if (playerTransform is null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
            if (opinionDisk is null)
            {
                opinionDisk = gameObject.GetComponentInChildren<SpriteRenderer>();
            }
            Vector3 opinionDiskRotation = opinionDisk.transform.rotation.eulerAngles;
            opinionDiskNumber.text = opinion.ToString();
            opinionDisk.transform.rotation.eulerAngles.Set
                (opinionDiskRotation.x, playerTransform.rotation.eulerAngles.y,
                opinionDiskRotation.z);
        }

    }
}
