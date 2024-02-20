using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonYes : MonoBehaviour
{
   public void OnClick()
    {
        PlayerPosition.yesClick=true;
        Debug.Log("yes");
    }
}
