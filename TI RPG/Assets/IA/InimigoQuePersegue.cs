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
        public List<Vector3> Pontos => pontos;
        private ConeDeVisão coneDeVisão;

        private void Awake()
        {
            coneDeVisão = GetComponent<ConeDeVisão>();
            coneDeVisão.OnFoundPlayer += EncontreiOPlayerNoCampoDeVisão;
            SetStatePatrulha();
        }

        protected override void Update()
        {
            base.Update();
            /*if (currentState.GetType() == typeof(PerseguindoState))
            {
                
            }*/
        }

        /*protected override void Update()
        {
            
            //se eu encontrar o player pelo campo de visão, eu mudo de patrulha para encontrar o player
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentState.GetType() == typeof(PatrulhaState)) SetStatePerseguindo();
                else SetStatePatrulha();
            }
        }*/
        void EncontreiOPlayerNoCampoDeVisão()
        {
            if (currentState.GetType() == typeof(PerseguindoState)) return;
            if (currentState.GetType() != typeof(EncontrandoPlayerState))
            {
                EncontrandoPlayerState encontrandoPlayerState = new EncontrandoPlayerState();
                encontrandoPlayerState.OnFoundPlayer +=  SetStatePerseguindo;
                encontrandoPlayerState.OnForgetPlayer += SetStatePatrulha;
                
                SetState(encontrandoPlayerState);
                StartCoroutine(MoverAtéOAlvo());

            }
            ((EncontrandoPlayerState)currentState).Encontrando();
        }

        IEnumerator MoverAtéOAlvo()
        {
            Vector3 lastKnownPosition = coneDeVisão.Alvo.position;
            yield return new WaitForSeconds(waitTimeWhenSuspicious);
            Mover(lastKnownPosition);

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