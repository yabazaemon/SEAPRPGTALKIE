using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventCtrl : MonoBehaviour
{

    public ControlAllTexst controlAllTexst;

    private TouchCheck tc = null;// �ڐG���擾

    //���K�p//�t���O�ADictionary���܂�//
    public Dictionary<int, string> tryRPG_dic;
    bool cat = false;
    bool ellipse = false;
    bool sword = false;
    bool ojisann = false;
    bool ojisann1 = false;
    int catf;
    int ellipsef;
    int swordf;
    int ojisanf;


    // Start is called before the first frame update
    void Start()
    {
        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();//��b���邽�߂̓����蔻��

        PlayerPrefs.SetInt("cat", 0);////�����͐��쒆�ɉ��x�ł��t���O���g���ĉ�b���ł���悤��Start()�őS�ĂP�񓝈ꂵ�Ă���B
        PlayerPrefs.SetInt("ellipse", 0);
        PlayerPrefs.SetInt("sword", 0);
        PlayerPrefs.SetInt("ojisan", 0);
        ojisanf = PlayerPrefs.GetInt("ojisan");
        ellipsef = PlayerPrefs.GetInt("ellipse");
        swordf = PlayerPrefs.GetInt("sword");
        catf = PlayerPrefs.GetInt("cat");
    }

    // Update is called once per frame
    void Update()
    {
        ///�����ɂ���2��if����1�ɏo����΁A����bool�̕ϐ���Update���Ŏg����悤�ɂȂ�B���󂾂Ɖ�b�֐������g�����ƂɈقȂ�bool�̕ϐ����K�v�B�܂��A�C�f�A���Ȃ��B
        



    }
}
