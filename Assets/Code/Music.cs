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
    }

    private void Update()
    {

        if (TheMonster.currentState == new RoamingState()|| player.movementState == new SwimState())
        {
            // ak.soundEngine.setSwitch("",this.object);
            AkSoundEngine.SetSwitch("MusicSwitch","Neutral", this.gameObject);
        }
        if (TheMonster.currentState == new AttackingState())
        {
            //ak.soundEngine.setSwitch("",this.object);
            AkSoundEngine.SetSwitch("MusicSwitch", "BeingChased", this.gameObject);
        }
        if (player.movementState == new WalkState())
        {
            //ak.soundEngine.setSwitch("",this.object);
            AkSoundEngine.SetSwitch("MusicSwitch", "InBase", this.gameObject);
        }
        
    }
}
