using System.Collections;
using Controllers;
using UnityEngine;

namespace Player
{
    public class PlayerDano : MonoBehaviour
    {
        [SerializeField] private int vidas = 2;
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
                Debug.Log("oi");
                Vidas -= 1;
                StartCoroutine(TomarDano());
            }
        }
        IEnumerator TomarDano()
        {
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            Color selfColor = mr.material.color;
            Color damageColor = Color.red;
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