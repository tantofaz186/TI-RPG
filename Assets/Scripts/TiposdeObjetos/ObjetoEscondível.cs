using System.Collections;
using UnityEngine;

namespace Objetos
{
    public class ObjetoEscondível : Interagível
    {
        private static readonly int TriggerFechando = Animator.StringToHash("TriggerFechando");
        private static readonly int TriggerAbrindo = Animator.StringToHash("TriggerAbrindo");

        [SerializeField]
        private Animator anim;

        [SerializeField]
        private Transform playerPosition;

        private bool estaEscondido;
        private Animator playerAnimator;

        protected override void Awake()
        {
            base.Awake();
            playerAnimator = player.GetComponent<Animator>();
            playerAnimator.keepAnimatorStateOnDisable = true;
        }

        protected override void Interagir()
        {
            Esconder();
        }

        private void Esconder()
        {
            StartCoroutine(EsperarAnimaçãoParaEsconder());
        }

        private IEnumerator EsperarAnimaçãoParaEsconder()
        {
            playerAnimator.SetTrigger(estaEscondido ? TriggerFechando : TriggerAbrindo);
            anim.SetTrigger(estaEscondido ? TriggerFechando : TriggerAbrindo);

            yield return new WaitForSeconds(1.5f);
            estaEscondido = !estaEscondido;
            if (!estaEscondido)
            {
                player.enabled = true;
            }
            else
            {
                player.gameObject.SetActive(false);
            }
        }

        private WaitUntil WaitUntilPlayerInPosition()
        {
            return new WaitUntil(() =>
            {
                Transform thisTranform = transform.parent;
                Vector3 forward = thisTranform.forward;
                Transform playerTransform = player.transform;
                Vector3 playerForward = playerTransform.forward;


                Quaternion targetRotation =
                    Quaternion.LookRotation(thisTranform.position - playerTransform.position);

                playerTransform.rotation =
                    Quaternion.Slerp(playerTransform.rotation, targetRotation, 5f * Time.deltaTime);
                return Vector3.Dot(playerForward, forward) < -0.998f;
            });
        }

        protected override IEnumerator MoverParaObjeto()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit)) yield break;
            if (!hit.collider.gameObject.Equals(gameObject)) yield break;


            if (player.isActiveAndEnabled)
            {
                player.Mover(playerPosition.position);
            }
            else
            {
                player.gameObject.SetActive(true);
            }

            yield return null;

            if (!estaEscondido)
            {
                Vector3 originalDestination = player.NavMeshAgent.destination;
                yield return new WaitUntil(() => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"));
                if (player.NavMeshAgent.destination != originalDestination)
                {
                    StopAllCoroutines();
                    yield break;
                }
            }

            player.enabled = false;

            yield return WaitUntilPlayerInPosition();

            Interagir();
        }
    }
}