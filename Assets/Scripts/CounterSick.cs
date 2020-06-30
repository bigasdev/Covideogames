using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterSick : MonoBehaviour
{
    Text text;
    PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.sickPeople.Count.ToString();
    }
}
