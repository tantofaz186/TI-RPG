using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace IA
{
    [RequireComponent(typeof(InimigoQuePersegue))]
    public class OuvindoPlayer : Agente
    {
        public float areaDeAudicao = 5.0f;

        [SerializeField]
        private float waitTimeWhenSuspicious = 1.5f;

        [SerializeField]
        protected NavMeshAgent agente;

        private InimigoQuePersegue inimigoQuePersegue;

        private Vector3 lastHeardPosition;

        private void Awake()
        {
            inimigoQuePersegue = gameObject.GetComponent<InimigoQuePersegue>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                areaDeAudicao = 7.0f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                areaDeAudicao = 3.0f;
            }
            else
            {
                areaDeAudicao = 5.0f;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, areaDeAudicao);
            if (colliders.Length <= 0) return;
            foreach (Collider _collider in colliders)
            {
                if (_collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("ola");
                }
                if (_collider.gameObject.CompareTag("Player") && inimigoQuePersegue.viuPlayer==false)
                {
                    Debug.Log("1");
                    lastHeardPosition = _collider.transform.position;
                    Mover(lastHeardPosition);
                }
                else if (Vector3.Distance(transform.position, lastHeardPosition) < 0.1f && inimigoQuePersegue.viuPlayer == false)
                {
                    Debug.Log("2");
                    inimigoQuePersegue.SetStatePatrulha();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaDeAudicao);
        }
    }
}