using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonYes : MonoBehaviour
{

    GameObject choice_mark = null;
    GameObject inst_choice_mark = null;
    private Canvas canvas = null;
    AudioSource audioSource;
    public AudioClip select_sound;//
    /// </summary>
    int choice_button = 0;
    int ctrInstantiate = 0;
    

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        choice_mark = (GameObject)Resources.Load("choice_mark");
        audioSource = this.GetComponent<AudioSource>();
        //this.gameObject.AddComponent<AudioSource>();//AudioSourceコンポーネントの追加
        //select_sound= (GameObject)Resources.Load("select_sound");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))//左移動
        {
            if(ctrInstantiate == 0)
            {
                inst_choice_mark = Instantiate(choice_mark, new Vector3(250.5f, 34.5f, 0.0f), Quaternion.identity, canvas.transform);
                ctrInstantiate = 1;
            }
            
            choice_button = 1;
        }

        if (Input.GetKey(KeyCode.D))//左移動
        {
            Destroy(inst_choice_mark);
            choice_button = 0;
            ctrInstantiate = 0;
        }

        if(choice_button == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(inst_choice_mark);
            choice_button = 0;
            ctrInstantiate = 0;
            PlayerPosition.yesClick = true;    
        }

    }
   public void OnClick()
    {
        PlayerPosition.yesClick=true;
        Debug.Log("yes");
    }


}
