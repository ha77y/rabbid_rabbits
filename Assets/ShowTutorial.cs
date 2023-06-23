using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public void Show(GameObject go)
    {
        go.SetActive(true);
    } 
    
    public void Hide(GameObject go)
    {
        go.SetActive(false);
    }
}
