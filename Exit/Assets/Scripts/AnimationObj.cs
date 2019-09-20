using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObj : MonoBehaviour
{
    Animator animator;
    public string parameterName;
    bool isAnime;                      //アニメーションできる状態ならtrue
    private int animeTiming;  //テキストの何行目にアニメーションするか

    public int AnimeTiming { get => animeTiming;}

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAnime = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoopAnimation()
    {
        if (isAnime)
        {
            animator.SetBool(parameterName, true);
            isAnime = false;
        }

        else
        {
            animator.SetBool(parameterName, false);
            isAnime = true;
        }

    }

   
}
