using System.Collections;
using Controllers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerDano : MonoBehaviour
    {
        [SerializeField] private int vidas = 2;
        [SerializeField] private float speedBoostMultiplier = 2f;
        [SerializeField] private float speedBoostTime = 2f;
        public int Vidas
        {
            get { return vidas; }
            set
            {
                vidas = value;
                if (vidas <= 0)
                {
                    Time.timeScale = 0;
                    Debug.Log("Game Over");
                    GameOverController.Instance.GameOver();
                }
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("Inimigo"))
            {
                Vidas -= 1;
                StartCoroutine(TomarDano());
            }
        }
        IEnumerator TomarDano()
        {
            Color selfColor;
            Color damageColor = Color.red;
            SkinnedMeshRenderer smr;
            MeshRenderer mr;

            if (this.gameObject.GetComponent<MeshRenderer>() == null)
            {
                smr = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
                selfColor = smr.material.color;
                mr = null;
            }
            else
            {
               mr = gameObject.GetComponent<MeshRenderer>();
               selfColor = mr.material.color;
               smr = null;
            }

           GetComponent<PlayerMovement>().GetSpeedBoost(speedBoostTime,speedBoostMultiplier);

            if (this.gameObject.GetComponent<MeshRenderer>() == null)
            {
                yield return new WaitForSeconds(0.3f);
                smr.material.color = damageColor;
                yield return new WaitForSeconds(0.3f);
                smr.material.color = selfColor;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    mr.material.color = damageColor;
                    yield return new WaitForSeconds(0.3f);
                    mr.material.color = selfColor;
                }
            }
            
        }
    }
}