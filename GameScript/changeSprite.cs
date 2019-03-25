using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class changeSprite : MonoBehaviour {
    public Sprite[] sprites;
    int index = 0;
    public float timer = 0;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 0.05f)
        {
            timer = 0;
            index++;
            index = index > sprites.Length - 1 ? 0 : index;
        }
        GetComponent<Image>().sprite = sprites[index];
        
	}
}
