using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LookRight()
    {
        animator.SetFloat("Direction", 1);
    }

    public void LookLeft()
    {
        animator.SetFloat("Direction", 0);
    }

    public void Walk()
    {
        animator.ResetTrigger("Stand");
        animator.SetTrigger("Walk");
    }

    public void Stand()
    {
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Stand");
    }

    public void Talk()
    {
        animator.SetBool("Talk", true);
    }

    public void StopTalk()
    {
        animator.SetBool("Talk", false);
    }

    public void Touch()
    {
        animator.SetTrigger("Touch");
    }

    public void Take()
    {
        animator.SetTrigger("Take");
    }

    public void Bite()
    {
        animator.SetTrigger("Bite");
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Use()
    {
        animator.SetTrigger("Use");
    }

    public bool GetIsWalking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Walk");
    }
}
