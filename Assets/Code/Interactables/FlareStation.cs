using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareStation : InteractableObject
{
    public override void Interact(Character player)
    {
        Debug.Log("FlareInteract");
    }
}
