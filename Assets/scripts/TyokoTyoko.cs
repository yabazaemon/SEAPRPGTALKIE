using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TyokoTyoko : MonoBehaviour
{
    //�e�L�X�g����
    public string[] InputTexts;//unity������͂��镶
    int st_num=0;//���͂������̐�������
    string displayedText="";//�\������镶
    int textCharKazu=0;//���̕�����
    float displayedTextSpeed=0.0f;
    public float st_speed=0.02f;//���̒l��傫������Ƃ������ɂȂ�
    bool click=true;
    bool textStop;

    //�摜�ύX
    public int st_st_num;//�摜��}�����镶�͉����ڂ��H���w�肷��(sentence sorting number)
    public int del_num;   
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;

    //�V�[���ړ����̈Ó]
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;

    //������m�点��_�ŃI�u�W�F�N�g
    GameObject blink;
    private GameObject inst_blink;
    private float blink_time=0.0f;
    public float set_blink_time = 0.0f;
    int k = 0;

    //���������ƏI���̃T�E���h
    public AudioClip sound;
    AudioSource audioSource;
    int i=0;
    public AudioClip sound1;

    //�t���O�֘A
    public int flag1;
    public int F1;

    // Start is called before the first frame update
    void Start()
    {
        
        sort_image1 = (GameObject)Resources.Load("Image");//�}������摜�̃v���n�u�I���BAnimation���g�p����ꍇ�̓v���n�u�ɃZ�b�g���āA�v���n�u�̃X�N���v�g���瑀�삷��B
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

        SceneManager.LoadScene("SampleScene");//�V�[���ړ�

    }

}
