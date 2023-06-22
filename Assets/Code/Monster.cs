using System.Collections;
using System.Collections.Generic;

using System.Threading;
using UnityEngine;

using UnityEngine.Splines;

//using UnityEngine.SplineUtility;

public class ActiveState
{

    public virtual void movement(Monster monster, Character player, Flareshot flare) { }
    public virtual void initialize(Monster monster) { }
}

public class RoamingState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare)
    {
        float playerDist = Vector3.Distance(player.transform.position, monster.transform.position);
        if (flare.currentflare)
        {
            float flareDist = Vector3.Distance(flare.currentflare.transform.position, monster.transform.position);
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
    public override void movement(Monster monster, Character player, Flareshot flare)
    {
        Debug.Log("Attacking");
        monster.transform.LookAt(player.transform.position);
        monster.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * monster.moveSpeed);

        //if flare is in range
        if (flare.currentflare)
        {
            float flareDist = Vector3.Distance(flare.currentflare.transform.position, monster.transform.position);
        }
        if (flareDist >= monster.flareRange || monster.health == 0)
        {
            monster.currentState = new RetreatingState();

        }

    }
    public override void initialize(Monster monster)
    {
    
    }
}

public class RetreatingState : ActiveState
{
    public override void movement(Monster monster, Character player, Flareshot flare)
    {

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


    public Spline 

    public float flareRange = 20f;

    public ActiveState currentState = new RoamingState();
    void Start()
    {
    }       

    // Update is called once per frame
    void Update()
    {
        currentState.movement(this, player, flare);
    }
}
