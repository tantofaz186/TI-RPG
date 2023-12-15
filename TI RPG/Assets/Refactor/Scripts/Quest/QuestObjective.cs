using System;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    public abstract class QuestObjective : ScriptableObject
    {
        public Dialogue dialogueOnComplete;
        public abstract void _OnEnable();
        public abstract void _OnDisable();
        public event Action OnComplete;
        public event Action OnFail;

        protected void CompleteObjective()
        {
            OnComplete?.Invoke();
        }

        protected void Fail()
        {
            OnFail?.Invoke();
        }
    }
}