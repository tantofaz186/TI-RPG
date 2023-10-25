using UnityEngine;

namespace Quests
{
    public class QuestSairCela : MonoBehaviour
    {
        [SerializeField] private GameObject grade;
        [SerializeField] private bool podeAbrir;
        [SerializeField] Dialogue dialogueAntesDeAbrir;
        [SerializeField] Dialogue dialogueQuandoPodeAbrir;
        private DialogueTrigger dialogueTrigger;

        private void Awake()
        {
            podeAbrir = false;
            dialogueTrigger = GetComponent<DialogueTrigger>();
            dialogueTrigger.dialogue = dialogueAntesDeAbrir;
            dialogueTrigger.triggeredDialogue = SetPodeAbrirTrue;
        }

        public void SetPodeAbrirTrue()
        {
            podeAbrir = true;
            dialogueTrigger.dialogue = dialogueQuandoPodeAbrir;
            dialogueTrigger.triggeredDialogue = AbrirGrade;
        }

        public void AbrirGrade()
        {
            if (podeAbrir)
            {
                grade.SetActive(false);
            }
        }
    
    
    }
}
