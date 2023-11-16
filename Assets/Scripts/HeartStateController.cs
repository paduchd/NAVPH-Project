using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartStateController : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    
    private Image heartImage;
    private Animator heartAnimator;
    private string ISPULSING = "IsPulsing";
    
    private void Awake()
    {
        heartImage = GetComponent<Image>();
        heartAnimator = GetComponent<Animator>();
    }
    
    public void SetHeartState(HeartState state)
    {
        if (state == HeartState.Empty)
        {
            heartImage.sprite = emptyHeart;
            heartAnimator.SetBool(ISPULSING,true);
            //for reseting animation from beginning if already playing
            heartAnimator.Play("HeartPulsing",-1, 0f); 
        } 
        else if (state == HeartState.Full)
        {
            heartImage.sprite = fullHeart;
            heartAnimator.SetBool(ISPULSING,false);
        }
    }
}
public enum HeartState
{
    Empty = 0,
    Full = 1,
}
