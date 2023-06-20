using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : InteractableObject
{
    public override void Interact(Character player)
    {
        if (player.oxygen < 100)
        {
            player.oxygen = 100;
        }
    }
}
