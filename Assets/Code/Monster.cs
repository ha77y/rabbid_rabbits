using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState
{

    public virtual void movement(Monster monster, Character player) { }
    public virtual void switchState(Monster monster) { }
    public virtual void initialize(Monster monster) { }
}

public class RoamingState : ActiveState
{
    public override void movement(Monster monster, Character player)
    {

    }
    public override void switchState(Monster monster)
    {

    }
    public override void initialize(Monster monster)
    {

    }
}

public class AttackingState : ActiveState
{
    public override void movement(Monster monster, Character player)
    {
        monster.transform.LookAt(player.transform);
        monster.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * monster.moveSpeed);
    }
    public override void switchState(Monster monster)
    {

    }
    public override void initialize(Monster monster)
    {
    
    }
}

public class RetreatingState : ActiveState
{
    public override void movement(Monster monster, Character player)
    {

    }
    public override void switchState(Monster monster)
    {

    }
    public override void initialize(Monster monster)
    {

    }
}





public class Monster : MonoBehaviour
{

    public Character player;
    // Start is called before the first frame update
    public float moveSpeed = 2f;

    ActiveState currentState = new RoamingState();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.movement(this, player);
    }
}
