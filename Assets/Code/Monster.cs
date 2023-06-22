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
        if (monster.cooldown > 0)
        {
            monster.cooldown -= Time.deltaTime;
        }
        float playerDist = Vector3.Distance(player.transform.position, monster.transform.position);

        float flareDist = 100f;
        if (flare.currentflare)
        {
            flareDist = Vector3.Distance(flare.currentflare.transform.position, monster.transform.position);
        }
        if (playerDist <= monster.aggroRange && monster.cooldown <= 0 && flareDist >= monster.flareRange)
        {
            monster.monsterReturn.transform.position = monster.transform.position;
            monster.monsterReturn.transform.rotation = monster.transform.rotation;
            monster.splineAnim.enabled = false;



            monster.currentState = new AttackingState();
            monster.cooldown = 5f;
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
            monster.currentState = new RetreatingState();
            //change to retreating
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
        monster.health = 10;

        monster.transform.LookAt(monster.monsterReturn.transform.position);

        //Quaternion movingRotation = Quaternion.identity;

        //movingRotation.eulerAngles = Vector3.RotateTowards(monster.transform.rotation.eulerAngles, monster.monsterReturn.transform.rotation.eulerAngles, 1f * Time.deltaTime, 0.1f);

        //monster.transform.rotation = movingRotation;
        monster.transform.position = Vector3.MoveTowards(monster.transform.position, monster.monsterReturn.transform.position, 8f * Time.deltaTime);


        if (monster.transform.position == monster.monsterReturn.transform.position)
        {
            monster.splineAnim.enabled = true;
            monster.currentState = new RoamingState();


        }

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
            //change to retreating

        }

    }
    public override void initialize(Monster monster)
    {

    }
}





public class Monster : MonoBehaviour
{
    public bool reoriented = false;
    public Flareshot flare;

    public Character player;
    // Start is called before the first frame update
    public float moveSpeed = 1.5f;

    public float aggroRange = 35f;

    public float cooldown = 0f;

    public GameObject monsterPath;

    public SplineAnimate splineAnim;


    public Spline spline;

    public float flareRange = 20f;


    public float health = 10f;

    public GameObject monsterReturn;

    public ActiveState currentState = new RoamingState();

    public GameObject damagePlayer;

    void Start()
    {
    }       

    // Update is called once per frame
    void Update()
    {
        currentState.movement(this, player, flare, spline);
        checkForPlayer();
    }


    public void takeDamage()
    {
        if(currentState is AttackingState)
        if(health <= 0)
        {
            currentState = new RetreatingState();
            
        }
        else
        {
            health -= 1;
        }

       // Debug.Log(currentState);
    }


    public void checkForPlayer()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, 1f))
        {

            //Debug.Log(raycastHit.transform);
            if (raycastHit.transform.TryGetComponent(out Character player))
            {
                player.takeDamage();
            }
        }
    }
}
