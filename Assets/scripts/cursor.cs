using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursor : MonoBehaviour
{
    public Image Mouse_Image;//�摜
    public Canvas canvas;//canvas�̕ϐ�
    public RectTransform canvasRect;//canvas����RectTransform
    public Vector2 Mousepos;//�}�E�X�ʒu�̍ŏI�I�Ȋi�[��
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;//�}�E�X�|�C���^�[�̔�\��
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

