using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.Dialogs
{
    public class DialogSystem : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public GameObject dialogBox;
        public Image imageSpeaker;
        public TMP_Text labelSpeaker;
        public TMP_Text labelMessage;
        public Button buttonNext;
        public Button[] buttonOptions;
        public TMP_Text[] labelOptions;

        public Dialog currentDialog;
        public int currentMessage;

        private void OnEnable()
        {
            _DisplayMessage();
            
            buttonNext.onClick.RemoveAllListeners();
            buttonNext.onClick.AddListener(() =>
            {
                DisplayMessage(currentDialog, currentMessage + 1);
            });
        }

        public void DisplayMessage(Dialog dialog, int message)
        {
            currentDialog = dialog;
            currentMessage = message;
            _DisplayMessage();
        }

        private void _DisplayMessage()
        {
            Dialog.DialogEntry entry = currentDialog == null ? null : currentDialog.FindEntry(currentMessage);

            if (entry == null)
            {
                CloseDialog();
                return;
            }

            //Fill fields
            imageSpeaker.sprite = entry.sprite;
            labelSpeaker.text = entry.speaker;
            labelMessage.text = entry.message;

            if (entry.type is Dialog.DialogEntryType.Normal)
            {
                buttonNext.gameObject.SetActive(true);

                foreach (var btn in buttonOptions)
                    btn.gameObject.SetActive(false);
            }
            else
            {
                buttonNext.gameObject.SetActive(false);

                for (int i = 0; i < buttonOptions.Length; i++)
                {
                    if (i < entry.options.Length)
                    {
                        Dialog.DialogOption option = entry.options[i];
                        Button btn = buttonOptions[i];
                        
                        //Show
                        labelOptions[i].text = option.text;
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(() =>
                        {
                            Dialog d = option.dialog == null ? currentDialog : option.dialog;
                            DisplayMessage(d, d.FindEntry(option.targetId));
                        });
                        
                        btn.gameObject.SetActive(true);
                    }
                    else
                        buttonOptions[i].gameObject.SetActive(false); 
                }
            }

            //Show message
            dialogBox.SetActive(true);
        }

        public void CloseDialog()
        {
            dialogBox.SetActive(false);
            currentDialog = null;
            currentMessage = 0;
        }
    }
}