using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchStation : InteractableObject
{
    public override void Interact(Character player) {
        Debug.Log("PatchInteract");
    }

}
