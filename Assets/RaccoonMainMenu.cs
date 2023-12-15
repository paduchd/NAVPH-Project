using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonMainMenu : MonoBehaviour
{
    private Animator animator;
    private bool isReadyToAttack = true;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdle", true);
    }


    void Update()
    {
        if (isReadyToAttack)
        {
            isReadyToAttack = false;
            StartCoroutine(AnimateRaccoon());
        }
    }

    IEnumerator AnimateRaccoon()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(Random.Range(3,10));
        isReadyToAttack = true;
    }
}
