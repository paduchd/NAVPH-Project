using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	private Animator mAnimator;
    
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(mAnimator != null)
		{
            if(Input.GetKeyDown(KeyCode.W))
			{
				mAnimator.SetTrigger("TrForward");
			}

			if(Input.GetKeyDown(KeyCode.S))
			{
				mAnimator.SetTrigger("TrBackwards");
			}
			
			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				mAnimator.SetTrigger("TrRun");
			}
			
			if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
			{
				mAnimator.SetTrigger("TrIdle");
			}
		}

    }
}
