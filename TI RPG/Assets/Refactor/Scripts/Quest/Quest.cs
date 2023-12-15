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

        public List<Rewards> rewards;

        [SerializeField]
        private bool _isCompleted;

        private int _currentObjectiveIndex;

        public bool IsCompleted
        {
            get => _isCompleted;
            private set => _isCompleted = value;
        }

        public bool CanBeStarted => requiredQuests.All(quest => quest.IsCompleted);

        public void _OnEnable()
        {
            Debug.Log("Quest Enabled");
            objectives[_currentObjectiveIndex]._OnEnable();
            objectives[_currentObjectiveIndex].OnComplete += OnObjectiveComplete;
        }

        public void _OnDisable()
        {
            Debug.Log("Quest Disabled");
            objectives[_currentObjectiveIndex]._OnDisable();
            objectives[_currentObjectiveIndex].OnComplete -= OnObjectiveComplete;
        }

        public event Action<List<Rewards>> OnComplete;

        private void OnObjectiveComplete()
        {
            Debug.Log("Objective Complete");
            objectives[_currentObjectiveIndex]._OnDisable();
            objectives[_currentObjectiveIndex].OnComplete -= OnObjectiveComplete;
            if (_currentObjectiveIndex < objectives.Count - 1)
            {
                _currentObjectiveIndex++;
                objectives[_currentObjectiveIndex]._OnEnable();
                objectives[_currentObjectiveIndex].OnComplete += OnObjectiveComplete;
            }
            else
            {
                Debug.Log("Quest Complete");
                CompleteQuest();
            }
        }

        private void CompleteQuest()
        {
            IsCompleted = true;
            OnComplete?.Invoke(rewards);
        }
    }

    [Serializable]
    public class Rewards
    {
        public Item item;
        public bool removeOnComplete;
    }
}