using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public int score;
    public int highScore;
    public Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        text.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
