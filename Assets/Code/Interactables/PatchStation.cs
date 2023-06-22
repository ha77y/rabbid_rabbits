using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchStation : InteractableObject
{

    public override void Interact(Character player) {

        if (player.numPatch >= player.maxItems)
        {
            Debug.Log("Full");
        }else
        {
            player.numPatch++;
            Destroy(gameObject);

        }
    }

}
