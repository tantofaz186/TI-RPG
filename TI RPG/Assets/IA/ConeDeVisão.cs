using System;
using UnityEngine;

public class ConeDeVisão : MonoBehaviour
{
    [SerializeField] float raio;
    [SerializeField] float angulo;
    [SerializeField] Transform alvo;
    public Transform Alvo => alvo;
    public event Action OnFoundPlayer;
    
    private void Awake()
    {
        alvo = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direcaoPlayer = alvo.position - transform.position;
        float anguloPlayer = Vector3.Angle(direcaoPlayer, transform.forward);

        if (!(anguloPlayer < angulo / 2f)) return;
        if (!Physics.Raycast(transform.position, direcaoPlayer, out var hit, raio)) return;
        if (!hit.collider.CompareTag("Player")) return;
        OnFoundPlayer?.Invoke();
        Debug.Log("Encontrado");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raio);

        Vector3 visaoDirInicio = Quaternion.AngleAxis(-angulo / 2f, transform.up) * transform.forward;
        Vector3 visaoDirFim = Quaternion.AngleAxis(angulo / 2f, transform.up) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, visaoDirInicio * raio);
        Gizmos.DrawRay(transform.position, visaoDirFim * raio);
    }
}