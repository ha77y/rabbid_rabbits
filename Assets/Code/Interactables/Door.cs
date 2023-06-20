using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{

    public override void Interact(Character player)
    {
        Debug.Log("SwitchMovement");
        //player.movementState.switchMovement(player);
        player.doorMove(this);
    }


    public void open(Character player, bool enter)
    {
        float animDur = 1.5f;

        StartCoroutine(Open(animDur, this, player, enter));
    }


    IEnumerator Open(float duration, Door door, Character player, bool enter)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        player.doorEnter(player, door, enter);
    }

    public void close()
    {

    }
}
