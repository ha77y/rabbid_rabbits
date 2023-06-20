using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{

    public override void Interact(Character player)
    {
        Debug.Log("SwitchMovement");
        //player.movementState.switchMovement(player);
        player.doorMove(this);
    }


    public void open(Character player, bool enter)
    {
        StartCoroutine(Open(this, player, enter));
    }

    public void close()
    {
        StartCoroutine(Close(this));
    }

    IEnumerator Open(Door door, Character player, bool enter)
    {
        float duration = 1.5f;
        float time = 0;
        Transform rotatePoint = transform.GetChild(1);
        Transform cylinder = transform.GetChild(0);

        Vector3 playerStartPos = player.transform.position;


        while (time < duration)
        {
            //if (enter && time > 0.3f && time < 0.6f)
            //{
            //    player.transform.position = Vector3.Lerp(playerStartPos, playerStartPos + player.transform.forward * -1f, time-0.4f / 0.6f);
            //}
            time += Time.deltaTime;
            cylinder.RotateAround(rotatePoint.position, Vector3.up, -90/duration * Time.deltaTime);
            yield return null;           
        }
        player.doorEnter(player, door, enter);
    }

    IEnumerator Close(Door door)
    {
        Debug.Log("Closing");
        float time = 0;
        float duration = 0.75f;

        Transform rotatePoint = transform.GetChild(1);
        Transform cylinder = transform.GetChild(0);

        while (time < duration)
        {
            time += Time.deltaTime;
            cylinder.RotateAround(rotatePoint.position, Vector3.up, 90/duration * Time.deltaTime);
            yield return null;
        }

    }


}
