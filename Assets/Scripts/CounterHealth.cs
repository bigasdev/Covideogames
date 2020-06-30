﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterHealth : MonoBehaviour
{


    Text text;
    PlayerController player;
    Health diseaseCounter;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        diseaseCounter = player.gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((100 - diseaseCounter.infectionLevel).ToString() + "%");
    }
}


