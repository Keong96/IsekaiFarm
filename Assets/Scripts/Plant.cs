using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int id;
    public string plantName;
    public int plantType;
    public int position;
    public int growthStage;
    public int growthPoint;
    public int waterPoint;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        switch (growthStage)
        {
            case 0:
                animator.playbackTime = 0.1f;
                break;
            case 1:
                animator.playbackTime = 0.4f;
                break;
            case 2:
                animator.playbackTime = 0.75f;
                break;
            case 3:
                animator.playbackTime = 1.0f;
                break;
        }
    }
}
