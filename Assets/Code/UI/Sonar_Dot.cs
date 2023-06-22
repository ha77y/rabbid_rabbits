using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonar_Dot : MonoBehaviour
{

    public Vector2 location;
    //public float playerRotation;
    public float lifespan = 3f;

    Vector3 distance;


    float span = 71.5f;
    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        float fade = remap(lifespan, 0, 3, 0, 1);
        GetComponent<Image>().color = new Color(1, 1, 1, fade);
    }

    public void initiate(Vector3 playerPos, Vector3 objectPos, float radius)
    {


        lifespan = 3f;
        distance = playerPos - objectPos;
        distance.x *= -1;
        distance.y = distance.z *= -1;
        distance.z = 0;

        distance.x = remap(distance.x, -radius, radius, -span, span);
        distance.y = remap(distance.y, -radius, radius, -span, span);

        GetComponent<Image>().GetComponent<RectTransform>().SetLocalPositionAndRotation(distance, Quaternion.identity);

    }

    public void destroy()
    {
        Destroy(gameObject);
    }



    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
