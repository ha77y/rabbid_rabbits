using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareStation : InteractableObject
{
    bool used = false;

    public override void Interact(Character player)
    {
        if (used)
        {
            Debug.Log("Empty");
        }
        else if (player.numFlare >= player.maxItems)
        {
            Debug.Log("Full");
        }
        else
        {
            player.numFlare++;
            used = true;
        }
    }

}
