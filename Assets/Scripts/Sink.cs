using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    PlayerController player;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Start()
    {
        player.sinks.Add(this);

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent.CompareTag("Player")) 
        {
            if (player.dirtyHands) player.WashHands();
        }
    }
}
