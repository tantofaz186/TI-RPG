using IA;
using Skills;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : Agente
    {
        private Vector3 targetPosition;
        private Camera mainCamera;
        
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
                    Mover(targetPosition);
                }
            }
        }
    }

}