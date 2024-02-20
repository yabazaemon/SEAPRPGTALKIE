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
    int h = 0;//1回だけ生成するのに使う

    //文末を知らせる点滅オブジェクト
    GameObject blink;
    private GameObject inst_blink;
    int k = 0;

    //文生成時と終わりのサウンド
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;

    //ボタン選択機能
    GameObject ButtonSelect;
    GameObject inst_ButtonSelect;
    int b = 0;//1回だけ生成するのに使う, ボタンの状態の管理を行う, 生成した瞬間に変化する
    int ba = 0;//ボタンをクリック(Destroy())した後に変化する
    int bs = 0;//b,baを0に戻すのに使う
    int bt_shokika = 0;
    int d = 0;

    //シーン移動時の暗転//用途によって使用するかしないかを「//」で分ける。　@@@@でこの関連の文を明記した
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    int f = 0;

    //使用する文の入れ物
    private Dictionary<int, string> dic;
    private Dictionary<int, string> yesBt_dic;
    private Dictionary<int, string> noBt_dic;
    private Dictionary<int, string> stage1_dic;
    private Dictionary<int, string> stage2_dic;

    //使用する関数の指定をするbool値群
    public bool NormalTalk;
    public bool ChangeSceneTalk;

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

    ////////使用済みのアルファベット//////(k, i, f, d)

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

        blink = (GameObject)Resources.Load("Blink");//点滅オブジェクト

        audioSource = this.GetComponent<AudioSource>();//文表示時のサウンド

        ButtonSelect= (GameObject)Resources.Load("ButtonSelect");

        Fade_Black = (GameObject)Resources.Load("Fade_Black");//暗転

        

        //Dictionaryには一応必ず一つ「""」を仕込むこと。Ex)使う文が4つ&&ボタンを使うのが4文目の場合、文生成のif文が終わってしまい会話が途切れてしまう。
        //つまり、ボタン選択の文の後にもう1文残すことによってボタン選択で変化する会話ができる。言ってしまえば、最終文が「""」である必要はないのだけど、たぶん。
        stage1_dic = new Dictionary<int, string>()
        {
            {1, "N.P,Kは植物の成長に必要な三大栄養素だよ"},
            {2, "土壌に含まれるNPKと作物に含有されるNPKにはやはり差がある"},
            {3, "このうち、Nにおいて一番大きな差がある"},
            {4, "NKが外部から供給されるのに対して、Pは必要量が少ないものの外部から供給されることがほとんどない"},
            {5, "つまり、植物が長期間栽培され続けると土壌中のPが不足してしまうんだ"},
            {6, "" }
        };

        stage2_dic = new Dictionary<int, string>()
        {
            {1, "喫煙による医療費1兆4902億円/年"},
            {2, "土壌に含まれるNPKと作物に含有されるNPKにはやはり差がある"},
            {3, "新版　土壌学の基礎"},
            {4, "NKが外部から供給されるのに対して、Pは必要量が少ないものの外部から供給されることがほとんどない"},
            {5, "肥料になった鉱物の物語"},
            {6, "" }
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
            {9, "なぜだかわからないけど、なんだか前を向いている感じがするなあ" },
            {11, "YESボタンを押しました" },
            {12, "Noボタンを押しました" },
            {10, "" }
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


        //引数の使い方(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定,ボタンは何文目？,Yesで使うDictionary,)
        if (NormalTalk == true)
        {
            ///ここにある2つのif文を1つに出来れば、同じboolの変数をUpdate内で使えるようになる。現状だと会話関数を一回使うごとに異なるboolの変数が必要。まだアイデアがない。
            if (Input.GetKeyDown(KeyCode.E) && tc.cat=="cat")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                cat = true;

            }
            if (cat == true)//とりあえずここでワンクッション入れた
            {
                normal_talk(tryRPG_dic,4, new int[] {2,3,4,10}, 0.04f, 0, 1, 2, tryRPG_dic, 1, new int[] { 11}, tryRPG_dic, 1, new int[] {12});//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("cat", 1);
                catf = PlayerPrefs.GetInt("cat");
                
                if (d==1 & b==0)
                {
                    PlayerPosition.yesClick = false;//staticのスクリプトPlayerPositionでボタンの状態を保存しているモノを元に戻してあげる
                    PlayerPosition.noClick = false;
                    d=0;
                }
               
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
                normal_talk(tryRPG_dic, 1, new int[] { 3 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 2, new int[] {8,9});//画像変更をしない場合、st_st_numとdel_numを0にしてください
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
                normal_talk(tryRPG_dic, 1, new int[] { 4 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] {8});//画像変更をしない場合、st_st_numとdel_numを0にしてください
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
                   normal_talk(tryRPG_dic, 1, new int[] { 1 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//画像変更をしない場合、st_st_numとdel_numを0にしてください
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
                    normal_talk(tryRPG_dic, 4, new int[] { 6, 7, 8, 9 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//画像変更をしない場合、st_st_numとdel_numを0にしてください

                }
                textStop = false;
                

            }
        }

    }







    //引数の使い方(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定,ボタンは何文目？,Yesで使うDictionary,)
    void normal_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num, int bt_st_num, Dictionary<int, string> yesBt_dic, int yesBt_use_st_count, int[] yesBt_use_st_no_key, Dictionary<int, string> noBt_dic, int noBt_use_st_count, int[] noBt_use_st_no_key)
    {//画像挿入では、挿入したい～文目から-1をした数を入れてください。ボタンも同様です。/////画像を消す～文目は-1をしなくて大丈夫です。

            if (textStop == false)
            {
                if (set_panel == 0)//関数開始時にPanalをスイッチオン, 使う文数をセット
                {
                    InputTexts = new string[use_st_count];
                    panel.SetActive(true);
                    set_panel++;
                }

                if(b==0 && ba==0)//ボタン選択をしないときの文生成
                {
                    for (int it = 0; it < use_st_count; it++)//使う文をDictionaryから読み込み・セット
                    {
                        InputTexts[it] = dic[use_st_no_key[it]];

                    }
                }
                if(b==1 && ba==1)//ボタン選択をするときの文生成
                {

                Debug.Log(PlayerPosition.noClick);
                Debug.Log(PlayerPosition.yesClick);
                if (PlayerPosition.yesClick==true)//YESボタンをクリックした場合
                    {
                        if (bt_shokika == 0)//ボタンを押された場合、初めに1回だけいろいろ値をセット, リセットしてあげる
                        {
                            InputTexts = new string[yesBt_use_st_count];
                            use_st_count = yesBt_use_st_count;
                            textCharKazu = 0;
                            st_num = 0;
                            displayedText = "";
                            i = 0;
                            k = 0;
                            h = 0;
                            bs = 1;
                            bt_shokika++;
                        }
                        for (int it = 0; it < yesBt_use_st_count; it++)//使う文をDictionaryから読み込み・セット
                        {
                            InputTexts[it] = yesBt_dic[yesBt_use_st_no_key[it]];
                        Debug.Log(11111);
                        }
                    }

                    if (PlayerPosition.noClick == true)//NOボタンをクリックした場合
                    {
                        if (bt_shokika == 0)//ボタンを押されたと場合、初めに1回だけいろいろ値をセット, リセットしてあげる
                        {
                            InputTexts = new string[noBt_use_st_count];
                            use_st_count = noBt_use_st_count;
                            textCharKazu = 0;
                            st_num = 0;
                            displayedText = "";
                            i = 0;
                            k = 0;
                            h = 0;
                            bs = 1;
                            bt_shokika++;
                        }
                        for (int it = 0; it < noBt_use_st_count; it++)//使う文をDictionaryから読み込み・セット
                        {
                            InputTexts[it] = noBt_dic[noBt_use_st_no_key[it]];
                        }
                    }
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
                            check_existence++;//画像がヒエラルキーに存在することをチェックしておきたいもの
                            h++;
                        }

                        if (i < textCharKazu)
                        {
                            audioSource.PlayOneShot(sound);
                            i++;
                        }
                    }
                    else//TextCharKazuが使う分の文字数と同じになった場合
                    {
                        if (st_num != InputTexts.Length - 1)//入力した文の数-1になるまでst_num++して文の移動
                        {

                            if (k == 0)
                            {
                                inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                                k++;
                            }

                            if (st_num == bt_st_num && b == 0)//////////////////////////////////////頭尾の文以外のボタン操作////////////////////////////////////////////////////////////
                            {
                                inst_ButtonSelect = Instantiate(ButtonSelect, new Vector3(398.5f, 154.5f, 0.0f), Quaternion.identity, canvas.transform);
                                b++;
                            }

                            if (click == true)
                            {
                                displayedText = "";
                                textCharKazu = 0;
                                k = 0;
                                Destroy(inst_blink);
                                i = 0;
                                st_num++;
                                //d = 0;
                                
                                if (st_num == del_num)
                                {
                                    Destroy(inst_sort_image1);//画像の削除
                                    check_existence = 0;
                                }
                                if(b==1)
                                {
                                    ba = 1;
                                }


                        }

                        }
                        else
                        {
                            if (k == 0)//「文末に生成されない問題」を解決するために書いてある
                            {
                                inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                                k++;
                            }

                            if (st_num == bt_st_num && b == 0)//「文末に生成されない問題」を解決するために書いてある//////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                inst_ButtonSelect = Instantiate(ButtonSelect, new Vector3(398.5f, 154.5f, 0.0f), Quaternion.identity, canvas.transform);
                                b++;
                            }

                            if (click == true)
                            {
                                //if (b == 1)
                                //{
                                //Destroy(inst_ButtonSelect);
                                //b = 0;
                                //}
                                displayedText = "";
                                textCharKazu = 0;
                                textStop = true;

                                dousunnda = false;//タグ関連のboolを全部ここでfalseにリセットする
                                cat = false;
                                ellipse = false;
                                sword = false;
                                ojisann = false;
                                ojisann1 = false;

                                st_num = 0;//ここで各値を元に戻すことで、再度関数を呼び出したときも初めからちゃんと動く
                                i = 0;
                                k = 0;
                                Destroy(inst_blink);
                                //b = 0;///////////最後の文終了後にbを初期化してる////////////////////////////////////////////////////////////////////
                                //ba = 1;
                                h = 0;

                                if (check_existence == 1)
                                {
                                    Destroy(inst_sort_image1);//画像の削除
                                    check_existence = 0;
                                }

                                this.GetComponent<Text>().text = "";
                                panel.SetActive(false);
                                set_panel = 0;
                                bt_shokika = 0;
                                //d = 0;


                            if (b==1)
                                {
                                    ba = 1;
                                }
                                if(bs==1)
                                {
                                b = 0;
                                ba = 0;
                                bs = 0;
                                }
                            }
                        }
                    }
                    click = false;
                }
                if (b == 1 & d==0)//ボタン選択の時のマウスを管理
                {

                    if (PlayerPosition.noClick | PlayerPosition.yesClick)
                    {
                        Destroy(inst_ButtonSelect);///////////////////////////////////////////////
                        d++;        
                    }
                    
                }
                if (d == 1)//上のif文にこれを入れてしまうと動かないので別にしてある
                {

                    if (Input.GetMouseButtonDown(0))
                    {

                        if (b != 0)///////////////////////////////////////////////////////////
                        {
                            PlayerPrefs.SetInt("button", 2);
                            int bt = PlayerPrefs.GetInt("button");
                            Debug.Log(bt);
                        }
                        click = true;
                        audioSource.PlayOneShot(sound1);
                        Destroy(inst_blink);
                        k = 0;

                }
            }

            if (b==0)// ボタン選択をしない時のマウスを管理
                {
                    if (Input.GetMouseButtonDown(0))
                    {

                        if (b != 0)///////////////////////////////////////////////////////////
                        {
                            PlayerPrefs.SetInt("button", 2);
                            int bt = PlayerPrefs.GetInt("button");
                        }
                        click = true;
                        audioSource.PlayOneShot(sound1);
                        Destroy(inst_blink);
                        k = 0;
                        //Debug.Log(b);

                    }
                }

            }
    }








    void stand_image_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定)
    {//画像挿入では、挿入したい～文目から-1をした数を入れてください。/////画像を消す～文目は-1をしなくて大丈夫です。
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

    IEnumerator Wait()//@@@@
    {

        for (float t = 0.01f; t < 0.5; t += Time.deltaTime)
        {
            if (f == 0)
            {
                //inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
                f++;
            }


            yield return null;
        }

       //SceneManager.LoadScene("talk_scene");//シーン移動

    }
}
