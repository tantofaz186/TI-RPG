using System.Collections;
using Controllers;
using IA;
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
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            Color selfColor = mr.material.color;
            Color damageColor = Color.red;
            GetComponent<PlayerMovement>().GetSpeedBoost(speedBoostTime,speedBoostMultiplier);
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