using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    public bool isTouch = false;
    public string touch;
    public string cat;
    public string ellipse;
    public string sword;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "touch")
        {
            touch="touch";
            //isTouch= true;
        }
        if (col.tag == "cat")
        {
            cat="cat";
        }
        if (col.tag == "ellipse")
        {
            ellipse = "ellipse";
        }
        if (col.tag == "sword")
        {
            sword="sword";
        }
    }
     void OnTriggerExit2D(Collider2D col)
    { 
        if (col.tag=="touch")
        {
            touch=null;
            //isTouch= false;
        }
        if (col.tag == "cat")
        {
            cat=null;
        }
        if (col.tag == "ellipse")
        {
            ellipse=null;
        }
        if (col.tag == "sword")
        {
            sword=null;
        }

    }



    
}
