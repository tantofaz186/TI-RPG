using System;
using System.Collections;
using System.Collections.Generic;
using IA;
using Skills;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : Agente
    {
        private Vector3 targetPosition;
        private Camera mainCamera;
        private Animator corpo_fsm;
        public GameObject mouseInput;

        private void Awake()
        {
            mainCamera = Camera.main;
            targetPosition = transform.position;
            corpo_fsm = gameObject.GetComponent<Animator>();
            corpo_fsm.SetFloat("Mover", 0.5f);
            mouseInput.SetActive(false);
        }

        protected override void Update()
        {
            
            base.Update();
            agente.speed = velocidade;
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    mouseInput.transform.position = targetPosition;
                    mouseInput.SetActive(true);
                    Mover(targetPosition);
                }
            }
            #region Movimentacao/Mecanim
            if (agente.destination == transform.position)
            {
                corpo_fsm.SetBool("movimentando", false);
                Debug.Log("Parou");
                mouseInput.SetActive(false);
            }
            else if(!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                corpo_fsm.SetBool("movimentando", true);
                velocidade = 3.0f;
                StartCoroutine(LerpValue("Mover", 0.5f));
            }
            if (Input.GetKey(KeyCode.LeftControl))//Agachar
            {
                corpo_fsm.SetBool("agachado", true);
                velocidade = 1.75f;
                StartCoroutine(LerpValue("Mover", 0f));
            }
            else corpo_fsm.SetBool("agachado", false);// Desagachar

            if (Input.GetKey(KeyCode.LeftShift))//Correr
            {
                velocidade = 4.25f;
                StartCoroutine(LerpValue("Mover", 1f));
            }


            #endregion
        }

        private IEnumerator LerpValue(string variableName, float targetValue)
        {
            float currentValue = corpo_fsm.GetFloat(variableName);
            while (currentValue - targetValue > 0.01f || currentValue - targetValue < -0.01f)
            {
                corpo_fsm.SetFloat(variableName, Mathf.Lerp(currentValue, targetValue, 0.1f));
                currentValue = corpo_fsm.GetFloat(variableName);
                yield return null;
            }
            corpo_fsm.SetFloat(variableName, targetValue);
        }
    }

}