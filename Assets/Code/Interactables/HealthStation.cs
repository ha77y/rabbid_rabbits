using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStation : InteractableObject
{
    public override void Interact(Character player)
    {
        if (player.health < 100)
        {
            player.health = 100;
            Destroy(gameObject);
        }
    }
}

