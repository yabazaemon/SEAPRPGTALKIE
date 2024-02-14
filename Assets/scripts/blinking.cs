using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinking : MonoBehaviour
{

    private float blinking_time = 0.0f;
    public float set_blinking_time;
    private Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        blinking_time += Time.deltaTime;
        if(blinking_time>set_blinking_time)
        {
            anim.SetBool("blinking", true);
        }

        else if(blinking_time>set_blinking_time && blinking_time<set_blinking_time*3)
        {
            anim.SetBool("blinking", false);
        }
        
        else
        {
            blinking_time = 0.0f;
        }
    }
}
