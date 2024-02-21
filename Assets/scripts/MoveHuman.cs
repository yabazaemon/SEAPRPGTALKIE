using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveHuman : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    public float speed;
    private TouchCheck tc = null;
    //GameObject mainCamera;

    private string position = null;

    public float black_time = 1.0f;
    private GameObject Fade_Black;
    GameObject inst_Fade_Black;
    private Canvas canvas = null;

    public int flag1;
    private int check;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        tc = this.transform.Find("TouchCheck").GetComponent<TouchCheck>();
        rb.position = PlayerPosition.position;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Fade_Black = (GameObject)Resources.Load("Fade_Black");
        //PlayerPrefs.DeleteAll();
        //mainCamera = GameObject.Find("Main Camera");
        check = PlayerPrefs.GetInt("Check");

        if(check==0)
        {
            PlayerPrefs.SetInt("Key2", 0);
            PlayerPrefs.SetInt("Check", 1);
        }
        flag1 = PlayerPrefs.GetInt("Key2");
        //StartCoroutine(FadeIn1());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rb.position;
        //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        if (PlayerPosition.halt == false)
        {
            if (Input.GetKey(KeyCode.A))//左移動
            {
                anim.SetBool("to-right", false);
                anim.SetBool("to-left", true);
                anim.SetBool("to-stop", false);
                anim.SetBool("to-front", false);
                anim.SetBool("to-back", false);
                pos += new Vector2(-speed * Time.deltaTime, 0.0f);
                //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            }

            if (Input.GetKey(KeyCode.D))//右移動
            {
                anim.SetBool("to-right", true);
                anim.SetBool("to-left", false);
                anim.SetBool("to-stop", false);
                anim.SetBool("to-front", false);
                anim.SetBool("to-back", false);
                pos += new Vector2(speed * Time.deltaTime, 0.0f);
                //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S))//停止
            {
                anim.SetBool("to-right", false);
                anim.SetBool("to-left", false);
                anim.SetBool("to-stop", true);
                anim.SetBool("to-front", false);
                anim.SetBool("to-back", false);
            }

            if (Input.GetKey(KeyCode.W))//前進
            {
                anim.SetBool("to-right", false);
                anim.SetBool("to-left", false);
                anim.SetBool("to-stop", false);
                anim.SetBool("to-front", false);
                anim.SetBool("to-back", true);
                pos += new Vector2(0.0f, speed * Time.deltaTime);
                //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            }

            if (Input.GetKey(KeyCode.S))//後進
            {
                anim.SetBool("to-right", false);
                anim.SetBool("to-left", false);
                anim.SetBool("to-stop", false);
                anim.SetBool("to-front", true);
                anim.SetBool("to-back", false);
                pos += new Vector2(0.0f, -speed * Time.deltaTime);
                //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            }

            rb.position = pos;

            //if (Input.GetKeyDown(KeyCode.Space) && tc.isTouch)
            //{
               //StartCoroutine(FadeIn());
               //PlayerPosition.position = pos;
                //PlayerPrefs.SetInt("Check1",1)
                //Debug.Log(flag1);
                //if (flag1 == 0)
                //{
                    //PlayerPrefs.SetInt("Key2", 1);
                    //PlayerPrefs.Save();
                //}
                //else
                //{
                    //PlayerPrefs.SetInt("Key2", 2);
                    //PlayerPrefs.Save();
                //}

            //}
            //mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }

    IEnumerator FadeIn()//シーン移動時の暗転
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
            yield return null;
        }
        SceneManager.LoadScene("talk_scene");

    }

    IEnumerator FadeIn1()//開始時の暗転
    {

        for (float t = 0.01f; t < black_time; t += Time.deltaTime)
        {
            if (t == 0.01f)
            {
                inst_Fade_Black = Instantiate(Fade_Black, new Vector3(400f, 221f, 0f), Quaternion.identity, canvas.transform);
            }
            yield return null;
        }
        Destroy(inst_Fade_Black);
        
    }
}
