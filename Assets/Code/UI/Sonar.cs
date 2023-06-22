using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar : MonoBehaviour
{

    public Character player;

    public float rotation = 0;

    public Image sonarSweep;
    public Image sonarDirection;
    // Start is called before the first frame update

    public float sonarRadius = 100f;

    public RaycastHit[] Hits;
    public Vector3 boxSize;

    List<Transform> recognised = new List<Transform>();
    List<GameObject> dots = new List<GameObject>();
    List<Transform> currentHits = new List<Transform>(); 

    public GameObject dotPF;
    public float angle = 0;

    public float sonarSpeed = 0.5f;
    /// less is longer

    public GameObject sonarBase;
    void Start()
    {
        boxSize = new Vector3(0.1f, 400, 0.1f) ;
        //transform.Rotate(0, 180, 0);
    }


    void Update()
    {

        rotation = player.transform.eulerAngles.y;
        sonarDirection.transform.rotation = Quaternion.Euler(0, 0, -rotation);
        sonarSweep.transform.Rotate(0, 0, sonarSpeed);

        transform.position = player.transform.position;
        transform.Rotate(0, -sonarSpeed, 0);

        Hits = Physics.BoxCastAll(player.transform.position, boxSize, transform.forward, transform.rotation, sonarRadius);


        currentHits.Clear();

    for (int i = 0; i < Hits.Length; i++)
    {
        if (Hits[i].transform.tag == "Sonar")
        {
            currentHits.Add(Hits[i].transform);
        }
    }

    for (int i = 0; i < currentHits.Count; i++)
    {
        for (int j = 0; j < recognised.Count; j++)
        {

            if (currentHits[i].transform == recognised[j])
            {
                dots[j].GetComponent<Sonar_Dot>().initiate(transform.position, recognised[j].position, sonarRadius);
                return;
            }
            
        }

        //Debug.Log(currentHits.Count);
        recognised.Add(Hits[i].transform);
        dots.Add(Instantiate(dotPF, sonarBase.transform));
        dots[dots.Count - 1].GetComponent<Sonar_Dot>().initiate(transform.position, recognised[recognised.Count - 1].position, sonarRadius);
    }

    for (int i = 0; i < recognised.Count; i++)
    {

        //Check if expired
        if (dots[i].GetComponent<Sonar_Dot>().lifespan <= 0)
        {
            dots[i].GetComponent<Sonar_Dot>().destroy();
            dots.RemoveAt(i);
            recognised.RemoveAt(i);
        }

    }

}
 
}
