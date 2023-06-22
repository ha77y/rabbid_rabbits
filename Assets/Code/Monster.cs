using System;
using System.Collections;
using System.Collections.Generic;

using System.Threading;
using UnityEngine;

using UnityEngine.Splines;

//using UnityEngine.SplineUtility;

public class ActiveState
{

    public virtual void movement(Monster monster, Character player, Flareshot flare, Spline spline) { }
    public virtual void initialize(Monster monster) { }
}

public class RoamingState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare, Spline spline)
    {
        float playerDist = Vector3.Distance(player.transform.position, monster.transform.position);

        float flareDist = 100f;
        if (flare.currentflare)
        {
            flareDist = Vector3.Distance(flare.currentflare.transform.position, monster.transform.position);
        }
        if (playerDist <= monster.aggroRange && monster.cooldown <= 0 && flareDist >= monster.flareRange)
        {
            monster.splineAnim.enabled = false;
            monster.currentState = new AttackingState();
            monster.cooldown = 5;
        }
        else{
            if (monster.cooldown > 0)
            {
                monster.cooldown -= Time.deltaTime;
            }
        }
    }
    public override void initialize(Monster monster)
    {

    }
}

public class AttackingState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare, Spline spline)
    {
        monster.transform.LookAt(player.transform.position);
        monster.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * monster.moveSpeed);
        float flareDist = 100f;
        //if flare is in range
        if (flare.currentflare)
        {
            flareDist = Vector3.Distance(flare.currentflare.transform.position, monster.transform.position);
        }

        if (flareDist <= monster.flareRange)
        {
            monster.currentState = new FlaringState();

        }

        if( monster.health == 0)
        {        
            monster.splineAnim.enabled = true;
            monster.currentState = new RetreatingState();
        }

    }
    public override void initialize(Monster monster)
    {
    
    }
}

public class RetreatingState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare, Spline spline)
    {


        //spline.SplineUtility.GetNearestPoint<Spline>(Spline, spline, monster.transform., out float3 nearest)
    }
    public override void initialize(Monster monster)
    {

    }
}

public class FlaringState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare, Spline spline)
    {

        if (flare.currentflare)
        {
            monster.transform.LookAt(flare.currentflare.transform.position);
            monster.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * monster.moveSpeed);
        }
        else
        {
            monster.currentState = new RetreatingState();
        }

    }
    public override void initialize(Monster monster)
    {

    }
}





public class Monster : MonoBehaviour
{

    public Flareshot flare;

    public Character player;
    // Start is called before the first frame update
    public float moveSpeed = 1.5f;

    public float aggroRange = 35f;

    public float cooldown = 10f;

    public GameObject monsterPath;

    public SplineAnimate splineAnim;


    public Spline spline;

    public float flareRange = 20f;


    public float health = 10f;
    public ActiveState currentState = new RoamingState();
    void Start()
    {
    }       

    // Update is called once per frame
    void Update()
    {
        currentState.movement(this, player, flare, spline);
    }
}
