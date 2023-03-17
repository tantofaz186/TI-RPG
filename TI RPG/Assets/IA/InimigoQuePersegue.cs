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
            //se eu encontrar o player pelo campo de visão, eu mudo de patrulha para encontrar o player
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentState.GetType() == typeof(PatrulhaState)) SetStatePerseguindo();
                else SetStatePatrulha();
            }
        }
        void EncontreiOPlayerNoCampoDeVisão()
        {
            if (currentState.GetType() == typeof(PerseguindoState)) return;
            if (currentState.GetType() != typeof(EncontrandoPlayerState))
            {
                EncontrandoPlayerState encontrandoPlayerState = new EncontrandoPlayerState();
                encontrandoPlayerState.OnFoundPlayer +=  SetStatePerseguindo;
                encontrandoPlayerState.OnForgetPlayer += SetStatePatrulha;
                
                SetState(encontrandoPlayerState);
                StartCoroutine(MoverAtéOALvo());

            }
            ((EncontrandoPlayerState)currentState).Encontrando();
        }

        IEnumerator MoverAtéOALvo()
        {
            Vector3 lastPosition = coneDeVisão.Alvo.position;
            yield return new WaitForSeconds(0.5f);
            while(Vector3.Distance(transform.position, coneDeVisão.Alvo.position) > 0.5f && currentState.GetType() == typeof(EncontrandoPlayerState))
            {
                Mover(lastPosition);
                yield return null;
            }
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
            }
        }
    }
}