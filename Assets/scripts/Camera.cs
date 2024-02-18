using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("‘OŒü‚«");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        this.transform.position = pos + new Vector3(0.0f, 0.0f, -10f);
    }
}
