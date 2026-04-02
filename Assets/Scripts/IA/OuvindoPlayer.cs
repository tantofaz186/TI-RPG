using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(InimigoQuePersegue))]
    public class OuvindoPlayer : MonoBehaviour
    {
        public float areaDeAudicao = 5.0f;

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

            Collider[] colliders = Physics.OverlapSphere(transform.position, areaDeAudicao, LayerMask.GetMask("Player"));

            if (colliders.Length <= 0) return;
            if (inimigoQuePersegue.CurrentState.GetType() == typeof(AtackState)) return;
            inimigoQuePersegue.SetStateAtacando();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, areaDeAudicao);
        }
    }
}