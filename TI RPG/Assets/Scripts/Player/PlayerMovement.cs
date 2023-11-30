using System.Collections;
using IA;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : Agente
    {
        public GameObject mouseInput;
        public float folego = 100;

        [SerializeField]
        private bool podeCorrer;

        [SerializeField]
        private CapsuleCollider _collider;

        private Animator corpo_fsm;
        private Camera mainCamera;
        private Vector3 targetPosition;

        private void Awake()
        {
            mainCamera = Camera.main;
            targetPosition = transform.position;
            corpo_fsm = gameObject.GetComponent<Animator>();
            corpo_fsm.SetFloat("Mover", 0.5f);
            mouseInput.SetActive(false);
            podeCorrer = true;
        }

        protected override void Update()
        {
            base.Update();
            agente.speed = velocidade;

            if (Input.GetMouseButton(1))
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    mouseInput.SetActive(true);
                    Mover(targetPosition);
                }

            mouseInput.transform.position = agente.destination;

            #region Movimentacao/Mecanim

            if (agente.destination == transform.position)
            {
                corpo_fsm.SetBool("movimentando", false);
                if (folego < 100) folego += 6 * Time.deltaTime;

                mouseInput.SetActive(false);
            }
            else if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                corpo_fsm.SetBool("movimentando", true);
                velocidade = 3.0f;
                if (folego < 100) folego += 6 * Time.deltaTime;

                StartCoroutine(LerpValue("Mover", 0.5f));
            }
            else
            {
                corpo_fsm.SetBool("movimentando", true);
            }

            if (Input.GetKey(KeyCode.LeftControl)) // Agachar
            {
                corpo_fsm.SetBool("agachado", true);
                velocidade = 1.75f;
                if (folego < 100) folego += 6 * Time.deltaTime;

                StartCoroutine(LerpValue("Mover", 0f));
                _collider.enabled = false;
            }
            else if (Input.GetKey(KeyCode.LeftShift)) // Correr
            {
                if (folego > 0 && podeCorrer)
                {
                    velocidade = 4.25f;
                    folego -= 10 * Time.deltaTime;
                    StartCoroutine(LerpValue("Mover", 1f));
                }
                else
                {
                    // Sem Folego
                    podeCorrer = false;
                    corpo_fsm.SetBool("movimentando", true);
                    velocidade = 3.0f;
                    StartCoroutine(LerpValue("Mover", 0.5f));
                    if (folego < 100) folego += 6 * Time.deltaTime;
                }
            }
            else if (!Input.GetKey(KeyCode.LeftControl))
            {
                corpo_fsm.SetBool("agachado", false); // Desagachar
                _collider.enabled = true;
            }

            if (folego >= 25 && podeCorrer == false) podeCorrer = true;

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