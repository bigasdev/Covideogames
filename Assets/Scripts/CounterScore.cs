using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CounterScore : MonoBehaviour
{
    Text text;
    PlayerController player;
    public int score;
    public float scoreTickTime = 5f;
    float timeSinceLastScore = Mathf.Infinity;
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
                if (bot.wearingMask) score++;
            }
            score -= player.sickPeople.Count;
        }
        text.text = (score + player.peopleHealed *10 - player.peopleLost *10).ToString();
        timeSinceLastScore += Time.deltaTime;
    }

}
