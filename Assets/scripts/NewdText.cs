using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewdText : MonoBehaviour
{
    //�e�L�X�g����
    private string[] InputTexts;//unity������͂��镶
    int st_num = 0;//���͂������̐�������
    string displayedText = "";//�\������镶
    int textCharKazu = 0;//���̕�����
    float displayedTextSpeed = 0.0f;
    public float st_speed = 0.02f;//���̒l��傫������Ƃ������ɂȂ�
    bool click = true;
    bool textStop;

    //�t���O�֌W
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //�摜�ύX
    public int st_st_num;//�摜��}�����镶�͉����ڂ��H���w�肷��(sentence sorting number)
    public int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;

    //������m�点��_�ŃI�u�W�F�N�g
    GameObject blink;
    private GameObject inst_blink;
    private float blink_time = 0.0f;
    public float set_blink_time = 0.0f;
    int k = 0;

    //���������ƏI���̃T�E���h
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;

    //�V�[���ړ����̈Ó]//�p�r�ɂ���Ďg�p���邩���Ȃ������u//�v�ŕ�����B�@@@@@�ł��̊֘A�̕��𖾋L����
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;

    //�g�p���镶�̓��ꕨ
    private Dictionary<int, string> dic;

    public bool p;


    // Start is called before the first frame update
    void Start()
    {
        flag1 = PlayerPrefs.GetInt("Key1");
        flag2 = PlayerPrefs.GetInt("Key2");

        sort_image1 = (GameObject)Resources.Load("Image");//�}������摜�̃v���n�u�I���BAnimation���g�p����ꍇ�̓v���n�u�ɃZ�b�g���āA�v���n�u�̃X�N���v�g���瑀�삷��B
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        blink = (GameObject)Resources.Load("Blink");

        audioSource = this.GetComponent<AudioSource>();

        Fade_Black = (GameObject)Resources.Load("Fade_Black");//@@@@


        dic = new Dictionary<int, string>()
        {
            {1, "N.P,K�͐A���̐����ɕK�v�ȎO��h�{�f����"},
            {2, "�y��Ɋ܂܂��NPK�ƍ앨�ɊܗL�����NPK�ɂ͂�͂荷������"},
            {3, "���̂����AN�ɂ����Ĉ�ԑ傫�ȍ�������"},
            {4, "NK���O�����狟�������̂ɑ΂��āAP�͕K�v�ʂ����Ȃ����̂̊O�����狟������邱�Ƃ��قƂ�ǂȂ�"},
            {5, "�܂�A�A���������ԍ͔|���ꑱ����Ɠy�뒆��P���s�����Ă��܂���"}
        };

        InputTexts = new string[5];//�g�p���������̐�

    }

    // Update is called once per frame
    void Update()
    {
        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        blink_time += Time.deltaTime;
        if (textStop == false)
            {
                InputTexts[0] = dic[1];//�g�p���镶�̃L�[���w��
                InputTexts[1] = dic[2];//���Ԃ�dic[?]�������ŕύX�ł���
                InputTexts[2] = dic[3];
                InputTexts[3] = dic[4];
                InputTexts[4] = dic[5];

                displayedTextSpeed += Time.deltaTime;
                if (displayedTextSpeed > st_speed)
                {
                    this.GetComponent<Text>().text = displayedText;
                    displayedTextSpeed = 0.0f;
                    if (textCharKazu != InputTexts[st_num].Length)//Update���ɕ��̕�����1��������displayedText�ɓ����
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
                        if (st_num != InputTexts.Length - 1)//���͂������̐�-1�ɂȂ�܂�st_num++���ĕ��̈ړ�
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
                                    inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//�摜�̕\��

                                }

                                if (st_num == del_num)
                                {
                                    Destroy(inst_sort_image1);//�摜�̍폜
                                }
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

                                StartCoroutine(FadeIn());//@@@@
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
        //}

      
    }

    IEnumerator FadeIn()//@@@@
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            if (t == 0.01f)
            {
                inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
            }


            yield return null;
        }

        SceneManager.LoadScene("SampleScene");//�V�[���ړ�

    }

}
