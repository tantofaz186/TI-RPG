﻿using System.Collections;
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
        [SerializeField] SkinnedMeshRenderer smr;
        public Text vidaInfinitaText;
        private PlayerMovement m_PlayerMovement;
        [SerializeField] private AudioSource audioPlayer;
    

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
<<<<<<< HEAD
        void OnCollisionEnter(Collision collision)
       {
=======
        void OnCollisionEnter(Collision collision) 
        {
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
            if (collision.collider.gameObject.CompareTag("Inimigo"))
            {
                Debug.Log("oi");
                Vidas -= 1;
                StartCoroutine(TomarDano());
                audioPlayer.Play();
<<<<<<< HEAD
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

=======
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

>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d

        IEnumerator TomarDano()
        {
            Color selfColor = smr.material.color;;
            Color damageColor = Color.red;

            m_PlayerMovement.GetSpeedBoost(speedBoostTime,speedBoostMultiplier);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.3f);
                smr.material.color = damageColor;
                yield return new WaitForSeconds(0.3f);
                smr.material.color = selfColor;
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
<<<<<<< HEAD
            GameObject vidaInfinitaTextObject = GameObject.Find("cheatSkillText");
=======
            GameObject vidaInfinitaTextObject = GameObject.FindWithTag("CheatSkillText");
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
            vidaInfinitaText = vidaInfinitaTextObject.GetComponent<Text>();
            vidaInfinitaText.text = "";
            m_PlayerMovement = GetComponent<PlayerMovement>();
        }

        private void Awake()
        {
            audioPlayer = GetComponent<AudioSource>();
        }
        private void Update()
        {
            vidaInfinita();
        }
    }
}