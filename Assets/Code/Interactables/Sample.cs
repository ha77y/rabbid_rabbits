using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : InteractableObject
{
    public override void Interact(Character player)
    {
        player.samples++;
        Destroy(gameObject);
    }
}
