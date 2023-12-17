using System.Collections;
using Player;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    public bool podeCorrer;

    public float folego = 100;

    private Animator corpo_fsm;
    private PlayerMovement Player;

    private void Awake()
    {
        Player = GetComponent<PlayerMovement>();
        corpo_fsm = gameObject.GetComponent<Animator>();
        corpo_fsm.SetFloat("Mover", 0.5f);
        podeCorrer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        #region Movimentacao/Mecanim

        if (Player.NavMeshAgent.destination == transform.position)
        {
            corpo_fsm.SetBool("movimentando", false);
            if (folego < 100) folego += 6 * Time.deltaTime;
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            corpo_fsm.SetBool("movimentando", true);
            Player.velocidade = 3.0f;
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
            Player.velocidade = 1.75f;
            if (folego < 100) folego += 6 * Time.deltaTime;

            StartCoroutine(LerpValue("Mover", 0f));
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // Correr
        {
            if (folego > 0 && podeCorrer)
            {
                Player.velocidade = 4.25f;
                folego -= 10 * Time.deltaTime;
                StartCoroutine(LerpValue("Mover", 1f));
            }
            else
            {
                // Sem Folego
                podeCorrer = false;
                corpo_fsm.SetBool("movimentando", true);
                Player.velocidade = 3.0f;
                StartCoroutine(LerpValue("Mover", 0.5f));
                if (folego < 100) folego += 6 * Time.deltaTime;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            corpo_fsm.SetBool("agachado", false); // Desagachar
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