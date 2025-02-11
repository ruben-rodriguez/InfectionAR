﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;
using Vuforia;

public class EnemyHandler : MonoBehaviour
{

    private Transform goal;
    private NavMeshAgent agent;
    [SerializeField]
    private float speed;
    public DefaultTrackableEventHandler trackingHandler;
    [SerializeField]
    public String anim_walk;
    [SerializeField]
    public String anim_death;
    [SerializeField]
    private String goal_name;

    private bool isOwned = false;
    //public PlayerShoot ps;

    void Update()
    {
        if (trackingHandler.isTracked)
        {
            //if (ps.targetAchieved)
            if(!isOwned)
            {
                //Debug.Log("start walking");
                //create references
                goal = GameObject.FindGameObjectWithTag(goal_name).transform;
                transform.position = Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
                //start the walking animation
                //GetComponent<Animation>().Play("Zombie_Walk_01");
                PlayAnimation(anim_walk);
                //trackingHandler.isTracked = false;
                //ps.targetAchieved = false;
            }

        }
    }
        

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "coin")
        {
            Debug.Log("Coin Destroyed");
            Destroy(col.gameObject);
            trackingHandler.isTracked = false;
        }
        if (col.gameObject.tag == "bullet")
        {
            Debug.Log("Enemy Destroyed");
            PlayAnimation(anim_death);
            Destroy(gameObject, 2);
        }
        if (col.gameObject.tag == goal_name)
        {
            Debug.Log("goal owned by enemy");
            Destroy(col.gameObject);
            isOwned = true;
        }
    }

    public void PlayAnimation(String anim_name)
    {
        GetComponent<Animation>().Play(anim_name);
    }

    public void StopAnimation(String anim_name)
    {
        GetComponent<Animation>().Stop(anim_name);
    }
}
