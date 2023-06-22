using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public Monster TheMonster;
    

    private void Awake()
    {
        TheMonster = FindObjectOfType<Monster>();
    }

    private void Update()
    {
        if (TheMonster.currentState == new RoamingState())
        {
            // ak.soundEngine.setSwitch("",this.object);
        }
        if (TheMonster.currentState == new AttackingState())
        {
            //ak.soundEngine.setSwitch("",this.object);
        }
        if (true)
        {
            ;
        }
    }
}
