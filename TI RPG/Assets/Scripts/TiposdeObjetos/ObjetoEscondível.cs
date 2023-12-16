using System.Collections;
using UnityEngine;

namespace Objetos
{
    public enum triggerName { TriggerArmario, TriggerMesa }

    public class ObjetoEscondível : Interagível
    {
        private static readonly int TriggerFechando = Animator.StringToHash("TriggerFechando");
        private static readonly int TriggerAbrindo = Animator.StringToHash("TriggerAbrindo");

        [SerializeField]
        private Animator anim;

        [SerializeField]
        private Transform playerPosition;

        [SerializeField]
        public triggerName trigger;

        private bool estaEscondido;
        private Animator playerAnimator;

        protected override void Awake()
        {
            base.Awake();
            playerAnimator = player.GetComponent<Animator>();
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

            float animationTime = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(animationTime);
            yield return new WaitForSeconds(1.5f);
            // player.gameObject.SetActive(estaEscondido);
            estaEscondido = !estaEscondido;
            if (!estaEscondido)
            {
                player.enabled = true;
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
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
            if (!hit.collider.gameObject.Equals(gameObject))
            {
                yield break; // Check if the hit collider belongs to this object
            }

            if (player.isActiveAndEnabled) player.Mover(playerPosition.position);
            yield return null;
            player.enabled = false;

            if (!estaEscondido)
            {
                yield return new WaitUntil(() => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"));
            }

            yield return WaitUntilPlayerInPosition();

            Interagir();
        }
    }
}