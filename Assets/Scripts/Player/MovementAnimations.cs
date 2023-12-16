using UnityEngine;
using System;

public class MovementAnimations : MonoBehaviour
{
    private Animator movementAnimator;
    private bool walking = false;
    private bool running = false;
    private bool idle = true;

    private string WALKING = "walking";
    private string RUNNING = "running";
    private string IDLE = "idle";
    
    void Start()
    {
        movementAnimator = GetComponent<Animator>();
    }

    public void AnimateIdle()
    {
        walking = false;
        running = false;
        idle = true;
        SetAnimations();
    }

    public void AnimateWalking()
    {
        walking = true;
        running = false;
        idle = false;
        SetAnimations();
    }

    public void AnimateRunning()
    {
        walking = false;
        running = true;
        idle = false;
        SetAnimations();
    }
    
    public void AnimateJump()
    {
        movementAnimator.SetTrigger("jump");
    }

    public void AnimateAttack()
    {
        movementAnimator.SetTrigger("attack");
    }
    
    public void AnimateAoe()
    {
        movementAnimator.SetTrigger("aoe");
    }

    public void AnimateDeath()
    {
        movementAnimator.SetTrigger("death");
    }

    private void SetAnimations()
    {
        movementAnimator.SetBool(WALKING, walking);
        movementAnimator.SetBool(RUNNING, running);
        movementAnimator.SetBool(IDLE, idle);
    }
    
    public float GetAnimationClipLength(string clipName)
    {
        RuntimeAnimatorController ac = movementAnimator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f; 
    }
    
    
}