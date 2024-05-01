using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    GameObject triangle = null;
    GameObject inst_triangle = null;
    Canvas canvas = null;
    GameObject MenuGamen = null;
    RectTransform MenuGamen_rect = null;

    Image menu = null;
    Image profile = null;
    Image item = null;
    Image armor = null;
    Image setting = null;
    Image stop = null;

    //RectTransform original_position = null;

    //GameObject inst_menu = null;
    //Image menu_image = null;
    int count = 0;
    int once = 0;
    int start = 0;
    float wait_time = 0.0f;
    float menu_move = 0.0f;
    float rect = 0.0f;

    int menu_start_once = 0;

    public AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        triangle= (GameObject)Resources.Load("choice_mark");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        //MenuGamen = GameObject.Find("menu");
        //MenuGamen_rect = GameObject.Find("menu").GetComponent<RectTransform>();
        //original_position = MenuGamen_rect.localPosition;

        //menu = GameObject.Find("menu").GetComponent<Image>();
        //profile= GameObject.Find("profile").GetComponent<Image>();
        //item= GameObject.Find("ÉAÉCÉeÉÄ").GetComponent<Image>();
        //armor= GameObject.Find("ëïîı").GetComponent<Image>();
        //setting= GameObject.Find("ê›íË").GetComponent<Image>();
        //stop= GameObject.Find("Ç‚ÇﬂÇÈ").GetComponent<Image>();
        //menu_image=menu.GetComponent<Image>();


        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            menu.enabled = true;
            profile.enabled = true;
            item.enabled = true;
            armor.enabled = true;
            setting.enabled = true;
            stop.enabled = true;

            

            start=1;
            PlayerPosition.halt = true;   
            
            if(menu_start_once==0)
            {
                audioSource.PlayOneShot(sound);
                menu_start_once++;
            }
        }
        if(start ==1)
        {
            float rect = MenuGamen_rect.localPosition.x;
            Debug.Log(rect);
            if (rect < -320)
            {
                menu_move += Time.deltaTime * 3;
                MenuGamen_rect.localPosition += new Vector3(menu_move, 0, 0);
            }
        }
        if(start==1)
        {
            select_button();
        }
        if (Input.GetKey(KeyCode.Q))
        {
           
            start = 0;
            menu.enabled = false;
            profile.enabled = false;
            item.enabled = false;
            armor.enabled = false;
            setting.enabled = false;
            stop.enabled = false;
            count = 0;
            once = 0;

            Destroy(inst_triangle);
            MenuGamen_rect.localPosition = new Vector3(-473, 0, 0);
            menu_move = 0;

            menu_start_once = 0;

            PlayerPosition.halt = false;
        }

        
    }

    void select_button()
    {
        wait_time += Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
        {
            if(wait_time>0.35f)
            {
                if (count != 0)
                {
                    count -= 1;
                    once = 0;
                    wait_time = 0.0f;
                }
            }
            

        }

        if (Input.GetKey(KeyCode.S))
        {
            if (wait_time > 0.5f)
            {
                if (count != 4)
                {
                    count++;
                    once = 0;
                    wait_time = 0.0f;
                }
            }
            
        }

        if (count == 0 && once==0)
        {
            Destroy(inst_triangle);
            inst_triangle = Instantiate(triangle, new Vector3(5f, 375f, 0.0f), Quaternion.identity, canvas.transform);
            once++;
            audioSource.PlayOneShot(sound);

        }
        if (count == 1 && once == 0)
        {
            Destroy(inst_triangle);
            inst_triangle = Instantiate(triangle, new Vector3(5f, 305f, 0.0f), Quaternion.identity, canvas.transform);
            once++;
            Debug.Log(2121);
            audioSource.PlayOneShot(sound);
        }
        if (count == 2 && once == 0)
        {
            Destroy(inst_triangle);
            inst_triangle = Instantiate(triangle, new Vector3(5f, 245f, 0.0f), Quaternion.identity, canvas.transform);
            once++;
            audioSource.PlayOneShot(sound);
        }
        if (count == 3 && once == 0)
        {
            Destroy(inst_triangle);
            inst_triangle = Instantiate(triangle, new Vector3(5f, 175f, 0.0f), Quaternion.identity, canvas.transform);
            once++;
            audioSource.PlayOneShot(sound);
        }
        if (count == 4 && once == 0)
        {
            Destroy(inst_triangle);
            inst_triangle = Instantiate(triangle, new Vector3(5f, 105f, 0.0f), Quaternion.identity, canvas.transform);
            once++;
            audioSource.PlayOneShot(sound);
        }
        Debug.Log(count);
    }
}
