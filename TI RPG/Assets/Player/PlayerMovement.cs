using System;
using System.Collections;
using Controllers;
using IA;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovement : Agente
    {
   
        private Vector3 targetPosition;
        private Camera mainCamera;
             
        [SerializeField] private int vidas =2;
        public int Vidas
        {
            get { return vidas; }
            set
            {
                vidas = value;
                if (vidas <= 0)
                {
                    Time.timeScale = 0;
                    Debug.Log("Game Over");
                    GameOverController.Instance.GameOver();
                }
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("Inimigo"))
            {
                Debug.Log("oi");
                Vidas -= 1;
                StartCoroutine(TomarDano());
            }
        }
        IEnumerator TomarDano()
        {
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            Color selfColor = mr.material.color;
            Color damageColor = Color.red;
            velocidade *= 2;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.3f);
                mr.material.color = damageColor;
                yield return new WaitForSeconds(0.3f);
                mr.material.color = selfColor;   
            }
            velocidade /= 2;
            
        }
        private void Awake()
        {
            mainCamera = Camera.main;
            targetPosition = transform.position;
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(1))
            {
                targetPosition = transform.position;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }
            }
            Mover(targetPosition);
        }
    }

}