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

    //�����G�ł̉�b�Ŏg�����̂Ŏg������
    GameObject talk_background;
    Image image_talk_background;
    int set_background = 0;
    int whether_use_stand = 0;

    //UI��Panel�I�u�W�F�N�g��OnOff
    GameObject panel = null;
    int set_panel = 0;
    Image panel_image = null;

    //�e�L�X�g����
    private string[] InputTexts;//���͂��镶
    int st_num = 0;//���͂������̐�������
    string displayedText = "";//�\������镶
    int textCharKazu = 0;//���̕�����
    float displayedTextSpeed = 0.0f;
    bool click = true;
    bool textStop;

    //�ڐG���擾
    private TouchCheck tc = null;

    //�t�H���g�T�C�Y�ύX�@�\
    int normal_fontsize = 20;
    int e = 0;//�ʏ�̃t�H���g�T�C�Y�ɖ߂��Ƃ��Ɏg��
    int normal_yes_no = 0;//normal=0//yes==1//no==2//
    public Fontsize_parameters fontsize_parameters;
    int check_st_num = 0;//change_fontsize()�Ŏg�p
    int check_char_num = 0;//change_fontsize()�Ŏg�p
    public int once = 0;//�\���̂̒��̕ϐ���1�񂾂��A�֐��Ăяo�����O�ɒl��ς��邽�߂Ɏg�p


    //�t���O�֌W
    public int flag1;
    public int flag2;
    public int F1;
    public int F2;

    //�摜�ύX
    //int st_st_num;//�摜��}�����镶�͉����ڂ��H���w�肷��(sorting sentsnce number)
    //int del_num;
    GameObject sort_image1;
    GameObject inst_sort_image1;
    private Canvas canvas = null;
    int check_existence = 0;//�摜�����邩�Ȃ������擾
    int h = 0;//1�񂾂���������̂Ɏg��

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
    //������m�点��_�ŃI�u�W�F�N�g
    GameObject blink;
    private GameObject inst_blink;
    int k = 0;

    //���������ƏI���̃T�E���h
    public AudioClip sound;
    AudioSource audioSource;
    int i = 0;
    public AudioClip sound1;
    public AudioClip select_sound;

    //�{�^���I���@�\
    GameObject ButtonSelect;
    GameObject inst_ButtonSelect;
    int b = 0;//1�񂾂���������̂Ɏg��, �{�^���̏�Ԃ̊Ǘ����s��, ���������u�Ԃɕω�����
    int ba = 0;//�{�^�����N���b�N(Destroy())������ɕω�����
    int bs = 0;//b,ba��0�ɖ߂��̂Ɏg��
    int bt_shokika = 0;
    int d = 0;

    //�Ó]
    public float black_time = 1.5f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    int f = 0;

    //�g�p���镶�̓��ꕨ
    private Dictionary<int, string> stage1_dic;
    private Dictionary<int, string> stage2_dic;

    //�g�p����֐��̎w�������bool�l�Q
    //public bool NormalTalk;

    //���K�p//�t���O�ADictionary���܂�//
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


    ////////�g�p�ς݂̃A���t�@�x�b�g(�O���[�o���ȓz)//////(k, i, f, d)

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
        image_talk_background = talk_background.GetComponent<Image>();//����Panel�Ɠ������Ƃ����Ă���

        panel = GameObject.Find("Panel");
        panel_image = panel.GetComponent<Image>();//Panel��Image�R���|�[�l���g���擾���Ă����邱�Ƃŕ\���E��\�����s���܂�(�V�[���N�̈āA���肪�Ƃ�)

        tc = GameObject.Find("TouchCheck").GetComponent<TouchCheck>();//��b���邽�߂̓����蔻��

        PlayerPrefs.SetInt("cat", 0);////�����͐��쒆�ɉ��x�ł��t���O���g���ĉ�b���ł���悤��Start()�őS�ĂP�񓝈ꂵ�Ă���B
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

        ButtonSelect = (GameObject)Resources.Load("ButtonSelect");//�{�^���I���Ŏg���v���n�u���擾

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



        /////////////�ꌩ����Ɗ֐��̌Ăяo���ɖ�肪�Ȃ��̂ɁA�u�͈͊O�ł��v�݂����ȃG���[���o��Ƃ��́A�t���O�����܂������Ă��Ȃ��B
        /////////////�֐���2�ȏ㓯���ɌĂяo�����Ƃ����Ȃ��Ă��܂��݂���
        ////////////////////////////�܂��́A�w�肵�����̕������Ƃ����A�z��̘g�𒴂��Ă���ꍇ������

        //�����̎g����(�g�p����Dictionary, �g�p���镶��, �g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��,�{�^���͉����ځH,Yes�Ŏg��Dictionary,)


        if (Input.GetKeyDown(KeyCode.E) && tc.cat == "cat")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
        {
            cat = true;
        }
        if (cat == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
        {
            //whether_use_stand = 1;//�����G�ł̉�b������Ȃ�1, ���Ȃ��Ȃ�0
            var g5 = Addressables.LoadAssetAsync<GameObject>("�O����");
            var g6 = Addressables.LoadAssetAsync<GameObject>("������");
            var g7 = Addressables.LoadAssetAsync<GameObject>("�E����");
            var g8 = Addressables.LoadAssetAsync<GameObject>("������");

            if (once == 0)//�\���̂̂Ȃ��̕ϐ���1�x�����ύX����
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
        


        if (Input.GetKeyDown(KeyCode.E) && tc.sword == "sword")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                sword = true;
            }
            if (sword == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                //normal_talk(tryRPG_dic, 1, new int[] { 3 }, 0.04f, 0, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 2, new int[] {8,9});//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
                PlayerPrefs.SetInt("sword", 1);
                swordf = PlayerPrefs.GetInt("sword");
                textStop = false;
            }



            if (Input.GetKeyDown(KeyCode.E) && tc.ellipse == "ellipse")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
            {
                ellipse = true;
            }
            if (ellipse == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
            {
                //normal_talk(tryRPG_dic, 1, new int[] { 4 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] {8});//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
                PlayerPrefs.SetInt("ellipse", 1);
                ellipsef = PlayerPrefs.GetInt("ellipse");
                textStop = false;
            }
            


            if (catf + ellipsef + swordf + ojisanf < 4)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
                {
                    ojisann = true;
                }
                if (ojisann == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
                {
                    //normal_talk(tryRPG_dic, 1, new int[] { 1 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
                    PlayerPrefs.SetInt("ojisan", 1);
                    ojisanf = PlayerPrefs.GetInt("ojisan");
                    textStop = false;
                }        
            }



            if (catf == 1 && ellipsef == 1 && swordf == 1 && ojisanf == 1)
            {

                if (Input.GetKeyDown(KeyCode.E) && tc.touch == "touch")//������if�Ɏg���֐��𒼐ړ����ƂȂ񂩂��܂������Ȃ�
                {
                    ojisann1 = true;
                }
                if (ojisann1 == true)//�Ƃ肠���������Ń����N�b�V�������ꂽ
                {
                //normal_talk(tryRPG_dic, 4, new int[] { 6, 7, 8, 9 }, 0.04f, 5, 5, 10, stage1_dic, 3, new int[] { 1, 2, 3 }, tryRPG_dic, 1, new int[] { 9 });//�摜�ύX�����Ȃ��ꍇ�Ast_st_num��del_num��0�ɂ��Ă�������
                textStop = false;
                }
            }

    }




    //�����̎g����(   �g�p����Dictionary,�g�p���镶��,�g�p���镶�̃L�[�@new int[] {?,?,?}, ���̕\��speed, �摜��}�����镶�̎w��, �摜���������̎w��,�{�^���͉����ځH,Yes�Ŏg��Dictionary,Yes�Ŏg������, Yes�Ŏg���L�[, No�Ŏg��Dictionary, Nod�g������, No�Ŏg���L�[)
    public void normal_talk(Dictionary<int, string> dic, int use_st_count, int[] use_st_no_key, float st_speed, int whether_use_stand, int bt_st_num, Dictionary<int, string> yesBt_dic, int yesBt_use_st_count, int[] yesBt_use_st_no_key, Dictionary<int, string> noBt_dic, int noBt_use_st_count, int[] noBt_use_st_no_key)



    {//�摜�}���ł́A�}���������`���ڂ���-1�������������Ă��������B�{�^�������l�ł��B/////�摜�������`���ڂ�-1�����Ȃ��đ��v�ł��B
     //�g�p����摜�E�v���n�u�������̏ꍇ�́A���̓s�x�֐�����Instantiate��Destroy�����āA�}���E�������镶���ڂ̎w��������ɂ������Ă��������B�g�p����ő吔�����������΂��Ԃ񂢂���͂��ł��B

        if (whether_use_stand == 1)//�����G�ł̉�b������Ƃ��́A�֐��Ăяo������whether_use_stand=1�Ƃ��邱��
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
            if (set_panel == 0)//�֐��J�n����Panal���X�C�b�`�I��, �g���������Z�b�g//�ŏ��Ɉ�񂾂��ݒ肵�����l��u���ꏊ
            {
                InputTexts = new string[use_st_count];
                panel_image.enabled = true;
                set_panel++;
                //st_num= 0;
                PlayerPosition.halt = true;
            }

            if (b == 0 && ba == 0)//�{�^���I�������Ȃ��Ƃ��̕�����
            {
                for (int it = 0; it < use_st_count; it++)//�g������Dictionary����ǂݍ��݁E�Z�b�g
                {
                    InputTexts[it] = dic[use_st_no_key[it]];

                }
            }
            if (b == 1 && ba == 1)//�{�^���I��������Ƃ��̕�����
            {

                Debug.Log(PlayerPosition.noClick);
                Debug.Log(PlayerPosition.yesClick);

                if (PlayerPosition.yesClick == true)//YES�{�^�����N���b�N�����ꍇ
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
                        normal_yes_no = 1;
                        ikkaidake = 0;
                        destroy_image(ref image_Ctrl_Parameters);
                    }

                    for (int it = 0; it < yesBt_use_st_count; it++)//�g������Dictionary����ǂݍ��݁E�Z�b�g
                    {
                        InputTexts[it] = yesBt_dic[yesBt_use_st_no_key[it]];
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
                        normal_yes_no = 2;
                        ikkaidake = 0;
                        destroy_image(ref image_Ctrl_Parameters);
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


                    sort_image(ref image_Ctrl_Parameters);
                    change_fontsize(ref fontsize_parameters);//�ύX����ӏ��������ƌ��ɂ����Ȃ肻���������̂ŁA�����܂����B

                    if (e == 0)//�ʏ�̃t�H���g�T�C�Y�ł̕�����
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
                else//TextCharKazu���g�����̕������Ɠ����ɂȂ����ꍇ
                {
                    if (st_num != InputTexts.Length - 1)//���͂������̐�-1�ɂȂ�܂�st_num++���ĕ��̈ړ�
                    {

                        if (k == 0)
                        {
                            inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                            k++;
                        }

                        if (st_num == bt_st_num && b == 0)/////////////////////�����̕��ȊO�̃{�^������////////////////////////////
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
                        if (k == 0)//�u�����ɐ�������Ȃ����v���������邽�߂ɏ����Ă���
                        {
                            inst_blink = Instantiate(blink, new Vector3(750f, 50f, 0.0f), Quaternion.identity, canvas.transform);
                            k++;
                        }

                        if (st_num == bt_st_num && b == 0)//�u�����ɐ�������Ȃ����v���������邽�߂ɏ����Ă���///////////////////////////////
                        {
                            inst_ButtonSelect = Instantiate(ButtonSelect, new Vector3(398.5f, 154.5f, 0.0f), Quaternion.identity, canvas.transform);
                            b++;
                        }

                        if (click == true)//�ŏI�����I������ۂ�
                        {

                            displayedText = "";
                            textCharKazu = 0;
                            textStop = true;

                            dousunnda = false;//�^�O�֘A��bool��S��������false�Ƀ��Z�b�g����
                            cat = false;
                            ellipse = false;
                            sword = false;
                            ojisann = false;
                            ojisann1 = false;

                            destroy_image(ref image_Ctrl_Parameters);

                            st_num = 0;//�����Ŋe�l�����ɖ߂����ƂŁA�ēx�֐����Ăяo�����Ƃ������߂��炿���Ɠ���
                            i = 0;
                            k = 0;
                            Destroy(inst_blink);
                            //b = 0;///////////�Ō�̕��I�����b�����������Ă�////////////////////////
                            //ba = 1;
                            h = 0;

                            if (check_existence == 1)
                            {
                                Destroy(inst_sort_image1);//�摜�̍폜
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

                            if (fontsize_parameters.normal_change_st_num != null)//�t�H���g�T�C�Y�ύX�̍\���̂����ɖ߂�(���ɖ߂��Ă���킯�ł͂Ȃ�)
                            {
                                fontsize_parameters.normal_change_st_num = null;
                            }
                            if (fontsize_parameters.yes_change_st_num != null)
                            {
                                fontsize_parameters.yes_change_st_num = null;
                                fontsize_parameters.no_change_st_num = null;
                            }


                            if(image_Ctrl_Parameters.st_st_num != null )//�摜�ύX�̍\���̂����ɖ߂�(���ɖ߂��Ă���킯�ł͂Ȃ�)
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
                                PlayerPosition.yesClick = false;//static�̃X�N���v�gPlayerPosition�Ń{�^���̏�Ԃ�ۑ����Ă��郂�m�����ɖ߂��Ă�����
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
            if (b == 1 & d == 0)//�{�^���I���̎��̃}�E�X���Ǘ�
            {

                if (PlayerPosition.noClick | PlayerPosition.yesClick)//�{�^���̏�Ԃ��擾���Ă���̂ł����āA�����ꂽ���ǂ����̓{�^���̃X�N���v�g�ő��삵�Ă���
                {


                    audioSource.PlayOneShot(select_sound);
                    Destroy(inst_ButtonSelect);//
                    d++;


                }

            }
            if (d == 1)//���if���ɂ�������Ă��܂��Ɠ����Ȃ��̂ŕʂɂ��Ă���
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

            if (b == 0)// �{�^���I�������Ȃ����̃}�E�X���Ǘ�
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


    void change_fontsize(ref Fontsize_parameters fontsize_parameters)//�t�H���g�T�C�Y��ύX����Ƃ��̕��������֐�
    {
        //�^�@�ϐ����@=�@A ? B :�@C ? D :�@E
        //�^�@�ϐ����@= A ? B :(A���^�Ȃ�B����)�@C ? D :(C���^�Ȃ�D����) ����ȊO�Ȃ�E����
        int use_st_count = normal_yes_no == 0 ? fontsize_parameters.change_use_st_count : normal_yes_no == 1 ? fontsize_parameters.Ychange_use_st_count : fontsize_parameters.Nchange_use_st_count;
        int[] change_st_num = normal_yes_no == 0 ? fontsize_parameters.normal_change_st_num : normal_yes_no == 1 ? fontsize_parameters.yes_change_st_num : fontsize_parameters.no_change_st_num;
        int change_char_count = normal_yes_no == 0 ? fontsize_parameters.max_change_char_count : normal_yes_no == 1 ? fontsize_parameters.Ymax_change_char_count : fontsize_parameters.Nmax_change_char_count;
        int[][] change_char_num = normal_yes_no == 0 ? fontsize_parameters.normal_change_char_num : normal_yes_no == 1 ? fontsize_parameters.yes_change_char_num : fontsize_parameters.no_change_char_num;
        int[][] fontsize = normal_yes_no == 0 ? fontsize_parameters.change_fontsize : normal_yes_no == 1 ? fontsize_parameters.Ychange_fontsize : fontsize_parameters.Nchange_fontsize;

            //Debug.Log(string.Join(", ", prameters.normal_change_st_num));
        if (change_st_num != null && change_char_num != null)
        {
            if (st_num == change_st_num[check_st_num]) //�T�C�Y�ύX���镶�̏o�Ԃ��L���b�`
            {

                if (textCharKazu == change_char_num[check_st_num][check_char_num])// �����̕����̏o�Ԃ��L���b�`
                {
                    int inst_change_fontsize = fontsize[check_st_num][check_char_num];
                    displayedText = displayedText + "<size=" + inst_change_fontsize + ">" + InputTexts[st_num][textCharKazu] + "</size>";
                    e++;
                    check_char_num++;

                    if (check_char_num == change_char_count)//�����͕�����T���Ă���̂ł͂Ȃ��A�ύX����E���������̐���T���Ă���
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

                set[bun, insort_img__retsu] = Instantiate(TansuOfImage[bun][insort_img__retsu], img_position[bun][insort_img__retsu], Quaternion.identity, canvas.transform);//�摜�̕\��
                check_existence = 1;//�摜���q�G�����L�[�ɑ��݂��邱�Ƃ��`�F�b�N���Ă�����������
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
            // ���ׂĂ̗v�f�����[�v�ŉ񂵂܂�
            for (int i = 0; i < set.GetLength(0); i++)
            {
                for (int j = 0; j < set.GetLength(1); j++)
                {
                    // �C���X�^���X�����݂���ꍇ�ɂ̂�Destroy���Ăяo���܂�
                    if (set[i, j] != null)
                    {
                        Destroy(set[i, j]);
                        set[i, j] = null; // �O�̂���null����
                    }
                }
            }
        }
    }
}
