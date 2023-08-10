using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Objetos
{
    public class ObjetoDistracao : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        [SerializeField] private GameObject mao;
        [SerializeField] public string maoNome;
        public delegate void OnHitGroundHandler(Vector3 contactpoint);

        public event OnHitGroundHandler OnHitGround;
        [SerializeField] private float forcePower = 400f;

        [SerializeField] private bool isPicked = false;
        private Rigidbody rb;
        private Collider pickupCollider; // Collider to be disabled when picked up
        private bool isNear = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            mao = EncontrarMao(player.gameObject, maoNome);
            pickupCollider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHitGround?.Invoke(collision.contacts[0].point);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isNear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                isNear = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && !isPicked && isNear)
            {
                PickUpObject();
            }
            else if (Input.GetKeyDown(KeyCode.G) && isPicked)
            {
                ThrowObject();
            }
        }

        private void PickUpObject()
        {
            try
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Player"));

                foreach (Collider col in colliders)
                {
                    Rigidbody colRb = col.GetComponent<Rigidbody>();

                    if (colRb != null && colRb != rb && colRb.isKinematic)
                    {
                        Debug.Log("O objeto próximo não pode ser pego.");
                        return;
                    }
                }

                rb.isKinematic = true;
                pickupCollider.enabled = false; // Disable the collider
                transform.parent = mao.transform;
                transform.position = transform.parent.position;
                isPicked = true;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("Não há nenhum objeto próximo para ser pego");
            }
        }

        private void ThrowObject()
        {
            rb.isKinematic = false;
            pickupCollider.enabled = true; // Enable the collider
            Vector3 throwDir = (player.transform.forward + Vector3.up).normalized;
            transform.SetParent(null);
            rb.AddForce(throwDir * forcePower);
            isPicked = false;
        }

        GameObject EncontrarMao(GameObject _player, string nome)
        {
            for (int i = 0; i < _player.transform.childCount; i++)
            {
                if (_player.transform.GetChild(i).name == nome)
                {
                    return _player.transform.GetChild(i).gameObject;
                }

                GameObject aux = EncontrarMao(_player.transform.GetChild(i).gameObject, maoNome);

                if (aux != null)
                {
                    return aux;
                }
            }
            return null;
        }
    }
}