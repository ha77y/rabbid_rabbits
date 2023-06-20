using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStation : InteractableObject
{
    public override void Interact(Character player)
    {
        Debug.Log("HealthInteract");
    }
}

