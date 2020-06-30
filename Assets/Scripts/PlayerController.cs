﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //handling
    public float speed = 10.0f;
    public float rotationSpeed = 450;
    public float currentSpeed = 0;
    public float actionRange = 2f;

    Vector3 input;
    public float yellRange = 5f;

    public Collider actionArea;

    AudioSource audioSource;
    public AudioClip washSFX;


    public List<AiController> npcs;
    public List<Sink> sinks;
    public List<Health> sickPeople;

    public bool dirtyHands = false;

    Health health;
    private Quaternion targetRotation;

    public int peopleHealed = 0;
    public int peopleLost = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        npcs = new List<AiController>();
        sinks = new List<Sink>();
        sickPeople = new List<Health>();
        health = GetComponent<Health>();
        health.infectionRate *= .25f;
    }
    void Update()
    {
        ProcessMovement();
        ProcessActions();
    }

    float minimumHeldDuration = 1.25f;
    float keyPressedTime = 0;
    bool holdingKey = false;
    private void ProcessActions()
    {
     

        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyPressedTime = 0;
            holdingKey = false;

        }



        else if (Input.GetKey(KeyCode.Space))
        {
            keyPressedTime += Time.deltaTime;
            if (keyPressedTime > minimumHeldDuration)
            {
                holdingKey = true;
                EnforceSocialDistance();

            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!holdingKey)
            {
                StartCoroutine(ActivateActionArea());
            }
        }

    }

    private IEnumerator ActivateActionArea()
    {
        actionArea.enabled = true;
        yield return new WaitForSeconds(.2f);
        actionArea.enabled = false;
    }

    private void EnforceSocialDistance()
    {
        foreach (AiController npc in npcs)
        {
            if (Vector3.Distance(this.transform.position, npc.transform.position) < yellRange)
            {
                npc.changeStatus(Status.SocialDistance);
            }
        }
    }

    private void ProcessMovement()
    {
        GetMovementInput();
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.z) < 1) return;
        Rotate();
        Move();
    }

    private void InteractWithWorld()
    {
        actionArea.enabled = true;

    }

    void GetMovementInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }


    void Rotate()
    {

        targetRotation = Quaternion.LookRotation(input);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Move()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    public void WashHands()
    {
        dirtyHands = false;
        health.infectionLevel -= 10;
        health.infectionLevel = Mathf.Max(health.infectionLevel, 0);
        audioSource.PlayOneShot(washSFX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AiController>() != null)
        {
            if (collision.gameObject.GetComponent<AiController>().wearingMask == false) return;
        }
    }


}