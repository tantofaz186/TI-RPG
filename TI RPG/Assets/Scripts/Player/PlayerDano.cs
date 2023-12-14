using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerDano : MonoBehaviour
    {
        [SerializeField]
        private int vidas = 5;

        [SerializeField]
        private float speedBoostMultiplier = 2f;

        [SerializeField]
        private float speedBoostTime = 2f;

        [SerializeField]
        private SkinnedMeshRenderer smr;

        public Text vidaInfinitaText;

        [SerializeField]
        private AudioSource audioPlayer;

        private PlayerMovement m_PlayerMovement;

        Color selfColor;

        Color damageColor = Color.red;

        public int Vidas
        {
            get => vidas;
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

        private void Awake()
        {
            audioPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            GameObject vidaInfinitaTextObject = GameObject.FindWithTag("CheatSkillText");
            vidaInfinitaText = vidaInfinitaTextObject.GetComponent<Text>();
            vidaInfinitaText.text = "";
            m_PlayerMovement = GetComponent<PlayerMovement>();
            selfColor = smr.material.color;
        }

        private void FixedUpdate()
        {
            vidaInfinita();
            
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.CompareTag("Inimigo"))
            {
                Debug.Log("oi");
                Vidas -= 1;
                StartCoroutine(TomarDano());
                audioPlayer.Play();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Armadilha"))
            {
                Debug.Log("oi");
                Vidas -= 1;
                StartCoroutine(TomarDano());
                audioPlayer.Play();
            }
        }

        private IEnumerator TomarDano()
        {


            m_PlayerMovement.GetSpeedBoost(speedBoostTime, speedBoostMultiplier);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.3f);
                smr.material.color = damageColor;
                yield return new WaitForSeconds(0.3f);
                smr.material.color = selfColor;
            }
        }
        void OnCoroutineEnd()
        {
            if (smr.material.color == damageColor)
            {
                smr.material.color = selfColor;
            }
            Debug.Log("Coroutine has ended!");
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
    }
}