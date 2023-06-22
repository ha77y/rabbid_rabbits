using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareStation : InteractableObject
{

    public override void Interact(Character player)
    {
        if (player.numFlare >= player.maxItems)
        {
            Debug.Log("Full");
        }
        else
        {
            player.numFlare++;
            Destroy(gameObject);

        }
    }

}
