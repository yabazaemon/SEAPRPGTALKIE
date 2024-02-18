using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlAllTexst : MonoBehaviour
{

    bool dousunnen = false;
    bool dousunnda = false;
    private TouchCheck tc = null;
    private Rigidbody2D rb = null;
    GameObject player = null;

    //stand_image_talk関数で使うもの
    GameObject talk_background;
    int set_background = 0;

    //UIのPanelオブジェクトのOnOff
    GameObject panel = null;
    int set_panel = 0;

    //テキスト送り
    private string[] InputTexts;//unityから入力する文
    int st_num = 0;//入力した文の数を扱う
    string displayedText = "";//表示される文
    int textCharKazu = 0;//文の文字数
    float displayedTextSpeed = 0.0f;
    bool click = true;
    bool textStop;

    //フラグ関係
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //画像変更
    //public int st_st_num;//画像を挿入する文は何文目か？を指定する(sorting sentsnce number)
    //public int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;
    int check_existence = 0;
    int h = 0;

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
    private Dictionary<int, string> stage1_dic;
    private Dictionary<int, string> stage2_dic;

    //使用する関数の指定をするbool値群
    public bool NormalTalk;
    public bool ChangeSceneTalk;
    public bool BattleSceneSentence;

    //練習用//
    private Dictionary<int, string> tryRPG_dic;
    bool cat = false;
    bool ellipse = false;
    bool sword = false;
    bool ojisann=false;
    bool ojisann1 = false;
    int catf;
    int ellipsef;
    int swordf;
    int ojisanf;

    ////////使用済みのアルファベット//////(k, i, f)

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        player = GameObject.Find("前向き");

        talk_background = GameObject.Find("talk_background");
        talk_background.SetActive(false);//ゲーム開始時の暗転でstaticにして、stand_image_talk関数内でtrue・falseにする

        panel = GameObject.Find("Panel");
        panel.SetActive(false);//ゲーム開始時の暗転でPanelをstaticにする。暗転中にstaticにしなくてよい、何か良い方法がないですかね。一度ゲームを開始したら関係ないのですけど。

        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();

        PlayerPrefs.SetInt("cat", 0);
        PlayerPrefs.SetInt("ellipse", 0);
        PlayerPrefs.SetInt("sword", 0);
        PlayerPrefs.SetInt("ojisan", 0);
        ojisanf = PlayerPrefs.GetInt("ojisan");
        ellipsef = PlayerPrefs.GetInt("ellipse");
        swordf = PlayerPrefs.GetInt("sword");
        catf = PlayerPrefs.GetInt("cat");

        sort_image1 = (GameObject)Resources.Load("Image");//挿入する画像のプレハブ選択。Animationを使用する場合はプレハブにセットして、プレハブのスクリプトから操作する。
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        blink = (GameObject)Resources.Load("Blink");

        audioSource = this.GetComponent<AudioSource>();

        Fade_Black = (GameObject)Resources.Load("Fade_Black");//@@@@

        


        stage1_dic = new Dictionary<int, string>()
        {
            {1, "N.P,Kは植物の成長に必要な三大栄養素だよ"},
            {2, "土壌に含まれるNPKと作物に含有されるNPKにはやはり差がある"},
            {3, "このうち、Nにおいて一番大きな差がある"},
            {4, "NKが外部から供給されるのに対して、Pは必要量が少ないものの外部から供給されることがほとんどない"},
            {5, "つまり、植物が長期間栽培され続けると土壌中のPが不足してしまうんだ"}
        };

        stage2_dic = new Dictionary<int, string>()
        {
            {1, "喫煙による医療費1兆4902億円/年"},
            {2, "土壌に含まれるNPKと作物に含有されるNPKにはやはり差がある"},
            {3, "新版　土壌学の基礎"},
            {4, "NKが外部から供給されるのに対して、Pは必要量が少ないものの外部から供給されることがほとんどない"},
            {5, "肥料になった鉱物の物語"}
        };

        tryRPG_dic = new Dictionary<int, string>()
        {
            {1, "やあこんにちは、全ての人に話しかけてね"},
            {2, "にゃー、ゴロゴロ、シャー！！！"},
            {3, "パンツ一丁はさすがに寒いなー（笑"},
            {4, "I'm a ellipse."},
            {5, "......全てのNPCと話してきて、と言ったはずだけど。"},
            {6, "よーし、やっと終わったか。" },
            {7, "君の名前はなんていうんだい？"},
            {8, "そうか、「前向き」というんだな" },
            {9, "なぜだかわからないけど、なんだか前を向いている感じがするなあ" }
        };

    }

    // Update is called once per frame
    void Update()
    {



        

        /////////////一見関数の呼び出しに問題がないのに、「範囲外です」みたいなエラーが出るときは、bool値の変数に問題がある

        if (ChangeSceneTalk == true)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && tc.isTouch)//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                dousunnen = true;

            }
            if (dousunnen == true)//とりあえずここでワンクッション入れた
            {
                stand_image_talk(stage1_dic, 3, new int[] { 2, 1, 1 }, 0.04f, 0, 3);//画像変更をしない場合、st_st_numとdel_numを0にしてください

            }
            textStop = false;

        }

        
        
        if (NormalTalk == true)
        {
            ///ここにある2つのif文を1つに出来れば、同じboolの変数をUpdate内で使えるようになる。現状だと会話関数を一回使うごとに異なるboolの変数が必要。まだアイデアがない。
            if (Input.GetKeyDown(KeyCode.E) && tc.cat=="cat")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                cat = true;

            }
            if (cat == true)//とりあえずここでワンクッション入れた
            {
                normal_talk(tryRPG_dic,1, new int[] {2}, 0.04f, 5, 5);//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("cat", 1);
                catf = PlayerPrefs.GetInt("cat");
            }
            textStop = false;
            
            Debug.Log(catf);


        }
        


        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        if (NormalTalk == true)
        {

            if (Input.GetKeyDown(KeyCode.E) && tc.sword=="sword")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                sword = true;

            }
            if (sword == true)//とりあえずここでワンクッション入れた
            {
                normal_talk(tryRPG_dic, 1, new int[] { 3 }, 0.04f, 5, 5);//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("sword", 1);
                swordf = PlayerPrefs.GetInt("sword");
            }
            textStop = false;
            
            Debug.Log(swordf);
        }
        //}


        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        if (NormalTalk == true)
        {

            if (Input.GetKeyDown(KeyCode.E) && tc.ellipse=="ellipse")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                ellipse = true;

            }
            if (ellipse == true)//とりあえずここでワンクッション入れた
            {
                normal_talk(tryRPG_dic, 1, new int[] { 4 }, 0.04f, 5, 5);//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("ellipse", 1);
                ellipsef = PlayerPrefs.GetInt("ellipse");
            }
            textStop = false;
            
            Debug.Log(ellipsef);

        }
        //}


        if (catf + ellipsef + swordf + ojisanf < 4)
        {
            if (NormalTalk == true)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//ここのifに使う関数を直接入れるとなんかうまく動かない
                {
                    ojisann = true;

                }
                if (ojisann == true)//とりあえずここでワンクッション入れた
                {
                   normal_talk(tryRPG_dic, 1, new int[] { 1 }, 0.04f, 5, 5);//画像変更をしない場合、st_st_numとdel_numを0にしてください
                   PlayerPrefs.SetInt("ojisan", 1);
                   ojisanf = PlayerPrefs.GetInt("ojisan");
                }
                textStop = false;
               
                Debug.Log(ojisanf);
            }
        }


        if(catf==1 &&  ellipsef==1 && swordf==1 && ojisanf==1)
        {
            if (NormalTalk == true)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//ここのifに使う関数を直接入れるとなんかうまく動かない
                {
                    ojisann1 = true;

                }
                if (ojisann1 == true)//とりあえずここでワンクッション入れた
                {
                    normal_talk(tryRPG_dic, 4, new int[] { 6, 7, 8, 9 }, 0.04f, 5, 5);//画像変更をしない場合、st_st_numとdel_numを0にしてください

                }
                textStop = false;
                

            }
        }

    }


    void normal_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定)
    {//画像挿入では、挿入したい〜文目から-1をした数を入れてください。/////画像を消す〜文目は-1をしなくて大丈夫です。

        if (textStop == false)
        {
            if (set_panel == 0)//関数開始時にPanalをスイッチオン, 使う文数をセット
            {
                InputTexts = new string[use_st_count];
                panel.SetActive(true);
                set_panel++;
            }

            for (int it = 0; it < use_st_count; it++)//使う文をDictionaryから読み込み・セット
            {
                InputTexts[it] = dic[use_st_no_key[it]];
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

                    if (st_num == st_st_num && h==0)
                    {
                        inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//画像の表示
                        check_existence++;
                        h++;
                    }

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
                            k = 0;
                            Destroy(inst_blink);
                            i = 0;
                            st_num++;
                            if (st_num == del_num)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                                check_existence = 0;
                            }
                            
                        }


                    }
                    else
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
                            textStop = true;

                            dousunnda = false;//タグ関連のboolを全部ここでfalseにリセットする
                            cat = false;
                            ellipse = false;
                            sword = false;
                            ojisann= false;
                            ojisann1 = false;

                            st_num = 0;//ここで各値を元に戻すことで、再度関数を呼び出したときも初めからちゃんと動く
                            i = 0;
                            k = 0;
                            Destroy(inst_blink);
                            h =0;
                            if (check_existence==1)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                                check_existence = 0;
                            }
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


    void stand_image_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定)
    {//画像挿入では、挿入したい〜文目から-1をした数を入れてください。/////画像を消す〜文目は-1をしなくて大丈夫です。
        if (set_background==0)
        {
            PlayerPosition.halt = true;
            InputTexts = new string[use_st_count];
            talk_background.SetActive(true);
            set_background++;
        }
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

                    if (st_num == st_st_num && h == 0)
                    {
                        inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//画像の表示
                        check_existence++;
                        h++;
                    }

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

                            if (st_num == del_num)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                            }
                        }


                    }
                    else
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
                            textStop = true;
                            dousunnen = false;//ここで各値を元に戻すことで、再度関数を呼び出したときも初めからちゃんと動く
                            st_num = 0;
                            i = 0;
                            k = 0;
                            Destroy(inst_blink);
                            h = 0;
                            if (check_existence == 1)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                                check_existence = 0;
                            }
                            this.GetComponent<Text>().text = "";
                            panel.SetActive(false);
                            set_panel = 0;
                            talk_background.SetActive(false);
                            set_background = 0;
                            PlayerPosition.halt = false;


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



    


    IEnumerator FadeIn()//@@@@
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            if (f == 0)
            {
                inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
                f++;
            }


            yield return null;
        }

        SceneManager.LoadScene("talk_scene");//シーン移動

    }
}
