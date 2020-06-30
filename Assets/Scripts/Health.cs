using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    GameObject body;
    MeshRenderer meshRenderer;
    Color startColor;
    Color endColor = Color.red;


    public float infectionRate = 1; 
    public float infectionLevel = 0;
    [Range(0,2)] public float spreadMultiplier = 1;
    public float baseSpread = 4f;
    public float spreadArea;

    ParticleSystem sickness;
    public ParticleSystem sicknessEffect;
    public bool isSick = false;

    PlayerController player;

    private void Awake()
    {
        body = gameObject.transform.Find("BaseCharacter").gameObject;
        body = body.gameObject.transform.Find("Body").gameObject;
        meshRenderer = body.GetComponent<MeshRenderer>();
        startColor = meshRenderer.material.color;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spreadArea = baseSpread * spreadMultiplier;
        ProcessDisease(FindPeopleInRange());
        ProcessInfectionMilestones();
    }



    private  float FindPeopleInRange()
    {
        List<Health> peopleInRange = new List<Health>();
        foreach (AiController npc in player.npcs)
        {
            if (Vector3.Distance(this.transform.position, npc.transform.position) < npc.GetComponent<Health>().spreadArea)
            {
                peopleInRange.Add(npc.GetComponent<Health>());
            }
        }
        return peopleInRange.Count;
    }
    private void ProcessDisease(float peopleInRange)
    {
        float originalRate = infectionRate;
        infectionRate = peopleInRange;
        infectionLevel += infectionRate * Time.deltaTime * spreadMultiplier;
        meshRenderer.material.color = Color.Lerp(startColor, endColor, infectionLevel * Time.deltaTime);
        infectionRate = originalRate;
    }




    private void ProcessInfectionMilestones()
    {
        if (infectionLevel > 50)
        {
            if (!isSick)
            {
                GetSick();
            }
            else
            {
                IncreaseSicknessOverTime();
            }
        }
        if (infectionLevel > 100)
        {
            Die();
        }
    }

    private void IncreaseSicknessOverTime()
    {
        var emission = sickness.emission;
        emission.rateOverTime = 2 + infectionLevel / 100;
    }



    private void Die()
    {
        player.npcs.Remove(GetComponent<AiController>());
        player.sickPeople.Remove(this);
        player.peopleLost++;
        Destroy(this.gameObject);
    }

    private void GetSick()
    {
        sickness = Instantiate(sicknessEffect, this.transform);
        isSick = true;
        if (!this.gameObject.CompareTag("Player")) player.sickPeople.Add(this);
        infectionRate *= 2;
        baseSpread *= 2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, spreadArea);
    }
}
