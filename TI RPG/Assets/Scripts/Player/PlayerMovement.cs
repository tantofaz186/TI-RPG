using IA;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : Agente
    {
        public GameObject mouseInput;

        private Camera mainCamera;
        private PlayerDano playerDano;
        private Vector3 targetPosition;

        private void Awake()
        {
            mainCamera = Camera.main;
            targetPosition = transform.position;

            mouseInput.SetActive(false);
            playerDano = GetComponent<PlayerDano>();
        }

        protected override void Update()
        {
            base.Update();
            if (!playerDano.GameOver)
            {
                agente.speed = velocidade;
            }
            else
            {
                agente.speed = 0;
            }

            if (Input.GetMouseButton(1))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    Mover(targetPosition);
                }
            }

            mouseInput.transform.position = agente.destination;
            mouseInput.SetActive(!agente.isStopped);
        }
    }
}