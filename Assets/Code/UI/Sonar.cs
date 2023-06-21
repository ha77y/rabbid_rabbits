using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{

    public Character player;

    public float rotation = 0;

    public Image sonarBase;

    public Image sonarSweep;
    public Image sonarDirection;
    // Start is called before the first frame update

    public float sonarRadius = 50f;

    public RaycastHit[] Hits;
    public Vector3 boxSize;


    public Quaternion orientation;
    public Vector3 direction;

    void Start()
    {
        boxSize = new Vector3(1, 400, 0.1f);
    }


    void Update()
    {

        rotation = player.transform.eulerAngles.y;
        sonarDirection.transform.rotation = Quaternion.Euler(0, 0, -rotation);
        sonarSweep.transform.Rotate(0, 0, 1);

        orientation.eulerAngles = new Vector3(player.lookDirection.x, 0, 0);
        //Debug.Log(player.lookDirection.x);

        //direction = new Vector3(0, 0, player.lookDirection.x);


        Hits = Physics.BoxCastAll(player.transform.position, boxSize, direction, orientation, sonarRadius);
        for (int i = 0; i < Hits.Length; i++)
        {
            RaycastHit hit = Hits[i];
            Debug.Log(Hits[i].transform);
        }
    }
}
