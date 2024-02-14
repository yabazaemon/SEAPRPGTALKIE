using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TyokoTyoko : MonoBehaviour
{
    //テキスト送り
    public string[] InputTexts;//unityから入力する文
    int st_num=0;//入力した文の数を扱う
    string displayedText="";//表示される文
    int textCharKazu=0;//文の文字数
    float displayedTextSpeed=0.0f;
    public float st_speed=0.02f;//この値を大きくするとゆっくりになる
    bool click=true;
    bool textStop;

    //画像変更
    public int st_st_num;//画像を挿入する文は何文目か？を指定する(sentence sorting number)
    public int del_num;   
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;

    //シーン移動時の暗転
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;

    //文末を知らせる点滅オブジェクト
    GameObject blink;
    private GameObject inst_blink;
    private float blink_time=0.0f;
    public float set_blink_time = 0.0f;
    int k = 0;

    //文生成時と終わりのサウンド
    public AudioClip sound;
    AudioSource audioSource;
    int i=0;
    public AudioClip sound1;

    //フラグ関連
    public int flag1;
    public int F1;

    // Start is called before the first frame update
    void Start()
    {
        
        sort_image1 = (GameObject)Resources.Load("Image");//挿入する画像のプレハブ選択。Animationを使用する場合はプレハブにセットして、プレハブのスクリプトから操作する。
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Fade_Black = (GameObject)Resources.Load("Fade_Black");
        blink= (GameObject)Resources.Load("Blink");
        audioSource = this.GetComponent<AudioSource>();
        flag1 = PlayerPrefs.GetInt("Key2");


    }
    
    // Update is called once per frame
    void Update()
    {
        if (flag1==F1)
        { 


            blink_time += Time.deltaTime;
            if (textStop == false)
            {

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

                                //inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);

                            }
                        }
                        else
                        {
                            if (click == true)
                            {
                                displayedText = "";
                                textCharKazu = 0;
                                textStop = true;
                                displayedTextSpeed = 0.0f;

                                StartCoroutine(FadeIn());

                            }
                        }
                    }


                    click = false;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    click = true;
                    Destroy(inst_blink);
                    k = 0;
                    Debug.Log(11);
                    i = 0;
                    audioSource.PlayOneShot(sound1);

                }
            }
        }

        else
        {
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator FadeIn()
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            if (t == 0.01f)
            {
                inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
            }

            
            yield return null;
        }

        SceneManager.LoadScene("SampleScene");//シーン移動

    }

}
