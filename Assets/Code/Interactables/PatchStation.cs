using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchStation : InteractableObject
{
    bool used = false;

    public override void Interact(Character player) {
        if (used)
        {
            Debug.Log("Empty");
        }else if (player.numPatch >= player.maxItems)
        {
            Debug.Log("Full");
        }else
        {
            player.numPatch++;
            used = true;
        }
    }

}
