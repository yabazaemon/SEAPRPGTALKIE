using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventCtrl : MonoBehaviour
{

    public ControlAllTexst controlAllTexst;

    private TouchCheck tc = null;// 接触を取得

    //練習用//フラグ、Dictionaryを含む//
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
        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();//会話するための当たり判定

        PlayerPrefs.SetInt("cat", 0);////ここは製作中に何度でもフラグを使って会話ができるようにStart()で全て１回統一している。
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
        ///ここにある2つのif文を1つに出来れば、同じboolの変数をUpdate内で使えるようになる。現状だと会話関数を一回使うごとに異なるboolの変数が必要。まだアイデアがない。
        



    }
}
