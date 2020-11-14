using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public static GameStats gameStats;

    public int peopleDead = 0;
    public int peopleHealed = 0;
    public int peopleMasked = 0;
    public int peopleTotal = 0;
    public int peopleSick = 0;

    public int score = 0;
    public float scoreTickTimer = 1;
    float scoreTimer;

    public List<AiController> npcs;
    public List<Sink> sinks;
    public List<Health> sickPeopleList;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameStats == null)
            gameStats = this.gameObject.GetComponent<GameStats>();
        npcs = new List<AiController>();
        sinks = new List<Sink>();
        sickPeopleList = new List<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdatePopulation();
    }

    private void UpdateScore()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer>scoreTickTimer && !GameManager.gameManager.gameIsOver)
        {
            score += 1;
            scoreTimer = 0;
        }

    }

    public void AddScore(int bonus)
    {
        score += bonus;
    }
    
   private void UpdatePopulation()
    {
        peopleSick = sickPeopleList.Count;
        peopleTotal = npcs.Count;

    }

 

}
