using System.Collections;
using Player;
using UnityEngine;

namespace Objetos
{
    public enum triggerName
    {
        TriggerArmario,
        TriggerMesa
    }
    public class ObjetoEscondível : Interagível
    {
        [SerializeField]
        private bool estaEscondido = false;
        public static bool estaEscondidoGlobal = true;

        protected override void Interagir() => Esconder();
        [SerializeField] public triggerName trigger;

        private void Esconder()
        {
            Debug.Log(estaEscondidoGlobal);
            StartCoroutine(EsperarAnimaçãoParaEsconder());
        }

        private IEnumerator EsperarAnimaçãoParaEsconder()
        {
            player.enabled = false;
            Animator animator = player.GetComponent<Animator>();
            animator.keepAnimatorStateOnDisable = true;
            if (!estaEscondido)
            {
                player.GetComponent<Animator>().SetTrigger(trigger.ToString());
                switch (trigger)
                {
                    case (triggerName.TriggerArmario):
                        yield return new WaitForSecondsRealtime(3f);
                        break;
                    case (triggerName.TriggerMesa):
                        yield return new WaitForSecondsRealtime(1.09f);
                        break;
                    default:
                        break;
                }
            }
            player.gameObject.SetActive(estaEscondido);
            animator.keepAnimatorStateOnDisable = false;
            if (estaEscondido)
            {
                switch (trigger)
                {
                    case (triggerName.TriggerArmario):
                        yield return new WaitForSecondsRealtime(3f);
                        break;
                    case (triggerName.TriggerMesa):
                        yield return new WaitForSecondsRealtime(1.09f);
                        break;
                    default:
                        break;
                }
            }
            estaEscondido = !estaEscondido;
            estaEscondidoGlobal = !estaEscondidoGlobal;
            player.enabled = true;


        }
    }
}