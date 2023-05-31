using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objetos
{
    public class ObjetoDistracao : MonoBehaviour
    {
        public delegate void OnHitGroundHandler(Vector3 contactpoint);

        public event OnHitGroundHandler OnHitGround;

        [SerializeField] private bool isPicked = false;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHitGround?.Invoke(collision.contacts[0].point);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && !isPicked)
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
                Collider col = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Player"))[0];
                rb.isKinematic = true;
                transform.SetParent(col.transform);
                transform.localPosition = Vector3.forward;
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
            Vector3 throwDir = transform.parent.forward;
            transform.SetParent(null);
            rb.AddForce(throwDir * 500f);
            isPicked = false;
        }
    }
}