using System;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    public abstract class QuestObjective : ScriptableObject
    {
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