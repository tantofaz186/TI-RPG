using System;
using UnityEngine;
using UnityEngine.Events;

namespace Rpg.Entities
{
    public class Interactible : MonoBehaviour
    {
        public bool isMouseOver = false;
        public UnityEvent onClick = new UnityEvent();

        public virtual void OnChangeIsMouseOver()
        {
            
        }
        
        private void OnMouseEnter()
        {
            isMouseOver = true;
            OnChangeIsMouseOver();
        }

        private void OnMouseExit()
        {
            isMouseOver = false;
            OnChangeIsMouseOver();
        }

        private void OnMouseDown()
        {
            onClick.Invoke();
        }
    }
}