using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Debug
{
    public class DEBUGTeleportView : MonoBehaviour
    {
        public GameObject teleportScreen;

        public void showView()
        {
            teleportScreen.SetActive(!teleportScreen.activeSelf);
        }
    }
}
