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
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetMouseButtonDown(0))
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