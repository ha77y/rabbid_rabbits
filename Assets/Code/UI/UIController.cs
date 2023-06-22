using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Character player;

    public GameObject healthBar;

    public GameObject oxygenBar;

    public GameObject samples;

    public GameObject flares;

    public GameObject patch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.GetComponent<Image>().fillAmount = remap(player.health, 0, 100, 0, 1);
        oxygenBar.GetComponent<Image>().fillAmount = remap(player.oxygen, 0, 100, 0, 1);
        samples.GetComponent<TMPro.TextMeshProUGUI>().text = player.samples.ToString();
        flares.GetComponent<TMPro.TextMeshProUGUI>().text = player.numFlare.ToString();
        patch.GetComponent<TMPro.TextMeshProUGUI>().text = player.numPatch.ToString();
    }

    public float remap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        return Mathf.Lerp(bLow, bHigh, normal);
    }
}
