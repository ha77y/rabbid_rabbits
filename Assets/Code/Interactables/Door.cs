using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Door : InteractableObject
{

    public override void Interact(Character player)
    {
        if(GetTag == "KeycardDoor")
        {
            if (player.hasKeycard)
            {
                Debug.Log("SwitchMovement");
                //player.movementState.switchMovement(player);
                player.doorMove(this);
            }
        }
        else
        {
            Debug.Log("SwitchMovement");
            //player.movementState.switchMovement(player);
            player.doorMove(this);
        }


    }

    public void pass(Character player, bool enter)
    {
        StartCoroutine(Pass(this, player, enter));
    }

    IEnumerator Pass(Door door, Character player, bool enter)
    {

        float time = 0;
        float duration = 1;
        while (time < duration)
        {
            //delay
            time += Time.deltaTime;
            yield return null;
        }


        duration = 1.5f;
        time = 0;
        Transform rotatePoint = transform.GetChild(1);
        Transform cylinder = transform.GetChild(0);

        Vector3 playerStartPos = player.transform.position;


        while (time < duration)
        {
            time += Time.deltaTime;
            cylinder.RotateAround(rotatePoint.position, Vector3.up, -90/duration * Time.deltaTime);
            yield return null;           
        }
        duration = 0.75f;
        time = 0;
        while (time < duration)
        {
            //Delay
            time += Time.deltaTime;
            yield return null;
        }

        duration = 0.75f;
        time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            cylinder.RotateAround(rotatePoint.position, Vector3.up, 90/duration * Time.deltaTime);
            yield return null;
            yield return null;
        }

    }


}
