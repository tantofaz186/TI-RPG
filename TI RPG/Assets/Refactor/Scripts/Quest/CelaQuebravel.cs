using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    public class CelaQuebravel : MonoBehaviour
    {
        public Quest quest;

        private void OnEnable()
        {
            quest.OnComplete += OnQuestComplete;
            if (quest.IsCompleted)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            quest.OnComplete -= OnQuestComplete;
        }

        private void OnQuestComplete(Item obj)
        {
            gameObject.SetActive(false);
        }
    }
}