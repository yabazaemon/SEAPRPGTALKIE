using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class ControlAllTexst : MonoBehaviour
{
    public int talk_finish=0;
    bool dousunnen = false;
    bool dousunnda = false;
    //private Rigidbody2D rb = null;
    //GameObject player = null;

    //立ち絵での会話で使うもので使うもの
    GameObject talk_background;
    Image image_talk_background;
    int set_background = 0;
    int whether_use_stand = 0;

    //UIのPanelオブジェクトのOnOff
    GameObject panel = null;
    int set_panel = 0;
    Image panel_image = null;

    //テキスト送り
    private string[] InputTexts;//入力する文
    int st_num = 0;//入力した文の数を扱う
    string displayedText = "";//表示される文
    int textCharKazu = 0;//文の文字数
    float displayedTextSpeed = 0.0f;
    bool click = true;
    bool textStop;

    //接触を取得
    private TouchCheck tc = null;

    //フォントサイズ変更機能
    int normal_fontsize = 20;
    int e = 0;//通常のフォントサイズに戻すときに使う
    int normal_yes_no = 0;//normal=0//yes==1//no==2//
    public Fontsize_parameters fontsize_parameters;
    int check_st_num = 0;//change_fontsize()で使用
    int check_char_num = 0;//change_fontsize()で使用
    public int once = 0;//構造体の中の変数を1回だけ、関数呼び出し直前に値を変えるために使用


    //フラグ関係
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //画像変更
    //int st_st_num;//画像を挿入する文は何文目か？を指定する(sorting sentsnce number)
    //int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;
    int check_existence = 0;//画像があるかないかを取得
    int h = 0;//1回だけ生成するのに使う

    int bun = 0;
    int insort_img__retsu = 0;
    //int use_img_num = 0;
    GameObject[][] setti;
    int bunbun = 0;
    int del_img_retsu = 0;
    public image_ctrl_parameters image_Ctrl_Parameters;
    int count_gyo = 0;
    int shokkika = 0;
    int ikkaidake = 0;
    //文末を知らせる点滅オブジェクト
    GameObject blink;
    private GameObject inst_blink;
    int k = 0;

    //文生成時と終わりのサウンド
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;
    public AudioClip select_sound;

    //ボタン選択機能
    GameObject ButtonSelect;
    GameObject inst_ButtonSelect;
    int b = 0;//1回だけ生成するのに使う, ボタンの状態の管理を行う, 生成した瞬間に変化する
    int ba = 0;//ボタンをクリック(Destroy())した後に変化する
    int bs = 0;//b,baを0に戻すのに使う
    int bt_shokika = 0;
    int d = 0;

    //暗転
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    int f = 0;

    //使用する文の入れ物
    private Dictionary<int, string> stage1_dic;
    private Dictionary<int, string> stage2_dic;

    //使用する関数の指定をするbool値群
    //public bool NormalTalk;

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

    //hihihih//
    GameObject g1 = null;
    GameObject g2 = null;
    GameObject g3 = null;
    GameObject g4 = null;


    ////////使用済みのアルファベット(グローバルな奴)//////(k, i, f, d)

    // Start is called before the first frame update
    void Start()
    {

        image_Ctrl_Parameters.st_st_num = new int[] { 0, 1 };
        image_Ctrl_Parameters.img_position = new Vector3[][] { new Vector3[] { new Vector3(672f, 230f, 0.0f), new Vector3(672f, 230f, 0.0f) }, new Vector3[] { new Vector3(672f, 230f, 0.0f), new Vector3(672f, 230f, 0.0f) } };
        image_Ctrl_Parameters.TansuOfImage = new GameObject[][] { new GameObject[] { g1, g2 }, new GameObject[] { g3, g4 } };
        image_Ctrl_Parameters.use_img_num = 2;
        image_Ctrl_Parameters.del_num = new int[][] { new int[] { 1, 1 }, new int[] { 2, 2 } };
        image_Ctrl_Parameters.set = new GameObject[image_Ctrl_Parameters.TansuOfImage.GetLength(0), image_Ctrl_Parameters.use_img_num];

        image_Ctrl_Parameters.Yst_st_num = new int[] { 0 };
        image_Ctrl_Parameters.Yimg_position = new Vector3[][] { new Vector3[] { new Vector3(672f, 230f, 0.0f), new Vector3(672f, 230f, 0.0f) }};
        image_Ctrl_Parameters.YTansuOfImage = new GameObject[][] { new GameObject[] { g1, g1 }};
        image_Ctrl_Parameters.Yuse_img_num = 2;
        image_Ctrl_Parameters.Ydel_num = new int[][] { new int[] { 0, 0 }};
        image_Ctrl_Parameters.Yset = new GameObject[image_Ctrl_Parameters.TansuOfImage.GetLength(0), image_Ctrl_Parameters.use_img_num];

        image_Ctrl_Parameters.Nst_st_num = new int[] { 0 };
        image_Ctrl_Parameters.Nimg_position = new Vector3[][] { new Vector3[] { new Vector3(672f, 230f, 0.0f), new Vector3(672f, 230f, 0.0f) } };
        image_Ctrl_Parameters.NTansuOfImage = new GameObject[][] { new GameObject[] { g1, g2 } };
        image_Ctrl_Parameters.Nuse_img_num = 2;
        image_Ctrl_Parameters.Ndel_num = new int[][] { new int[] { 1, 1 }};
        image_Ctrl_Parameters.Nset = new GameObject[image_Ctrl_Parameters.TansuOfImage.GetLength(0), image_Ctrl_Parameters.use_img_num];



        fontsize_parameters.change_use_st_count = 3;
        fontsize_parameters.max_change_char_count = 3;
        fontsize_parameters.normal_change_char_num = new int[][] { new int[] { 0, 2, 3 }, new int[] { 0, 1, 5 }, new int[] { 0, 2 ,4 } };//new int[][] { new int[] { 1, 2, 6 }, new int[] { 0, 1, 3 }, new int[] { 1, 2, 3 } };
        fontsize_parameters.change_fontsize = new int[][] { new int[] { 10, 20, 30 }, new int[] { 5, 29, 4 }, new int[] { 1, 2 ,3 } };

        fontsize_parameters.Ychange_use_st_count = 1;
        fontsize_parameters.Ymax_change_char_count = 1;
        fontsize_parameters.yes_change_char_num = new int[][] { new int[] { 0 } };
        fontsize_parameters.Ychange_fontsize = new int[][] { new int[] { 20 } };

        fontsize_parameters.Nchange_use_st_count = 1;
        fontsize_parameters.Nmax_change_char_count = 1;
        fontsize_parameters.no_change_char_num = new int[][] { new int[] { 0 } };
        fontsize_parameters.Nchange_fontsize = new int[][] { new int[] { 20 } };





        talk_background = GameObject.Find("talk_background");//
        image_talk_background = talk_background.GetComponent<Image>();//下のPanelと同じことをしている

        panel = GameObject.Find("Panel");
        panel_image = panel.GetComponent<Image>();//PanelのImageコンポーネントを取得していじることで表示・非表示を行います(シーン君の案、ありがとう)

        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();//会話するための当たり判定

        PlayerPrefs.SetInt("cat", 0);////ここは製作中に何度でもフラグを使って会話ができるようにStart()で全て１回統一している。
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

        ButtonSelect = (GameObject)Resources.Load("ButtonSelect");//ボタン選択で使うプレハブを取得

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



        /////////////一見すると関数の呼び出しに問題がないのに、「範囲外です」みたいなエラーが出るときは、フラグがうまくいっていない。
        /////////////関数が2個以上同時に呼び出されるとそうなってしまうみたい
        ////////////////////////////または、指定した文の文字数とかが、配列の枠を超えている場合がある

        //引数の使い方(使用するDictionary, 使用する文数, 使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定,ボタンは何文目？,Yesで使うDictionary,)


        if (Input.GetKeyDown(KeyCode.E) && tc.cat == "cat")//ここのifに使う関数を直接入れるとなんかうまく動かない
        {
            cat = true;
        }
        if (cat == true)//とりあえずここでワンクッション入れた
        {
            //whether_use_stand = 1;//立ち絵での会話をするなら1, しないなら0
            var g5 = Addressables.LoadAssetAsync<GameObject>("前向き");
            var g6 = Addressables.LoadAssetAsync<GameObject>("後ろ向き");
            var g7 = Addressables.LoadAssetAsync<GameObject>("右向き");
            var g8 = Addressables.LoadAssetAsync<GameObject>("左向き");

            if (once == 0)//構造体のなかの変数を1度だけ変更する
            {
                g5.Completed += op5 =>
                {
                    g6.Completed += op6 =>
                    {
                        g7.Completed += op7 =>
                        {
                            g8.Completed += op8 =>
                            {
                                image_Ctrl_Parameters.TansuOfImage = new GameObject[][] { new GameObject[] { op5.Result, op6.Result }, new GameObject[] { op7.Result, op8.Result } };
                                image_Ctrl_Parameters.YTansuOfImage = new GameObject[][] { new GameObject[] { op5.Result, op6.Result } };
                                image_Ctrl_Parameters.NTansuOfImage = new GameObject[][] { new GameObject[] { op5.Result, op6.Result } };
                            };
                        };
                    };
                };

                fontsize_parameters.change_use_st_count = 3;
                fontsize_parameters.normal_change_st_num = new int[] { 0, 1, 2 };
                fontsize_parameters.max_change_char_count = 3;
                fontsize_parameters.normal_change_char_num = new int[][] { new int[] { 0, 2, 3 }, new int[] { 0, 1, 5 }, new int[] { 0, 2, 4 } };//new int[][] { new int[] { 1, 2, 6 }, new int[] { 0, 1, 3 }, new int[] { 1, 2, 3 } };
                fontsize_parameters.change_fontsize = new int[][] { new int[] { 10, 20, 30 }, new int[] { 5, 29, 4 }, new int[] { 1, 2, 3 } };

                fontsize_parameters.Ychange_use_st_count = 1;
                fontsize_parameters.yes_change_st_num = new int[] { 0 };
                fontsize_parameters.Ymax_change_char_count = 1;
                fontsize_parameters.yes_change_char_num = new int[][] { new int[] { 0 } };
                fontsize_parameters.Ychange_fontsize = new int[][] { new int[] { 30 } };

                fontsize_parameters.Nchange_use_st_count = 1;
                fontsize_parameters.no_change_st_num = new int[] { 0 };
                fontsize_parameters.Nmax_change_char_count = 1;
                fontsize_parameters.no_change_char_num = new int[][] { new int[] { 0 } };
                fontsize_parameters.Nchange_fontsize = new int[][] { new int[] { 30 } };

                once++;
            }
            if(image_Ctrl_Parameters.TansuOfImage[0][0] != null)
            {
                normal_talk(tryRPG_dic, 4, new int[] { 2, 3, 4, 10 }, 0.04f, 1, 2, tryRPG_dic, 1, new int[] { 11 }, tryRPG_dic, 1, new int[] { 12 });
                if (talk_finish == 1)
                {
                    Addressables.Release(g5);
                    Addressables.Release(g6);
                    Addressables.Release(g7);
                    Addressables.Release(g8);
                    cat = false;
                    talk_finish = 0;
                    PlayerPrefs.SetInt("cat", 1);
                    catf = PlayerPrefs.GetInt("cat");
                }
            }
            
        }
        


        if (Input.GetKeyDown(KeyCode.E) && tc.sword == "sword")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                sword = true;
            }
            if (sword == true)//とりあえずここでワンクッション入れた
            {
                //normal_talk(tryRPG_dic, 1, new int[] { 3 }, 0.04f, 0, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 2, new int[] {8,9});//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("sword", 1);
                swordf = PlayerPrefs.GetInt("sword");
                textStop = false;
            }



            if (Input.GetKeyDown(KeyCode.E) && tc.ellipse == "ellipse")//ここのifに使う関数を直接入れるとなんかうまく動かない
            {
                ellipse = true;
            }
            if (ellipse == true)//とりあえずここでワンクッション入れた
            {
                //normal_talk(tryRPG_dic, 1, new int[] { 4 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] {8});//画像変更をしない場合、st_st_numとdel_numを0にしてください
                PlayerPrefs.SetInt("ellipse", 1);
                ellipsef = PlayerPrefs.GetInt("ellipse");
                textStop = false;
            }
            


            if (catf + ellipsef + swordf + ojisanf < 4)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//ここのifに使う関数を直接入れるとなんかうまく動かない
                {
                    ojisann = true;
                }
                if (ojisann == true)//とりあえずここでワンクッション入れた
                {
                    //normal_talk(tryRPG_dic, 1, new int[] { 1 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//画像変更をしない場合、st_st_numとdel_numを0にしてください
                    PlayerPrefs.SetInt("ojisan", 1);
                    ojisanf = PlayerPrefs.GetInt("ojisan");
                    textStop = false;
                }        
            }



            if (catf == 1 && ellipsef == 1 && swordf == 1 && ojisanf == 1)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//ここのifに使う関数を直接入れるとなんかうまく動かない
                {
                    ojisann1 = true;
                }
                if (ojisann1 == true)//とりあえずここでワンクッション入れた
                {
                //normal_talk(tryRPG_dic, 4, new int[] { 6, 7, 8, 9 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//画像変更をしない場合、st_st_numとdel_numを0にしてください
                textStop = false;
                }
            }

    }




    //引数の使い方(   使用するDictionary,使用する文数,使用する文のキー　new int[] {?,?,?}, 文の表示speed, 画像を挿入する文の指定, 画像を消す文の指定,ボタンは何文目？,Yesで使うDictionary,Yesで使う文数, Yesで使うキー, Noで使うDictionary, Nod使う文数, Noで使うキー)
    public void normal_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int whether_use_stand, int bt_st_num, Dictionary<int, string> yesBt_dic, int yesBt_use_st_count, int[] yesBt_use_st_no_key, Dictionary<int, string> noBt_dic, int noBt_use_st_count, int[] noBt_use_st_no_key)



    {//画像挿入では、挿入したい～文目から-1をした数を入れてください。ボタンも同様です。/////画像を消す～文目は-1をしなくて大丈夫です。
     //使用する画像・プレハブが複数の場合は、その都度関数内にInstantiateとDestroyをして、挿入・消去する文数目の指定を引数にも加えてください。使用する最大数を書き足せばたぶんいけるはずです。

        if (whether_use_stand == 1)//立ち絵での会話をするときは、関数呼び出し時にwhether_use_stand=1とすること
        {
            if (set_background == 0)
            {
                PlayerPosition.halt = true;
                image_talk_background.enabled = true;
                set_background++;
                whether_use_stand = 0;
            
            }
        }


        if (textStop == false)
        {
            if (set_panel == 0)//関数開始時にPanalをスイッチオン, 使う文数をセット//最初に一回だけ設定したい値を置く場所
            {
                InputTexts = new string[use_st_count];
                panel_image.enabled = true;
                set_panel++;
                //st_num= 0;
                PlayerPosition.halt = true;
            }

            if (b == 0 && ba == 0)//ボタン選択をしないときの文生成
            {
                for (int it = 0; it < use_st_count; it++)//使う文をDictionaryから読み込み・セット
                {
                    InputTexts[it] = dic[use_st_no_key[it]];

                }
            }
            if (b == 1 && ba == 1)//ボタン選択をするときの文生成
            {

                Debug.Log(PlayerPosition.noClick);
                Debug.Log(PlayerPosition.yesClick);

                if (PlayerPosition.yesClick == true)//YESボタンをクリックした場合
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
                        normal_yes_no = 1;
                        ikkaidake = 0;
                        destroy_image(ref image_Ctrl_Parameters);
                    }

                    for (int it = 0; it < yesBt_use_st_count; it++)//使う文をDictionaryから読み込み・セット
                    {
                        InputTexts[it] = yesBt_dic[yesBt_use_st_no_key[it]];
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
                        normal_yes_no = 2;
                        ikkaidake = 0;
                        destroy_image(ref image_Ctrl_Parameters);
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


                    sort_image(ref image_Ctrl_Parameters);
                    change_fontsize(ref fontsize_parameters);//変更する箇所が多いと見にくくなりそうだったので、分けました。

                    if (e == 0)//通常のフォントサイズでの文生成
                    {
                        displayedText = displayedText + "<size=" + normal_fontsize + ">" + InputTexts[st_num][textCharKazu] + "</size>";//
                    }
                    e = 0;
                    textCharKazu++;


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

                        if (st_num == bt_st_num && b == 0)/////////////////////頭尾の文以外のボタン操作////////////////////////////
                        {
                            inst_ButtonSelect = Instantiate(ButtonSelect, new Vector3(398.5f, 34.5f, 0.0f), Quaternion.identity, canvas.transform);
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

                            destroy_image(ref image_Ctrl_Parameters);

                            if (b == 1)
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

                        if (st_num == bt_st_num && b == 0)//「文末に生成されない問題」を解決するために書いてある///////////////////////////////
                        {
                            inst_ButtonSelect = Instantiate(ButtonSelect, new Vector3(398.5f, 154.5f, 0.0f), Quaternion.identity, canvas.transform);
                            b++;
                        }

                        if (click == true)//最終文が終わった際の
                        {

                            displayedText = "";
                            textCharKazu = 0;
                            textStop = true;

                            dousunnda = false;//タグ関連のboolを全部ここでfalseにリセットする
                            cat = false;
                            ellipse = false;
                            sword = false;
                            ojisann = false;
                            ojisann1 = false;

                            destroy_image(ref image_Ctrl_Parameters);

                            st_num = 0;//ここで各値を元に戻すことで、再度関数を呼び出したときも初めからちゃんと動く
                            i = 0;
                            k = 0;
                            Destroy(inst_blink);
                            //b = 0;///////////最後の文終了後にbを初期化してる////////////////////////
                            //ba = 1;
                            h = 0;

                            if (check_existence == 1)
                            {
                                Destroy(inst_sort_image1);//画像の削除
                                check_existence = 0;
                            }

                            this.GetComponent<Text>().text = "";
                            panel_image.enabled = false;
                            set_panel = 0;
                            bt_shokika = 0;


                            if (set_background == 1)
                            {
                                image_talk_background.enabled = false;
                                set_background = 0;
                                whether_use_stand = 0;
                            }

                            if (b == 1)
                            {
                                ba = 1;
                            }

                            if (bs == 1)
                            {
                                b = 0;
                                ba = 0;
                                bs = 0;
                            }
                            PlayerPosition.halt = false;
                            normal_yes_no = 0;
                            once = 0;
                            ikkaidake = 0;

                            if (fontsize_parameters.normal_change_st_num != null)//フォントサイズ変更の構造体を元に戻す(元に戻しているわけではない)
                            {
                                fontsize_parameters.normal_change_st_num = null;
                            }
                            if (fontsize_parameters.yes_change_st_num != null)
                            {
                                fontsize_parameters.yes_change_st_num = null;
                                fontsize_parameters.no_change_st_num = null;
                            }


                            if(image_Ctrl_Parameters.st_st_num != null )//画像変更の構造体を元に戻す(元に戻しているわけではない)
                            {
                                image_Ctrl_Parameters.st_st_num = null;
                                image_Ctrl_Parameters.del_num = null;
                            }
                            if(image_Ctrl_Parameters.Yst_st_num != null | image_Ctrl_Parameters.Nst_st_num != null)
                            {
                                image_Ctrl_Parameters.Yst_st_num = null;
                                image_Ctrl_Parameters.Nst_st_num = null;
                                image_Ctrl_Parameters.Ydel_num = null;
                                image_Ctrl_Parameters.Ndel_num = null;
                            }

                            if (d == 1 & b == 0)
                            {
                                PlayerPosition.yesClick = false;//staticのスクリプトPlayerPositionでボタンの状態を保存しているモノを元に戻してあげる
                                PlayerPosition.noClick = false;
                                d = 0;
                            }
                            textStop = false;
                            talk_finish = 1;
                        }
                    }
                }
                click = false;
            }
            if (b == 1 & d == 0)//ボタン選択の時のマウスを管理
            {

                if (PlayerPosition.noClick | PlayerPosition.yesClick)//ボタンの状態を取得しているのであって、押されたかどうかはボタンのスクリプトで操作している
                {


                    audioSource.PlayOneShot(select_sound);
                    Destroy(inst_ButtonSelect);//
                    d++;


                }

            }
            if (d == 1)//上のif文にこれを入れてしまうと動かないので別にしてある
            {

                if (Input.GetMouseButtonDown(0))
                {

                    if (b != 0)//
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

            if (b == 0)// ボタン選択をしない時のマウスを管理
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (b != 0)//
                    {
                        PlayerPrefs.SetInt("button", 2);
                        int bt = PlayerPrefs.GetInt("button");
                    }
                    click = true;
                    audioSource.PlayOneShot(sound1);
                    Destroy(inst_blink);
                    k = 0;

                }
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

    public struct Fontsize_parameters
    {
        public int change_use_st_count;
        public int[] normal_change_st_num;
        public int max_change_char_count;
        public int[][] normal_change_char_num;
        public int[][] change_fontsize;

        public int Ychange_use_st_count;
        public int[] yes_change_st_num;
        public int Ymax_change_char_count;
        public int[][] yes_change_char_num;
        public int[][] Ychange_fontsize;

        public int Nchange_use_st_count;
        public int[] no_change_st_num;
        public int Nmax_change_char_count;
        public int[][] no_change_char_num;
        public int[][] Nchange_fontsize;
    }


    void change_fontsize(ref Fontsize_parameters fontsize_parameters)//フォントサイズを変更するときの文字生成関数
    {
        //型　変数名　=　A ? B :　C ? D :　E
        //型　変数名　= A ? B :(Aが真ならBを代入)　C ? D :(Cが真ならDを代入) それ以外ならEを代入
        int use_st_count = normal_yes_no == 0 ? fontsize_parameters.change_use_st_count : normal_yes_no == 1 ? fontsize_parameters.Ychange_use_st_count : fontsize_parameters.Nchange_use_st_count;
        int[] change_st_num = normal_yes_no == 0 ? fontsize_parameters.normal_change_st_num : normal_yes_no == 1 ? fontsize_parameters.yes_change_st_num : fontsize_parameters.no_change_st_num;
        int change_char_count = normal_yes_no == 0 ? fontsize_parameters.max_change_char_count : normal_yes_no == 1 ? fontsize_parameters.Ymax_change_char_count : fontsize_parameters.Nmax_change_char_count;
        int[][] change_char_num = normal_yes_no == 0 ? fontsize_parameters.normal_change_char_num : normal_yes_no == 1 ? fontsize_parameters.yes_change_char_num : fontsize_parameters.no_change_char_num;
        int[][] fontsize = normal_yes_no == 0 ? fontsize_parameters.change_fontsize : normal_yes_no == 1 ? fontsize_parameters.Ychange_fontsize : fontsize_parameters.Nchange_fontsize;

            //Debug.Log(string.Join(", ", prameters.normal_change_st_num));
        if (change_st_num != null && change_char_num != null)
        {
            if (st_num == change_st_num[check_st_num]) //サイズ変更する文の出番をキャッチ
            {

                if (textCharKazu == change_char_num[check_st_num][check_char_num])// 文中の文字の出番をキャッチ
                {
                    int inst_change_fontsize = fontsize[check_st_num][check_char_num];
                    displayedText = displayedText + "<size=" + inst_change_fontsize + ">" + InputTexts[st_num][textCharKazu] + "</size>";
                    e++;
                    check_char_num++;

                    if (check_char_num == change_char_count)//ここは文末を探っているのではなく、変更する・した文字の数を探っている
                    {
                        check_char_num = 0;
                        check_st_num++;
                    }
                }
            }

            if (check_st_num == use_st_count)
            {
                check_st_num = 0;
            }
        }        
    }
    public struct image_ctrl_parameters
    {
        public int[] st_st_num;
        public GameObject[][] TansuOfImage;
        public Vector3[][] img_position;
        public int use_img_num;
        public int[][] del_num;
        public GameObject[, ] set;

        public int[] Yst_st_num;
        public GameObject[][] YTansuOfImage;
        public Vector3[][] Yimg_position;
        public int Yuse_img_num;
        public int[][] Ydel_num;
        public GameObject[,] Yset;

        public int[] Nst_st_num;
        public GameObject[][] NTansuOfImage;
        public Vector3[][] Nimg_position;
        public int Nuse_img_num;
        public int[][] Ndel_num;
        public GameObject[,] Nset;
    }

    void sort_image(ref image_ctrl_parameters image_Ctrl_Parameters)
    {
        int[] st_st_num = normal_yes_no == 0 ? image_Ctrl_Parameters.st_st_num : normal_yes_no == 1 ? image_Ctrl_Parameters.Yst_st_num : image_Ctrl_Parameters.Nst_st_num;
        GameObject[][] TansuOfImage = normal_yes_no == 0 ? image_Ctrl_Parameters.TansuOfImage : normal_yes_no == 1 ? image_Ctrl_Parameters.YTansuOfImage : image_Ctrl_Parameters.NTansuOfImage;
        Vector3[][] img_position = normal_yes_no == 0 ? image_Ctrl_Parameters.img_position : normal_yes_no == 1 ? image_Ctrl_Parameters.Yimg_position : image_Ctrl_Parameters.Nimg_position ;
        int use_img_num = normal_yes_no == 0 ? image_Ctrl_Parameters.use_img_num : normal_yes_no == 1 ? image_Ctrl_Parameters.Yuse_img_num : image_Ctrl_Parameters.Nuse_img_num ;
        int[][] del_num = normal_yes_no == 0 ? image_Ctrl_Parameters.del_num : normal_yes_no == 1 ? image_Ctrl_Parameters.Ydel_num : image_Ctrl_Parameters.Ndel_num;
        GameObject[,] set = normal_yes_no == 0 ? image_Ctrl_Parameters.set : normal_yes_no == 0 ? image_Ctrl_Parameters.Yset : image_Ctrl_Parameters.Nset ;

        Debug.Log(bun);
        Debug.Log(insort_img__retsu);
        //Debug.Log(st_st_num[1]);
        if (st_st_num != null)
        {
            if (st_num == st_st_num[bun] && insort_img__retsu < TansuOfImage[bun].Length && ikkaidake == 0)
            {

                set[bun, insort_img__retsu] = Instantiate(TansuOfImage[bun][insort_img__retsu], img_position[bun][insort_img__retsu], Quaternion.identity, canvas.transform);//画像の表示
                check_existence = 1;//画像がヒエラルキーに存在することをチェックしておきたいもの
                insort_img__retsu++;
                Debug.Log(99090909090990);

                if (insort_img__retsu == TansuOfImage[bun].Length)
                {

                    bun++;
                    insort_img__retsu = 0;
                    //count_gyo++;

                    if (bun == TansuOfImage.GetLength(0) && ikkaidake == 0)
                    {
                        bun = 0;
                        //use_img_num = TansuOfImage[bun].Length;
                        //insort_img__retsu = 0;
                        Debug.Log(757575);
                        ikkaidake++;
                        //break;
                    }
                }
            }
        }
    }


    void destroy_image(ref image_ctrl_parameters image_Ctrl_Parameters)
    {
        GameObject[,] set = normal_yes_no == 0 ? image_Ctrl_Parameters.set : normal_yes_no == 0 ? image_Ctrl_Parameters.Yset : image_Ctrl_Parameters.Nset;
        int[][] del_num = normal_yes_no == 0 ? image_Ctrl_Parameters.del_num : normal_yes_no == 1 ? image_Ctrl_Parameters.Ydel_num : image_Ctrl_Parameters.Ndel_num;

        if (del_num!=null)
        {
            if (del_num[bunbun][del_img_retsu] == st_num)
            {
                Destroy(set[bunbun, del_img_retsu]);
                check_existence = 0;
                del_img_retsu++;


                if (set.GetLength(1) == del_img_retsu)
                {
                    bunbun++;
                    del_img_retsu = 0;

                    if (bunbun == del_num.GetLength(0))
                    {
                        bunbun = 0;
                    }
                }
            }
        }
        
        
        
        if (st_num== InputTexts.Length - 1 | bt_shokika==1)
        {
            // すべての要素をループで回します
            for (int i = 0; i < set.GetLength(0); i++)
            {
                for (int j = 0; j < set.GetLength(1); j++)
                {
                    // インスタンスが存在する場合にのみDestroyを呼び出します
                    if (set[i, j] != null)
                    {
                        Destroy(set[i, j]);
                        set[i, j] = null; // 念のためnullを代入
                    }
                }
            }
        }
    }
}
