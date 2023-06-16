using System.Collections;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerDano : MonoBehaviour
    {
        [SerializeField] private int vidas = 2;
        [SerializeField] private float speedBoostMultiplier = 2f;
        [SerializeField] private float speedBoostTime = 2f;
        public Text vidaInfinitaText;
        private PlayerMovement m_PlayerMovement;

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

            if (gameObject.GetComponent<MeshRenderer>() == null)
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
            m_PlayerMovement.GetSpeedBoost(speedBoostTime,speedBoostMultiplier);

            if (gameObject.GetComponent<MeshRenderer>() == null)
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
        private void vidaInfinita()
        {
            if (Input.GetKeyDown(KeyCode.F8))
            {
                Vidas += 9999999;
                StartCoroutine(ActivateCheatText());
            }
        }
        private IEnumerator ActivateCheatText()
        {
           
            vidaInfinitaText.gameObject.SetActive(true);
            vidaInfinitaText.text = "Cheat Activated";

            yield return new WaitForSeconds(3f);


            vidaInfinitaText.text = "";
        }
        private void Start()
        {
            vidaInfinitaText.text = "";
            m_PlayerMovement = GetComponent<PlayerMovement>();
        }
        private void Update()
        {
            vidaInfinita();
        }
    }
}