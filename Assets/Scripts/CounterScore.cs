using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CounterScore : MonoBehaviour
{
    Text text;
    PlayerController player;
    public int countingScore;
    public int totalScore;
    int scoreModifier = 0;
    public float scoreTickTime = 5f;
    float timeSinceLastScore = Mathf.Infinity;
    public List<AiController> peopleMasked = new List<AiController>();


    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceLastScore > scoreTickTime)
        {
            timeSinceLastScore = 0;
            foreach(AiController bot in player.npcs)
            {
                if (bot.wearingMask && !peopleMasked.Contains(bot)) peopleMasked.Add(bot);
            }
            countingScore += peopleMasked.Count;
            countingScore -= player.sickPeople.Count;
        }
        totalScore = (countingScore + player.peopleHealed * 10 - player.peopleLost * 10 + scoreModifier);
        text.text = GetScore().ToString();
        timeSinceLastScore += Time.deltaTime;
        if (totalScore > PlayerPrefs.GetInt("HighsScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
        }

    }
    public int ChangeScore(int amount)
    {
       scoreModifier += amount;
        return scoreModifier;
    }

    public int GetScore()
    {
        if (totalScore < 0) return 0;
        else return totalScore;
    }

}
