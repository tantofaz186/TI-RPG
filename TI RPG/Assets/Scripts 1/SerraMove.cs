using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerraMove : MonoBehaviour
{
    public GameObject ponto1, ponto2;
    public float vel_m, vel_r;
    float rot;
    public Transform proxPos;

    void Mover()
    {
        if (transform.position == ponto1.transform.position) proxPos = ponto2.transform;
        if (transform.position == ponto2.transform.position) proxPos = ponto1.transform;
        transform.position = Vector3.MoveTowards(transform.position, proxPos.position, vel_m * Time.deltaTime);
    }

    void Girar()
    {
        if (proxPos.position == ponto1.transform.position)
        {
            rot = rot + Time.deltaTime * vel_r;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        else
        {
            rot = rot + Time.deltaTime * -vel_r;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }
    private void Awake()
    {
        ponto1 = transform.parent.GetChild(1).gameObject;
        ponto2 = transform.parent.GetChild(2).gameObject;
        proxPos = ponto1.transform;
    }

    void Update()
    {
        Mover();
        Girar();
    }
}
