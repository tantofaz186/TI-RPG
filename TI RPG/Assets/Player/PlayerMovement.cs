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

        private void Awake()
        {
            mainCamera = Camera.main;
            targetPosition = transform.position;
            corpo_fsm = gameObject.GetComponent<Animator>();
        }

        protected override void Update()
        {
            base.Update();
            agente.speed = velocidade;
            if (Input.GetMouseButtonDown(1))
            {
                targetPosition = transform.position;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                }
                if (transform.position != targetPosition)
                {
                    corpo_fsm.SetBool("movimentando", true);

                }
            }
            Mover(targetPosition);

            #region Movimentacao/Mecanim

            if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)//Idle
            {
                corpo_fsm.SetBool("movimentando", false);
                Debug.Log("Parou");
            }
            if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z && Input.GetKey(KeyCode.LeftControl) == true)//Agachar
            {
                corpo_fsm.SetBool("agachado", true);
            }
            else corpo_fsm.SetBool("agachado", false);// Desagachar

            if (Input.GetKey(KeyCode.LeftShift))//Correr
            {
                velocidade = 4.25f;
                corpo_fsm.SetFloat("Mover", 0.5f);
            }
            else if (Input.GetKey(KeyCode.LeftControl))//Sneak
            {
                velocidade = 1.75f;
                corpo_fsm.SetFloat("Mover", 1.0f);
            }
            else//Parar de correr
            {
                velocidade = 3.0f;
                corpo_fsm.SetFloat("Mover", 0f);
            }

            #endregion
        }
    }

}