using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewestNormalTalk : MonoBehaviour
{
  
    bool dousunnen = false;
    bool dousunnda = false;
    private TouchCheck tc = null;
    private Rigidbody2D rb = null;
    GameObject player = null;

    //UIのPanelオブジェクトのOnOff
    GameObject panel = null;
    int set_panel = 0;

    //テキスト送り
    private string[] InputTexts;//unityから入力する文
    int st_num = 0;//入力した文の数を扱う
    string displayedText = "";//表示される文
    int textCharKazu = 0;//文の文字数
    float displayedTextSpeed = 0.0f;
    private float st_speed = 0.02f;//この値を大きくするとゆっくりになる
    bool click = true;
    bool textStop;
    public int use_st_count;//使用する文の数,Inspectorから指定できる。
    public int[] use_st_no_key;//使用する文に割り当てられたキーを入れる配列,Inspectorから指定できる。


    //フラグ関係
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //画像変更
    public int st_st_num;//画像を挿入する文は何文目か？を指定する(sorting sentsnce number)
    public int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;

    //文末を知らせる点滅オブジェクト
    GameObject blink;
    private GameObject inst_blink;
    int k = 0;

    //文生成時と終わりのサウンド
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;

    //シーン移動時の暗転//用途によって使用するかしないかを「//」で分ける。　@@@@でこの関連の文を明記した
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    int f = 0;

    //使用する文の入れ物
    private Dictionary<int, string> dic;

    //使用する関数の指定をするbool値群
    public bool NormalTalk;
    public bool ChangeSceneTalk;
    public bool BattleSceneSentence;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("前向き");

        panel = GameObject.Find("Panel");
        panel.SetActive(false);//ゲーム開始時の暗転でPanelをstaticにする。暗転中にstaticにしなくてよい、何か良い方法がないですかね。一度ゲームを開始したら関係ないのですけど。

        //tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();

        InputTexts = new string[use_st_count];//使用したい文の数を代入

        flag1 = PlayerPrefs.GetInt("Key1");
        flag2 = PlayerPrefs.GetInt("Key2");

        sort_image1 = (GameObject)Resources.Load("Image");//挿入する画像のプレハブ選択。Animationを使用する場合はプレハブにセットして、プレハブのスクリプトから操作する。
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        blink = (GameObject)Resources.Load("Blink");

        audioSource = this.GetComponent<AudioSource>();

        Fade_Black = (GameObject)Resources.Load("Fade_Black");//@@@@


        dic = new Dictionary<int, string>()
        {
            {1, "N.P,Kは植物の成長に必要な三大栄養素だよ"},
            {2, "土壌に含まれるNPKと作物に含有されるNPKにはやはり差がある"},
            {3, "このうち、Nにおいて一番大きな差がある"},
            {4, "NKが外部から供給されるのに対して、Pは必要量が少ないものの外部から供給されることがほとんどない"},
            {5, "つまり、植物が長期間栽培され続けると土壌中のPが不足してしまうんだ"}
        };



    }

    // Update is called once per frame
    void Update()
    {

       

        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        if (NormalTalk == true)
        {
            tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();
            if (Input.GetKeyDown(KeyCode.R) && tc.isTouch)//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                dousunnda = true;

            }
            if (dousunnda == true)//とりあえずここでワンクッション入れた
            {
                normal_talk(5,new int[] {2,1,1,3,4}, 0.04f, 0, 0);//画像変更をしない場合、st_st_numとdel_numを0にしてください

            }
            textStop = false;
        }
        //}

        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        if (ChangeSceneTalk == true)
        {
            tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();
            if (Input.GetKeyDown(KeyCode.Q) && tc.isTouch)
            {
                dousunnen = true;

            }
            if (dousunnen == true)
            {
                change_scene_talk();

            }
        }
        //}

        
    }


    void normal_talk(int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定)
    {
        
        if (textStop == false)
        {

            for (int it = 0; it < use_st_count; it++)
            {
                InputTexts[it] = dic[use_st_no_key[it]];
            }
            if (set_panel == 0)
            {
                panel.SetActive(true);
                set_panel++;
            }


            displayedTextSpeed += Time.deltaTime;
            if (displayedTextSpeed > st_speed)
            {
                this.GetComponent<Text>().text = displayedText;
                displayedTextSpeed = 0.0f;
                if (textCharKazu != InputTexts[st_num].Length)//Update毎に文の文字を1文字ずつdisplayedTextに入れる
                {
                    displayedText = displayedText + InputTexts[st_num][textCharKazu];//
                    textCharKazu++;
                    if (i < textCharKazu)
                    {
                        audioSource.PlayOneShot(sound);
                        i++;
                    }
                }
                else
                {
                    if (st_num != InputTexts.Length - 1)//入力した文の数-1になるまでst_num++して文の移動
                    {

                        if (k == 0)
                        {
                            inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                            k++;
                        }


                        if (click == true)
                        {
                            displayedText = "";
                            textCharKazu = 0;
                            st_num++;
                            k = 0;
                            Destroy(inst_blink);
                            i = 0;


                            if (st_num == st_st_num)
                            {
                                inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//画像の表示

                            }

                            if (st_num == del_num)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                            }
                        }


                    }
                    else
                    {
                        if (click == true)
                        {
                            displayedText = "";
                            textCharKazu = 0;
                            textStop = true;//ここで各値を元に戻すことで、再度関数を呼び出したときも初めからちゃんと動く
                            dousunnda = false;
                            st_num = 0;
                            i = 0;
                            this.GetComponent<Text>().text = "";
                            panel.SetActive(false);
                            set_panel = 0;



                        }
                    }
                }


                click = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                click = true;
                audioSource.PlayOneShot(sound1);
                Destroy(inst_blink);
                k = 0;


            }
        }


    }




    

    void change_scene_talk()
    {
        StartCoroutine(FadeIn());

        rb = player.GetComponent<Rigidbody2D>();
        Vector2 pos = rb.position;
        PlayerPosition.position = rb.position;
    }


    IEnumerator FadeIn()//@@@@
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            if (f==0)
            {
                inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
                f++;
            }


            yield return null;
        }

        SceneManager.LoadScene("talk_scene");//シーン移動

    }
}
//使える機能///////////////////////////////////////////////////////////////////////////////////////////////////////
//テキスト送り
//画像変更
//文字表示時のサウンドと、文終了時にするクリックでのサウンド
//ゲーム開始時の暗転、シーン移動時の暗転
//文の最後には、文末を知らせるための点滅するオブジェクト
//Dictionaryを参照することで任意の文を指定・表示できる
//テキスト送りのプログラムを関数にして、フラグ使用時に使いやすそうにしてみた(フラグ管理の経験はない)。
//シーン移動時にPlayerのpositionをstaticのスクリプトで保存して、シーンを戻った際に元の場所(座標)から再開できる。
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//留意点
//　1つのスクリプトで全ての会話を一元管理することはできていない。
//　Playerの座標を保存する機能のところでアウト(シーン遷移をする会話がなければ問題ない)
//　
//　NormalTalkはNormalTalkで使う文章を参照できるようにすればよいのではないかと思う。
//　シーン遷移を伴う会話では、それのDictionaryを参照する。文数が多くなった際に、いちいち文を探してキーを割り当てることが面倒くさそう。
//　とりあえず、NormalTalkだけを使うのであれば問題はないと思います。
//　関数を呼び出すときの文をもう少しコンパクトにしたいのですが、良い案ないでしょうか?
