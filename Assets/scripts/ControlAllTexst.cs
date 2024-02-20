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

    //stand_image_talk�֐��Ŏg������
    GameObject talk_background;
    int set_background = 0;

    //UI��Panel�I�u�W�F�N�g��OnOff
    GameObject panel = null;
    int set_panel = 0;

    //�e�L�X�g����
    private string[] InputTexts;//unity������͂��镶
    int st_num = 0;//���͂������̐�������
    string displayedText = "";//�\������镶
    int textCharKazu = 0;//���̕�����
    float displayedTextSpeed = 0.0f;
    bool click = true;
    bool textStop;

    //�t���O�֌W
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //�摜�ύX
    //public int st_st_num;//�摜��}�����镶�͉����ڂ��H���w�肷��(sorting sentsnce number)
    //public int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;
    int check_existence = 0;
    int h = 0;//1�񂾂���������̂Ɏg��

    //������m�点��_�ŃI�u�W�F�N�g
    GameObject blink;
    private GameObject inst_blink;
    int k = 0;

    //���������ƏI���̃T�E���h
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;

    //�{�^���I���@�\
    GameObject ButtonSelect;
    GameObject inst_ButtonSelect;
    int b = 0;//1�񂾂���������̂Ɏg��, �{�^���̏�Ԃ̊Ǘ����s��, ���������u�Ԃɕω�����
    int ba = 0;//�{�^�����N���b�N(Destroy())������ɕω�����
    int bs = 0;//b,ba��0�ɖ߂��̂Ɏg��
    int bt_shokika = 0;
    int d = 0;

    //�V�[���ړ����̈Ó]//�p�r�ɂ���Ďg�p���邩���Ȃ������u//�v�ŕ�����B�@@@@@�ł��̊֘A�̕��𖾋L����
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    int f = 0;

    //�g�p���镶�̓��ꕨ
    private Dictionary<int, string> dic;
    private Dictionary<int, string> yesBt_dic;
    private Dictionary<int, string> noBt_dic;
    private Dictionary<int, string> stage1_dic;
    private Dictionary<int, string> stage2_dic;

    //�g�p����֐��̎w�������bool�l�Q
    public bool NormalTalk;
    public bool ChangeSceneTalk;

    //���K�p//
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

    ////////�g�p�ς݂̃A���t�@�x�b�g//////(k, i, f, d)

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        player = GameObject.Find("�O����");

        talk_background = GameObject.Find("talk_background");
        talk_background.SetActive(false);//�Q�[���J�n���̈Ó]��static�ɂ��āAstand_image_talk�֐�����true�Efalse�ɂ���

        panel = GameObject.Find("Panel");
        panel.SetActive(false);//�Q�[���J�n���̈Ó]��Panel��static�ɂ���B�Ó]����static�ɂ��Ȃ��Ă悢�A�����ǂ����@���Ȃ��ł����ˁB��x�Q�[�����J�n������֌W�Ȃ��̂ł����ǁB

        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();

        PlayerPrefs.SetInt("cat", 0);
        PlayerPrefs.SetInt("ellipse", 0);
        PlayerPrefs.SetInt("sword", 0);
        PlayerPrefs.SetInt("ojisan", 0);
        ojisanf = PlayerPrefs.GetInt("ojisan");
        ellipsef = PlayerPrefs.GetInt("ellipse");
        swordf = PlayerPrefs.GetInt("sword");
        catf = PlayerPrefs.GetInt("cat");

        sort_image1 = (GameObject)Resources.Load("Image");//�}������摜�̃v���n�u�I���BAnimation���g�p����ꍇ�̓v���n�u�ɃZ�b�g���āA�v���n�u�̃X�N���v�g���瑀�삷��B
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        blink = (GameObject)Resources.Load("Blink");//�_�ŃI�u�W�F�N�g

        audioSource = this.GetComponent<AudioSource>();//���\�����̃T�E���h

        ButtonSelect= (GameObject)Resources.Load("ButtonSelect");

        Fade_Black = (GameObject)Resources.Load("Fade_Black");//�Ó]

        

        //Dictionary�ɂ͈ꉞ�K����u""�v���d���ނ��ƁBEx)�g������4��&&�{�^�����g���̂�4���ڂ̏ꍇ�A��������if�����I����Ă��܂���b���r�؂�Ă��܂��B
        //�܂�A�{�^���I���̕��̌�ɂ���1���c�����Ƃɂ���ă{�^���I���ŕω������b���ł���B�����Ă��܂��΁A�ŏI�����u""�v�ł���K�v�͂Ȃ��̂����ǁA���Ԃ�B
        stage1_dic = new Dictionary<int, string>()
        {
            {1, "N.P,K�͐A���̐����ɕK�v�ȎO��h�{�f����"},
            {2, "�y��Ɋ܂܂��NPK�ƍ앨�ɊܗL�����NPK�ɂ͂�͂荷������"},
            {3, "���̂����AN�ɂ����Ĉ�ԑ傫�ȍ�������"},
            {4, "NK���O�����狟�������̂ɑ΂��āAP�͕K�v�ʂ����Ȃ����̂̊O�����狟������邱�Ƃ��قƂ�ǂȂ�"},
            {5, "�܂�A�A���������ԍ͔|���ꑱ����Ɠy�뒆��P���s�����Ă��܂���"},
            {6, "" }
        };

        stage2_dic = new Dictionary<int, string>()
        {
            {1, "�i���ɂ���Ô�1��4902���~/�N"},
            {2, "�y��Ɋ܂܂��NPK�ƍ앨�ɊܗL�����NPK�ɂ͂�͂荷������"},
            {3, "�V�Ł@�y��w�̊�b"},
            {4, "NK���O�����狟�������̂ɑ΂��āAP�͕K�v�ʂ����Ȃ����̂̊O�����狟������邱�Ƃ��قƂ�ǂȂ�"},
            {5, "�엿�ɂȂ����z���̕���"},
            {6, "" }
        };

        tryRPG_dic = new Dictionary<int, string>()
        {
            {1, "�₠����ɂ��́A�S�Ă̐l�ɘb�������Ă�"},
            {2, "�ɂ�[�A�S���S���A�V���[�I�I�I"},
            {3, "�p���c�꒚�͂������Ɋ����ȁ[�i��"},
            {4, "I'm a ellipse."},
            {5, "......�S�Ă�NPC�Ƙb���Ă��āA�ƌ������͂������ǁB"},
            {6, "��[���A����ƏI��������B" },
            {7, "�N�̖��O�͂Ȃ�Ă����񂾂��H"},
            {8, "�������A�u�O�����v�Ƃ����񂾂�" },
            {9, "�Ȃ������킩��Ȃ����ǁA�Ȃ񂾂��O�������Ă��銴��������Ȃ�" },
            {11, "YES�{�^���������܂���" },
            {12, "No�{�^���������܂���" },
            {10, "" }
        };

    }

    // Update is called once per frame
    void Update()
    {



        

        /////////////�ꌩ�֐��̌Ăяo���ɖ�肪�Ȃ��̂ɁA�u�͈͊O�ł��v�݂����ȃG���[���o��Ƃ��́Abool�l�̕ϐ��ɖ�肪����

        if (ChangeSceneTalk == true)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && tc.isTouch)//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                dousunnen = true;

            }
            if (dousunnen == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                stand_image_talk(stage1_dic, 3, new int[] { 2, 1, 1 }, 0.04f, 0, 3);//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������

            }
            textStop = false;

        }


        //�����̎g����(�g�p����Dictionary, �g�p���镶��, �g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��,�{�^���͉����ځH,Yes�Ŏg��Dictionary,)
        if (NormalTalk == true)
        {
            ///�����ɂ���2��if����1�ɏo����΁A����bool�̕ϐ���Update���Ŏg����悤�ɂȂ�B���󂾂Ɖ�b�֐������g�����ƂɈقȂ�bool�̕ϐ����K�v�B�܂��A�C�f�A���Ȃ��B
            if (Input.GetKeyDown(KeyCode.E) && tc.cat=="cat")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                cat = true;

            }
            if (cat == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                normal_talk(tryRPG_dic,4, new int[] {2,3,4,10}, 0.04f, 0, 1, 2, tryRPG_dic, 1, new int[] { 11}, tryRPG_dic, 1, new int[] {12});//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
                PlayerPrefs.SetInt("cat", 1);
                catf = PlayerPrefs.GetInt("cat");
                
                if (d==1 & b==0)
                {
                    PlayerPosition.yesClick = false;//static�̃X�N���v�gPlayerPosition�Ń{�^���̏�Ԃ�ۑ����Ă��郂�m�����ɖ߂��Ă�����
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

            if (Input.GetKeyDown(KeyCode.E) && tc.sword=="sword")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                sword = true;

            }
            if (sword == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                normal_talk(tryRPG_dic, 1, new int[] { 3 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 2, new int[] {8,9});//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
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

            if (Input.GetKeyDown(KeyCode.E) && tc.ellipse=="ellipse")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                ellipse = true;

            }
            if (ellipse == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                normal_talk(tryRPG_dic, 1, new int[] { 4 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] {8});//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
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

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
                {
                    ojisann = true;

                }
                if (ojisann == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
                {
                   normal_talk(tryRPG_dic, 1, new int[] { 1 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
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

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
                {
                    ojisann1 = true;

                }
                if (ojisann1 == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
                {
                    normal_talk(tryRPG_dic, 4, new int[] { 6, 7, 8, 9 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������

                }
                textStop = false;
                

            }
        }

    }







    //�����̎g����(�g�p����Dictionary, �g�p���镶��, �g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��,�{�^���͉����ځH,Yes�Ŏg��Dictionary,)
    void normal_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num, int bt_st_num, Dictionary<int, string> yesBt_dic, int yesBt_use_st_count, int[] yesBt_use_st_no_key, Dictionary<int, string> noBt_dic, int noBt_use_st_count, int[] noBt_use_st_no_key)
    {//�摜�}���ł́A�}���������`���ڂ���-1�������������Ă��������B�{�^�������l�ł��B/////�摜�������`���ڂ�-1�����Ȃ��đ��v�ł��B

            if (textStop == false)
            {
                if (set_panel == 0)//�֐��J�n����Panal���X�C�b�`�I��, �g���������Z�b�g
                {
                    InputTexts = new string[use_st_count];
                    panel.SetActive(true);
                    set_panel++;
                }

                if(b==0 && ba==0)//�{�^���I�������Ȃ��Ƃ��̕�����
                {
                    for (int it = 0; it < use_st_count; it++)//�g������Dictionary����ǂݍ��݁E�Z�b�g
                    {
                        InputTexts[it] = dic[use_st_no_key[it]];

                    }
                }
                if(b==1 && ba==1)//�{�^���I��������Ƃ��̕�����
                {

                Debug.Log(PlayerPosition.noClick);
                Debug.Log(PlayerPosition.yesClick);
                if (PlayerPosition.yesClick==true)//YES�{�^�����N���b�N�����ꍇ
                    {
                        if (bt_shokika == 0)//�{�^���������ꂽ�ꍇ�A���߂�1�񂾂����낢��l���Z�b�g, ���Z�b�g���Ă�����
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
                        for (int it = 0; it < yesBt_use_st_count; it++)//�g������Dictionary����ǂݍ��݁E�Z�b�g
                        {
                            InputTexts[it] = yesBt_dic[yesBt_use_st_no_key[it]];
                        Debug.Log(11111);
                        }
                    }

                    if (PlayerPosition.noClick == true)//NO�{�^�����N���b�N�����ꍇ
                    {
                        if (bt_shokika == 0)//�{�^���������ꂽ�Əꍇ�A���߂�1�񂾂����낢��l���Z�b�g, ���Z�b�g���Ă�����
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
                        for (int it = 0; it < noBt_use_st_count; it++)//�g������Dictionary����ǂݍ��݁E�Z�b�g
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
                    if (textCharKazu != InputTexts[st_num].Length)//Update���ɕ��̕�����1��������displayedText�ɓ����
                    {
                        displayedText = displayedText + InputTexts[st_num][textCharKazu];//
                        textCharKazu++;

                        if (st_num == st_st_num && h == 0)
                        {
                            inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//�摜�̕\��
                            check_existence++;//�摜���q�G�����L�[�ɑ��݂��邱�Ƃ��`�F�b�N���Ă�����������
                            h++;
                        }

                        if (i < textCharKazu)
                        {
                            audioSource.PlayOneShot(sound);
                            i++;
                        }
                    }
                    else//TextCharKazu���g�����̕������Ɠ����ɂȂ����ꍇ
                    {
                        if (st_num != InputTexts.Length - 1)//���͂������̐�-1�ɂȂ�܂�st_num++���ĕ��̈ړ�
                        {

                            if (k == 0)
                            {
                                inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                                k++;
                            }

                            if (st_num == bt_st_num && b == 0)//////////////////////////////////////�����̕��ȊO�̃{�^������////////////////////////////////////////////////////////////
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
                                    Destroy(inst_sort_image1);//�摜�̍폜
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
                            if (k == 0)//�u�����ɐ�������Ȃ����v���������邽�߂ɏ����Ă���
                            {
                                inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                                k++;
                            }

                            if (st_num == bt_st_num && b == 0)//�u�����ɐ�������Ȃ����v���������邽�߂ɏ����Ă���//////////////////////////////////////////////////////////////////////////////////////////////////
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

                                dousunnda = false;//�^�O�֘A��bool��S��������false�Ƀ��Z�b�g����
                                cat = false;
                                ellipse = false;
                                sword = false;
                                ojisann = false;
                                ojisann1 = false;

                                st_num = 0;//�����Ŋe�l�����ɖ߂����ƂŁA�ēx�֐����Ăяo�����Ƃ������߂��炿���Ɠ���
                                i = 0;
                                k = 0;
                                Destroy(inst_blink);
                                //b = 0;///////////�Ō�̕��I�����b�����������Ă�////////////////////////////////////////////////////////////////////
                                //ba = 1;
                                h = 0;

                                if (check_existence == 1)
                                {
                                    Destroy(inst_sort_image1);//�摜�̍폜
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
                if (b == 1 & d==0)//�{�^���I���̎��̃}�E�X���Ǘ�
                {

                    if (PlayerPosition.noClick | PlayerPosition.yesClick)
                    {
                        Destroy(inst_ButtonSelect);///////////////////////////////////////////////
                        d++;        
                    }
                    
                }
                if (d == 1)//���if���ɂ�������Ă��܂��Ɠ����Ȃ��̂ŕʂɂ��Ă���
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

            if (b==0)// �{�^���I�������Ȃ����̃}�E�X���Ǘ�
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








    void stand_image_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int st_st_num, int del_num)//(�g�p����Dictionary, �g�p���镶��, �g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��)
    {//�摜�}���ł́A�}���������`���ڂ���-1�������������Ă��������B/////�摜�������`���ڂ�-1�����Ȃ��đ��v�ł��B
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
                if (textCharKazu != InputTexts[st_num].Length)//Update���ɕ��̕�����1��������displayedText�ɓ����
                {
                    displayedText = displayedText + InputTexts[st_num][textCharKazu];//
                    textCharKazu++;

                    if (st_num == st_st_num && h == 0)
                    {
                        inst_sort_image1 = Instantiate(sort_image1, new Vector3(672f, 230f, 0.0f), Quaternion.identity, canvas.transform);//�摜�̕\��
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

                            if (st_num == del_num)
                            {
                                Destroy(inst_sort_image1);//�摜�̍폜
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
                            dousunnen = false;//�����Ŋe�l�����ɖ߂����ƂŁA�ēx�֐����Ăяo�����Ƃ������߂��炿���Ɠ���
                            st_num = 0;
                            i = 0;
                            k = 0;
                            Destroy(inst_blink);
                            h = 0;
                            if (check_existence == 1)
                            {
                                Destroy(inst_sort_image1);//�摜�̍폜
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

        SceneManager.LoadScene("talk_scene");//�V�[���ړ�

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

       //SceneManager.LoadScene("talk_scene");//�V�[���ړ�

    }
}
