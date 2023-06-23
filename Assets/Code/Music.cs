using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public Monster TheMonster;
    public Character player;

    private void Awake()
    {
        TheMonster = FindObjectOfType<Monster>();
        player = FindObjectOfType<Character>();
        AkSoundEngine.SetState("InWater", "OutBase");
        AkSoundEngine.SetState("BeingAttacked", "Safe");
    }

    private void Update()
    {

        
        
    }
}
