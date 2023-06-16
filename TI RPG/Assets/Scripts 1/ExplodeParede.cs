using System.Collections;
using System.Collections.Generic;
using Objetos;
using Player;
using UnityEngine;

public class ExplodeParede : Interagível
{
    [SerializeField] GameObject[] peças;

    void Explode()
    {
        foreach(GameObject i in peças)
        {
            i.GetComponent<Rigidbody>().isKinematic = false;
            i.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10.0f, 2.0f);
        }

        transform.GetChild(14).gameObject.SetActive(false);
        enabled = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        peças = new GameObject[gameObject.transform.childCount - 1];
        for(int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            peças[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    protected override void Interagir() => Explode();
}
