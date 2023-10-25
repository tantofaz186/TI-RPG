using System;
using System.Linq;
using UnityEngine;

namespace Rpg.Dialogs
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "RPG/Dialog", order = 0)]
    public class Dialog : ScriptableObject
    {
        [Serializable]
        public enum DialogEntryType
        {
            Normal,
            Choice
        }
        
        [Serializable]
        public class DialogEntry
        {
            public string id;
            public DialogEntryType type;
            public string speaker;
            public string message;
            public Sprite sprite;
            
            [Header("CHOICE")]
            public DialogOption[] options;
        }

        [Serializable]
        public class DialogOption
        {
            public string text;
            public Dialog dialog;
            public string targetId;
        }

        #region Fields
        public DialogEntry[] entries;
        #endregion

        public int FindEntry(string id)
        {
            if (String.IsNullOrEmpty(id))
                return 0;
            
            for(int i = 0; i < entries.Length; i ++)
                if (entries[i].id == id)
                    return i;

            return -1;
        }

        public DialogEntry FindEntry(int idx)
        {
            if (idx >= 0 && idx < entries.Length)
                return entries[idx];

            return null;
        }
    }
}