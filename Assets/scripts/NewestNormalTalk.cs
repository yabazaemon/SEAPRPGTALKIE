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

    //UI��Panel�I�u�W�F�N�g��OnOff
    GameObject panel = null;
    int set_panel = 0;

    //�e�L�X�g����
    private string[] InputTexts;//unity������͂��镶
    int st_num = 0;//���͂������̐�������
    string displayedText = "";//�\������镶
    int textCharKazu = 0;//���̕�����
    float displayedTextSpeed = 0.0f;
    private float st_speed = 0.02f;//���̒l��傫������Ƃ������ɂȂ�
    bool click = true;
    bool textStop;
    public int use_st_count;//�g�p���镶�̐�,Inspector����w��ł���B
    public int[] use_st_no_key;//�g�p���镶�Ɋ��蓖�Ă�ꂽ�L�[������z��,Inspector����w��ł���B


    //�t���O�֌W
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //�摜�ύX
    public int st_st_num;//�摜��}�����镶�͉����ڂ��H���w�肷��(sorting sentsnce number)
    public int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;

    //������m�点��_�ŃI�u�W�F�N�g
    GameObject blink;
    private GameObject inst_blink;
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
    int f = 0;

    //�g�p���镶�̓��ꕨ
    private Dictionary<int, string> dic;

    //�g�p����֐��̎w�������bool�l�Q
    public bool NormalTalk;
    public bool ChangeSceneTalk;
    public bool BattleSceneSentence;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("�O����");

        panel = GameObject.Find("Panel");
        panel.SetActive(false);//�Q�[���J�n���̈Ó]��Panel��static�ɂ���B�Ó]����static�ɂ��Ȃ��Ă悢�A�����ǂ����@���Ȃ��ł����ˁB��x�Q�[�����J�n������֌W�Ȃ��̂ł����ǁB

        //tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();

        InputTexts = new string[use_st_count];//�g�p���������̐�����

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



    }

    // Update is called once per frame
    void Update()
    {

       

        //if ((flag1 == 1 && flag2 == 0) || p=0)
        //{
        if (NormalTalk == true)
        {
            tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();
            if (Input.GetKeyDown(KeyCode.R) && tc.isTouch)//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                dousunnda = true;

            }
            if (dousunnda == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                normal_talk(5,new int[] {2,1,1,3,4}, 0.04f, 0, 0);//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������

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


    void normal_talk(int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(�g�p���镶��, �g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��)
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
                            textStop = true;//�����Ŋe�l�����ɖ߂����ƂŁA�ēx�֐����Ăяo�����Ƃ������߂��炿���Ɠ���
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

        SceneManager.LoadScene("talk_scene");//�V�[���ړ�

    }
}
//�g����@�\///////////////////////////////////////////////////////////////////////////////////////////////////////
//�e�L�X�g����
//�摜�ύX
//�����\�����̃T�E���h�ƁA���I�����ɂ���N���b�N�ł̃T�E���h
//�Q�[���J�n���̈Ó]�A�V�[���ړ����̈Ó]
//���̍Ō�ɂ́A������m�点�邽�߂̓_�ł���I�u�W�F�N�g
//Dictionary���Q�Ƃ��邱�ƂŔC�ӂ̕����w��E�\���ł���
//�e�L�X�g����̃v���O�������֐��ɂ��āA�t���O�g�p���Ɏg���₷�����ɂ��Ă݂�(�t���O�Ǘ��̌o���͂Ȃ�)�B
//�V�[���ړ�����Player��position��static�̃X�N���v�g�ŕۑ����āA�V�[����߂����ۂɌ��̏ꏊ(���W)����ĊJ�ł���B
//
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//���ӓ_
//�@1�̃X�N���v�g�őS�Ẳ�b���ꌳ�Ǘ����邱�Ƃ͂ł��Ă��Ȃ��B
//�@Player�̍��W��ۑ�����@�\�̂Ƃ���ŃA�E�g(�V�[���J�ڂ������b���Ȃ���Ζ��Ȃ�)
//�@
//�@NormalTalk��NormalTalk�Ŏg�����͂��Q�Ƃł���悤�ɂ���΂悢�̂ł͂Ȃ����Ǝv���B
//�@�V�[���J�ڂ𔺂���b�ł́A�����Dictionary���Q�Ƃ���B�����������Ȃ����ۂɁA������������T���ăL�[�����蓖�Ă邱�Ƃ��ʓ|���������B
//�@�Ƃ肠�����ANormalTalk�������g���̂ł���Ζ��͂Ȃ��Ǝv���܂��B
//�@�֐����Ăяo���Ƃ��̕������������R���p�N�g�ɂ������̂ł����A�ǂ��ĂȂ��ł��傤��?
