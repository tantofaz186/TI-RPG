using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDistracao : MonoBehaviour
{
    public delegate void contactPoint(Vector3 contactpoint);
    public event contactPoint onhitground;
    
    [SerializeField]
    private bool isPicked = false;

    private void OnCollisionEnter(Collision collision)
    {
        onhitground?.Invoke(collision.contacts[0].point);
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
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 3f, Vector3.up, out hit, LayerMask.GetMask("Player")))
        {       
                GetComponent<Rigidbody>().isKinematic = true;
                transform.SetParent(hit.collider.transform);
                transform.localPosition = Vector3.forward;
                isPicked = true;
           }
    }

    private void ThrowObject()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
        isPicked = false;
    }
}
