using UnityEngine;

// Animator for all racoon movements, attack included
public class MovementAnimations : MonoBehaviour
{
    private Animator movementAnimator;
    private bool walking ;
    private bool running;
    private bool idle = true;

    private string WALKING = "walking";
    private string RUNNING = "running";
    private string IDLE = "idle";
    private string JUMP = "jump";
    private string ATTACK = "attack";
    private string AOE = "aoe";
    private string DEATH = "death";
    
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
        movementAnimator.SetTrigger(JUMP);
    }

    public void AnimateAttack()
    {
        movementAnimator.SetTrigger(ATTACK);
    }
    
    public void AnimateAoe()
    {
        movementAnimator.SetTrigger(AOE);
    }

    public void AnimateDeath()
    {
        movementAnimator.SetTrigger(DEATH);
    }

    private void SetAnimations()
    {
        movementAnimator.SetBool(WALKING, walking);
        movementAnimator.SetBool(RUNNING, running);
        movementAnimator.SetBool(IDLE, idle);
    }
}