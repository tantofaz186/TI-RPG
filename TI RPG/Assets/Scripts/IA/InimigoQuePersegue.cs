using Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(ConeDeVisão))]
    public class InimigoQuePersegue : Agente
    {
        [SerializeField]
        private List<Vector3> pontos;

        [SerializeField]
        private float waitTimeWhenSuspicious = 1.5f;

        [SerializeField]
        private float forgetTime = 5f;

        [SerializeField]
        private InimigoUI inimigoUI;

        private ConeDeVisão coneDeVisão;
        [SerializeField]
        private float forgetTimer;
        public List<Vector3> Pontos => pontos;

        private void Awake()
        {
            coneDeVisão = GetComponent<ConeDeVisão>();
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;
            SetStatePatrulha();
            animator.SetBool("movimentando", true);
            animator.SetFloat("Mover", 0.5f);
        }

        protected override void Update()
        {
            base.Update();
            if (currentState.GetType() != typeof(PerseguindoState)) return;
            forgetTimer += Time.deltaTime;
            if (!(forgetTimer >= forgetTime)) return;
            PararDeCampar();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            foreach (Vector3 ponto in pontos)
            {
                Gizmos.DrawSphere(ponto, 0.5f);
            }

            Gizmos.color = Color.red;
            try
            {
                Gizmos.DrawSphere(coneDeVisão.Alvo.position, 0.5f);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Armadilha")) gameObject.SetActive(false);
        }

        private void EncontreiOPlayerNoCampoDeVisão()
        {
            if (currentState.GetType() == typeof(PerseguindoState)) return;
            if (currentState.GetType() != typeof(EncontrandoPlayerState)) SetStateEncontrandoPlayer();

            ((EncontrandoPlayerState)currentState).Encontrando();
        }

        private IEnumerator MoverAtéOAlvo()
        {
            inimigoUI.MostrarImagem(true);
            Vector3 lastKnownPosition = coneDeVisão.Alvo.position;
            Mover(transform.position);
            yield return new WaitForSeconds(waitTimeWhenSuspicious);
            Mover(lastKnownPosition);
            inimigoUI.MostrarImagem(false);
        }

        private void SetStatePerseguindo()
        {
            coneDeVisão.OnFoundPlayer -= EncontreiOPlayerNoCampoDeVisão;
            SetState(new PerseguindoState(this, coneDeVisão.Alvo));
        }

        private void SetStateAtacando()
        {
            coneDeVisão.OnFoundPlayer -= EncontreiOPlayerNoCampoDeVisão;
            SetState(new AtackState(this, coneDeVisão.Alvo));
        }

        public void SetStatePatrulha()
        {
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;
            SetState(new PatrulhaState(this, pontos));
        }

        private void SetStateEncontrandoPlayer(float percentage = 0f)
        {
            EncontrandoPlayerState encontrandoPlayerState = new(percentage, 1f / waitTimeWhenSuspicious);
            encontrandoPlayerState.OnFoundPlayer += SetStateAtacando;
            encontrandoPlayerState.OnForgetPlayer += SetStatePatrulha;
            SetState(encontrandoPlayerState);
            StartCoroutine(MoverAtéOAlvo());
        }
        public void PararDeCampar()
        {
            if (forgetTimer >= forgetTime && ObjetoEscondível.estaEscondidoGlobal == true)
            {
                Debug.Log("esqueci");
                SetStatePatrulha();
                SetStateEncontrandoPlayer(.8f);
                forgetTimer = 0f;
            }
        }
    }
}