using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNo : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPosition.noClick = true;
        Debug.Log("no");
    }
}
