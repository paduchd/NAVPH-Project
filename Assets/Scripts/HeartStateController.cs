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
    
    public void SetImage(HeartState state)
    {
        if (state == HeartState.Empty)
        {
            heartImage.sprite = emptyHeart;
            heartAnimator.SetBool(ISPULSING,false);
            
        } 
        else if (state == HeartState.Full)
        {
            heartImage.sprite = fullHeart;
            heartAnimator.SetBool(ISPULSING,false);
        }
        else if (state == HeartState.Pulsing)
        {
            heartImage.sprite = emptyHeart;
            heartAnimator.SetBool(ISPULSING,true);
        }
    }
}
public enum HeartState
{
    Empty = 0,
    Full = 1,
    Pulsing = 2
}
