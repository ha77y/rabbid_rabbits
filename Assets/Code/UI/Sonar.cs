using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
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


    public RaycastHit[] Hits;
    public Vector3 boxSize;
    void Start()
    {
        boxSize = new Vector3(1, 400, 0.1f);
    }


    void Update()
    {
        rotation = player.transform.eulerAngles.y;
        sonarDirection.transform.rotation = Quaternion.Euler(0, 0, -rotation);
        sonarSweep.transform.Rotate(0, 0, 1);
        Hits = BoxCastAll(player.transform,boxSize)
    }
}
