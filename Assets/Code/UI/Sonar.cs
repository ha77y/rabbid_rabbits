using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        
    }


    void Update()
    {
        rotation = player.transform.eulerAngles.y;
        sonarDirection.transform.rotation = Quaternion.Euler(0, 0, -rotation);

        Debug.Log(rotation);


        sonarSweep.transform.Rotate(0, 0, 1);
    }
}
