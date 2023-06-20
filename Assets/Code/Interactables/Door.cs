using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{

    public override void Interact(Character player)
    {
        Debug.Log("SwitchMovement");
        player.movementState.switchMovement(player);
    }

}
