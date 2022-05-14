using UnityEngine;
namespace Cardinal.AI.NPC
{
    public class NPCDialogue : NPCDialogueCore
    {
        public bool oneShot = false;
        public string dialogue = "";

        GameObject spawnedDialogue;


        void Start()
        {

        }

        void Update()
        {

        }

        void OnMouseDown()
        {
            if (canTalk() & oneShot)
            {
                spawnedDialogue = Instantiate(dialogueWindow, dialogueSpot.transform);
                spawnedDialogue.GetComponent<RectTransform>().localPosition.Set(0, 0, 0);
                //spawnedDialogue.GetComponent<DialogueInstance>()
                //    .NewDialogueInstance(true, false, dialogue, this.gameObject);
            }
        }

        private void OnValidate()
        {
            //this.name = dialogue;
        }
    }
}
