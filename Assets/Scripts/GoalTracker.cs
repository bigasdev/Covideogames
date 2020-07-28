using System;
using System.Collections;
using UnityEngine;

public class GoalTracker : MonoBehaviour
{
    public Goals goals;
    public float goalScore = 0;
    bool countingScore = false;
    public int peopleToMask = 0;
    bool countingMask = false;
    public float timeToSurvive = 0;
    bool countingSurvival = false;
    public int peopleToSendToHospital = 0;
    bool countingSendHospital = false;



    float timeSurvived = 0;
    public PlayerController player;
    public CounterScore playerScore;

    public WinScreen winScreen;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetGoals();
    }

    private void SetGoals()
    {
        if (goals.highScore.activated)
        {
            goalScore = goals.highScore.highScore;
            countingScore = true;
        }
        if (goals.peopleMasked.activated)
        {
            peopleToMask = goals.peopleMasked.peopleMasked;
            countingMask = true;

        }
        if (goals.survival.activated)
        {
            timeToSurvive = goals.survival.timeSurvived;
            countingSurvival = true;
        }
        if (goals.sendHospital.activated)
        {
            peopleToSendToHospital = goals.sendHospital.peopleSent;
            countingSendHospital = true;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ProcessWinConditions())
        {
            Win();
        }
    }

    private bool ProcessWinConditions()
    {
        return (ProcessScore(goalScore) && ProcessMasks(peopleToMask) && ProcessSurvival(timeToSurvive) && ProcessSendHospital(peopleToSendToHospital));
    }

    private bool ProcessSendHospital(int peopleToSendToHospital)
    {
        if (!countingSendHospital) return true;
        else if (player.peopleHealed >= peopleToSendToHospital) return true;
        else return false;
    }

    bool ProcessScore(float goalScore)
    {
        if (!countingScore) return true;
        else if (playerScore.GetScore() >= goalScore) return true;
        else return false;
    }
    bool ProcessMasks(int peopleToMask)
    {
        if (!countingMask) return true;
        if (playerScore.peopleMasked.Count >= peopleToMask) return true;
        else return false;
    }

    bool ProcessSurvival(float timeToSurvive)
    {
        if (!countingSurvival) return true;
        timeSurvived += Time.deltaTime;
        if (timeSurvived > timeToSurvive) return true;
        else return false;
    }

    public void Win()
    {

        winScreen.won = true;
    }
}
