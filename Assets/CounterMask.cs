using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterMask : MonoBehaviour
{
    // Start is called before the first frame update
    public    CounterScore player;
    Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.peopleMasked.Count.ToString();
    }
}
