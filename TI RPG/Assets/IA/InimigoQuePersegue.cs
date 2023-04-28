using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(ConeDeVisão))]
    public class InimigoQuePersegue : Agente
    {
        [SerializeField] private List<Vector3> pontos;
        [SerializeField] private float waitTimeWhenSuspicious = 1.5f;
        [SerializeField] private float forgetTime = 5f;
        private float forgetTimer = 0f;
        public List<Vector3> Pontos => pontos;
        private ConeDeVisão coneDeVisão;
	[SerializeField] private InimigoUI inimigoUI;

        private void Awake()
        {
            coneDeVisão = GetComponent<ConeDeVisão>();
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;
            SetStatePatrulha();
        }

        protected override void Update()
        {
            base.Update();
            if (currentState.GetType() != typeof(PerseguindoState)) return;
            forgetTimer += Time.deltaTime;
            if (!(forgetTimer >= forgetTime)) return;
            SetStatePatrulha();
            SetStateEncontrandoPlayer(.8f);
            forgetTimer = 0f;
        }
        
        void EncontreiOPlayerNoCampoDeVisão()
        {
            if (currentState.GetType() == typeof(PerseguindoState)) return;
            if (currentState.GetType() != typeof(EncontrandoPlayerState))
            {
                SetStateEncontrandoPlayer();
            }
            ((EncontrandoPlayerState)currentState).Encontrando();
        }

        IEnumerator MoverAtéOAlvo()
        {
            inimigoUI.MostrarImagem(true);
            Vector3 lastKnownPosition = coneDeVisão.Alvo.position;
            Mover(transform.position);
            yield return new WaitForSeconds(waitTimeWhenSuspicious);
            Mover(lastKnownPosition);
            inimigoUI.MostrarImagem(false);

        }
        void SetStatePerseguindo()
        {
            coneDeVisão.OnFoundPlayer -= EncontreiOPlayerNoCampoDeVisão;
            SetState(new PerseguindoState(this, coneDeVisão.Alvo));
        }

        void SetStatePatrulha()
        {
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;
            SetState(new PatrulhaState(this, pontos));
        }
        void SetStateEncontrandoPlayer(float percentage = 0f)
        {
            EncontrandoPlayerState encontrandoPlayerState = new EncontrandoPlayerState(percentage);
            encontrandoPlayerState.OnFoundPlayer += SetStatePerseguindo;
            encontrandoPlayerState.OnForgetPlayer += SetStatePatrulha;
            SetState(encontrandoPlayerState);
            StartCoroutine(MoverAtéOAlvo());
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            foreach (var ponto in pontos)
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
    }
}
