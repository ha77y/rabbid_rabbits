using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : InteractableObject
{
    public override void Interact(Character player)
    {
        player.hasKeycard = true;
        Destroy(gameObject);
    }
}
