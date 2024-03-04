using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    private GameObject player;
    GameObject maincamera;
    GameObject subcamera;
    Camera subCamera;

    float siki = 0.0f;
    float x = 0.0f;
    float certain_value = 0.0f;

    float xx = 0;
    float yy = 0;
    int k = 0;

    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("�O����");
        maincamera = GameObject.Find("Main Camera");
        subcamera = GameObject.Find("SubCamera");
        subCamera = subcamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera();
        //siki = (float)Math.Pow(x, 1) + 1;
        //siki = 5;
        //KansuGraphCamera(0, 30, 0.05f, 1, 1);
        //if (i==0)
        //{
        //XYstraightTrans(200, 200, 0.1f);

        //}


        //DestiNanameTrans(100, 100, 0.1f, 1, 1);
        //if (maincamera.transform.position.y>200)
        //{
        //ZoomInOut(1, 0.001f, 172);
        //}

        //destinationTeleport(100, 200, 10.0f);
        
    }

    public void MainCamera()//�v���C���[�ɒǏ]
    {
        maincamera.SetActive(true);
        subcamera.SetActive(false);
        Vector3 pos_main = player.transform.position;
        maincamera.transform.position = pos_main + new Vector3(0.0f, 0.0f, -10f);
    }

    //�Ăяo�����ɁAsiki = x^2 + x;�@�̂悤��siki�Ɏg�������������邱��
    public void KansuGraphCamera(float min_x, float max_x, float move_speed, float a, float b)//�������W�n�̊֐����`�����ɉ����ē�������
    {//KansuGraphCamera(x�̍��[, x�̉E�[, �������������� )
        maincamera.SetActive(false);
        subcamera.SetActive(true);

        certain_value += Time.deltaTime;

        Vector3 pos_sub = player.transform.position;
        subcamera.transform.position = pos_sub + new Vector3(a * x, b * siki, -10f);

        if (move_speed < certain_value)
        {
            if (min_x <= x && x <= max_x)
            {
                x += 1.0f;
            }
            certain_value = 0.0f;
        }
        
    }

    public void DestiNanameTrans(float d_x, float d_y, float move_speed)//�ړI�̍��W�֒��Ŏ΂߈ړ�����
    {//DestiNanameTrans(�ړI�n��x���W, �ړI�n��y���W, ����speed, x�̌W��, y�̌W��)
        PlayerPosition.halt = true;
        maincamera.SetActive(true);
        subcamera.SetActive(false);

        Vector3 pos = player.transform.position;
        float sa_x = d_x - pos.x;
        float sa_y = d_y - pos.y;

        if (sa_y == 0)
        {
            Debug.Log("���ݒn�ƖړI�n�Ƃ�y���W�̍����[���ł�");
            return;
        }

        float slop = sa_x / sa_y;

        float ctrl = 0.0f;

        ctrl += Time.deltaTime;
        if (ctrl > 0.001f)
        {
            if (xx < sa_x)
            {
                xx += move_speed;
                float y = slop * xx;
                maincamera.transform.position = pos + new Vector3(xx, y, -10f);
            }
            
        }
        ctrl = 0.0f;

        
    }

    public void XYstraightTrans(float d_x, float d_y, float move_speed)//�ړI�n�ւ́Ax�����Ƃ������̂Q��̒����ړ�
    {
        PlayerPosition.halt = true;
        maincamera.SetActive(true);
        subcamera.SetActive(false);

        Vector3 pos = player.transform.position;
        float sa_x = d_x - pos.x;
        float sa_y = d_y - pos.y;
        Debug.Log(pos.x);
        Debug.Log(pos.y);

        float ctrl = 0.0f;
        ctrl += Time.deltaTime;
        if (ctrl>0.001f)
        {
            if (xx < sa_x)
            {
                xx += move_speed;
                maincamera.transform.position = new Vector3(xx, 0, -10f);
            }
            else
            {
                k = 1;
            }
            if (k == 1)
            {
                if (yy < sa_y)
                {
                    yy += move_speed;
                    maincamera.transform.position = new Vector3(xx, yy, -10f);
                }
                k = 0;

            }
            
        }
        ctrl = 0.0f;

    }

    public void ZoomInOut(int inout, float zoom_speed, int camerasize)//camerasize�̍ő��179
    {
        maincamera.SetActive(false);
        subcamera.SetActive(true);
        PlayerPosition.halt = true;
        if (inout==1)
        {
            float size = subCamera.fieldOfView;
            if (camerasize < size)
            {
                size -= zoom_speed;
                subCamera.fieldOfView = size;
            }
        }

        if (inout == 0)
        {
            float size = subCamera.fieldOfView;
            if (camerasize > size)
            {
                size += zoom_speed;
                subCamera.fieldOfView = size;
            }
        }
    }

    public void destinationTeleport(float x, float y, float wait_time)
    {
        maincamera.SetActive(false);
        subcamera.SetActive(true);

        subcamera.transform.position = new Vector3(x, y, -10.0f);
    }


}

