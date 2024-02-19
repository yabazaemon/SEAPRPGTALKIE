using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonYes : MonoBehaviour
{
   public void OnClick()
    {
        PlayerPosition.yesClick=true;
        Debug.Log("yes");
    }
}
