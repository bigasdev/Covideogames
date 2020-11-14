using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Música : MonoBehaviour
{
    public static Música music;

    // Start is called before the first frame update
    void Start()
    {
        music = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
