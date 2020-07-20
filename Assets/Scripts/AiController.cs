using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Status
{
    Normal,
    SocialDistance
}
public class AiController : MonoBehaviour
{
    NavMeshAgent agent;
    public float runFactor = 1.25f;

    public GameObject target;
    GameObject mask;
    PlayerController player;
    Health health;

    AudioSource audioSource;
    public AudioClip maskON;
    public AudioClip maskOFF;

    //tOM = takeoffmask
    public ParticleSystem tOMEffect;
    public bool tOMBehaviourAllowed = false;
    public float tOMMaskChance = 0;
    public float tOMBehaviourAllowedChance = 25f;
    public float tOMCooldown = 10f;
    float tOMTimer = 0f;

    public Transform hospitalWaypoint;

    public bool wearingMask = false;
    public bool goHospital = false;

    public float wanderRadius = 10;
    public float wanderDistance = 10;
    public float wanderJitter = 5;

    public Status status = Status.Normal;
    float timeInThisStatus = 0f;
    public float socialDistance = 2f;
    GameObject socialDistanceIndicator;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        health = GetComponent<Health>();
        mask = gameObject.transform.Find("BaseCharacter").gameObject;
        mask = mask.gameObject.transform.Find("Specifics").gameObject;
        mask = mask.gameObject.transform.Find("Mask").gameObject;
        hospitalWaypoint = GameObject.FindGameObjectWithTag("Finish").transform;
        audioSource = this.GetComponent<AudioSource>();
        socialDistanceIndicator = gameObject.transform.Find("Social Distance Indicator").gameObject;

    }
    void Start()
    {
        player.npcs.Add(this);
        DetermineMaskBehaviour();
    }

    private void DetermineMaskBehaviour()
    {
        if (UnityEngine.Random.Range(0, 100) < tOMBehaviourAllowedChance)
        {
            tOMBehaviourAllowed = true;
        }
    }



    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee()
    {
        Vector3 fleeVector;
        fleeVector = player.transform.position - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    Vector3 wanderTarget = Vector3.zero;
    void Wander()
    {
        wanderTarget += new Vector3(UnityEngine.Random.Range(-1f, 1f) * wanderJitter, 0,
                                UnityEngine.Random.Range(-1f, 1f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
        Seek(targetWorld);
    }
    void Update()
    {
        NavigationBehaviour();
        TakeOffMaskBehaviour();
        ProcessStates();
    }

    private void ProcessStates()
    {
        timeInThisStatus += Time.deltaTime;
        if (status == Status.SocialDistance & timeInThisStatus > 3f)
        {
            ChangeStatus(Status.Normal);
        }
    }

    private void NavigationBehaviour()
    {
        if (goHospital)
        {

        }
        else if (status == Status.SocialDistance)
        {
            List<Vector3> fleeTargets = new List<Vector3>();

            foreach (AiController npc in player.npcs)
            {
                if (Vector3.Distance(this.transform.position, npc.transform.position) < socialDistance)
                {
                    fleeTargets.Add(npc.transform.position);
                }
                Flee();
            }
        }
        else Wander();
    }

    private void TakeOffMaskBehaviour()
    {
        if (tOMBehaviourAllowed)
        {
            if (!wearingMask) return;
            tOMTimer += Time.deltaTime;
            if (tOMTimer > tOMCooldown)
            {
                if (tOMMaskChance < UnityEngine.Random.Range(0, 100))
                {
                    TakeOffMask();
                }
                tOMTimer = 0;
            }
        }
    }
    public void WearMask()
    {
        mask.SetActive(true);
        wearingMask = true;
        health.infectionRate *= .25f;
        health.spreadMultiplier *= .25f;
        audioSource.PlayOneShot(maskON);
    }
    private void TakeOffMask()
    {
        mask.SetActive(false);
        wearingMask = false;
        health.infectionRate = 1;
        health.spreadMultiplier = 1;
        audioSource.PlayOneShot(maskOFF);
        ParticleSystem particle = Instantiate(tOMEffect, this.gameObject.transform);
        Destroy(particle, 2f);
    }

    public void GoToHospital()
    {
        goHospital = true;
        agent.speed = agent.speed * runFactor;
        agent.SetDestination(hospitalWaypoint.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        HospitalTrigger(other);
        PlayerTrigger(other);
    }

    private void PlayerTrigger(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            if (wearingMask && health.isSick) GoToHospital();
            if (player.dirtyHands == false)
            {

                if (!wearingMask)
                {
                    WearMask();
                    player.dirtyHands = true;
                }
            }
            else PlayFailedActionSound();
        }
    }

    private void PlayFailedActionSound()
    {
        throw new NotImplementedException();
    }

    private void HospitalTrigger(Collider other)
    {
        if (other.gameObject.transform == hospitalWaypoint.transform)
        {
            player.npcs.Remove(this);
            player.sickPeople.Remove(health);
            player.peopleHealed++;
            Destroy(this.gameObject);
        }
    }

    public void ChangeStatus(Status newStatus)
    {
        status = newStatus;
        if (newStatus == Status.SocialDistance)
        {
            socialDistanceIndicator.SetActive(true);
            timeInThisStatus = 0;

        }
        if (newStatus == Status.Normal)
        {
            socialDistanceIndicator.SetActive(false);
            timeInThisStatus = 0;
        }
    }
}
