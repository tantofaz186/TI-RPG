using System;
using System.Collections.Generic;
using System.Linq;
using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quests/Quest")]
    public class Quest : ScriptableObject
    {
        public List<QuestObjective> objectives = new();
        public List<Quest> requiredQuests = new();

        public Item reward;
        private int _currentObjectiveIndex;
        private bool isCompleted;
        public bool CanBeStarted => requiredQuests.All(quest => quest.isCompleted);

        public void OnEnable()
        {
            objectives[_currentObjectiveIndex].OnComplete += OnObjectiveComplete;
        }

        public void OnDisable()
        {
            objectives[_currentObjectiveIndex].OnComplete -= OnObjectiveComplete;
        }

        public event Action<Item> OnComplete;

        private void OnObjectiveComplete()
        {
            objectives[_currentObjectiveIndex].OnComplete -= OnObjectiveComplete;
            if (_currentObjectiveIndex < objectives.Count - 1)
            {
                _currentObjectiveIndex++;
                objectives[_currentObjectiveIndex].OnComplete += OnObjectiveComplete;
            }
            else
            {
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            isCompleted = true;
            OnComplete?.Invoke(reward);
        }
    }
}