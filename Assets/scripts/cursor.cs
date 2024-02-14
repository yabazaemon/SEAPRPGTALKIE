using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor : MonoBehaviour
{
    public Image Mouse_Image;//画像
    public Canvas canvas;//canvasの変数
    public RectTransform canvasRect;//canvas内のRectTransform
    public Vector2 Mousepos;//マウス位置の最終的な格納先
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;//マウスポインターの非表示
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Mouse_Image = GameObject.Find("MouseImage").GetComponent<Image>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out Mousepos);
        Mouse_Image.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mousepos.x, Mousepos.y);
    }
}

