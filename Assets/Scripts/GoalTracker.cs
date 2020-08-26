using System;
using System.Collections;
using UnityEngine;

public class GoalTracker : MonoBehaviour
{
    public static GoalTracker goalTracker;
    public bool noGoal = false;

    public Goals goals;
    float goalScore = 0;
    bool countingScore = false;

    int peopleToMask = 0;
    bool countingMask = false;

    float timeToSurvive = 0;
    bool countingSurvival = false;

    int peopleToSendToHospital = 0;
    bool countingSendHospital = false;

    public bool goalsFulfilled = false;

    // Start is called before the first frame update
    private void Awake()
    {
        SetGoals();
        if (goalTracker == null)
            goalTracker = this.gameObject.GetComponent<GoalTracker>();
    }

    private void SetGoals()
    {
        if (noGoal) return;
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
            goalsFulfilled = true;
        
        }
    }

    private bool ProcessWinConditions()
    {
        return (ProcessScore(goalScore) && ProcessMasks(peopleToMask) && ProcessSurvival(timeToSurvive) && ProcessSendHospital(peopleToSendToHospital));
    }

    private bool ProcessSendHospital(int peopleToSendToHospital)
    {        if (!countingSendHospital) return true;
        else if (GameStats.gameStats.peopleHealed >= peopleToSendToHospital) return true;
        else return false;
    }

    bool ProcessScore(float goalScore)
    {
        if (!countingScore) return true;
        else if (GameStats.gameStats.score >= goalScore) return true;
        else return false;
    }
    bool ProcessMasks(int peopleToMask)
    {
        if (!countingMask) return true;
        if (GameStats.gameStats.peopleMasked >= peopleToMask) return true;
        else return false;
    }

    bool ProcessSurvival(float timeToSurvive)
    {
        if (!countingSurvival) return true;
        if (GameManager.gameManager.currentTime > timeToSurvive) return true;
        else return false;
    }

    public void Win()
    {
        goalsFulfilled = true;
    }
}
