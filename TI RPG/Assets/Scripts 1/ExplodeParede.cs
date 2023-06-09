using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeParede : MonoBehaviour
{
    [SerializeField] GameObject[] peças;
    [SerializeField] GameObject player;
    [SerializeField] bool explodiu = false;

    void Explode()
    {
        explodiu = true;

        foreach(GameObject i in peças)
        {
            i.GetComponent<Rigidbody>().isKinematic = false;
            i.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10.0f, 2.0f);
        }

        transform.GetChild(14).gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        peças = new GameObject[gameObject.transform.childCount - 1];

        for(int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            peças[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position - transform.position).magnitude <= 4.0f && Input.GetKeyDown(KeyCode.Space) && explodiu == false)
        {
            Explode();
        }
    }
}
