using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public bool isTouch = false;
    private string touchTag = "touch";

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "touch")
        {
            isTouch = true;
        }
    }

     void OnTriggerExit2D(Collider2D col)
    { 
        if (col.tag=="touch")
        {
            isTouch = false;
        }
    }
}
