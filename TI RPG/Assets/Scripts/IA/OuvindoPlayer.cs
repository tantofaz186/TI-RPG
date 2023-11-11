using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    public class OuvindoPlayer : Agente
    {
        public float areaDeAudicao = 5.0f;
        public LayerMask detectionLayer;
        [SerializeField] private float waitTimeWhenSuspicious = 1.5f;
        [SerializeField] private InimigoUI inimigoUI;
        [SerializeField] Transform alvo;
        public Transform Alvo => alvo;

        private void Awake()
        {
            alvo = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                areaDeAudicao = 5.0f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                areaDeAudicao = 3.0f;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                areaDeAudicao = 7.0f;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, areaDeAudicao);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        StartCoroutine(MoverAteOSom());
                    }
                    else
                    {
                        StopCoroutine(MoverAteOSom());
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaDeAudicao);
        }
        IEnumerator MoverAteOSom()
        {
            Vector3 lastKnownPosition = Alvo.position;
            Mover(lastKnownPosition);
            yield return new WaitForSeconds(waitTimeWhenSuspicious);
            SetState(new PatrulhaState(this, this.gameObject.GetComponent<InimigoQuePersegue>().Pontos));

        }
    }
}