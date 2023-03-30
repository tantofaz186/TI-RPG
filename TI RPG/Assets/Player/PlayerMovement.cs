using System;
using IA;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovement : Agente
    {
   
        private Vector3 targetPosition;
        private Camera mainCamera;
             [SerializeField]
        private int vidas =2;
        private GameObject Gameoverscreen;
        public int Vidas
        {
            get { return vidas; }
            set
            {
                vidas = value;
                if (vidas <= 0)
                {
                    vidas = 0;
                    Time.timeScale = 0;
                    Debug.Log("Game Over");
                }
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Inimigo")
            {
                Debug.Log("oi");
                Vidas -= 1;
            }
        }
        private void Awake()
        {
            mainCamera = Camera.main;
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