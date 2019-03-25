using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFont : MonoBehaviour {
    private Animator animator;
    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        // 判断动画是否播放完成
        if (info.normalizedTime >= 1.0f)
        {
            //播放完毕，销毁自身
            Destroy(gameObject);
        }
    }
}
