using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour
{
    //删除时间
    public float deadTime;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, deadTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
