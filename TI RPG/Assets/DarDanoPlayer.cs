using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class DarDanoPlayer : MonoBehaviour
{
    [SerializeField] PlayerDano playerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ola mundo");
            playerScript = other.GetComponent<PlayerDano>();
            if (playerScript != null)
            {
                //playerScript.tomarDano();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
