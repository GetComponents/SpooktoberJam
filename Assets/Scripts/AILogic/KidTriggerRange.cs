using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidTriggerRange : MonoBehaviour
{
    bool amHidden = true;
    [SerializeField]
    float reactionTime;
    [SerializeField]
    KidMovement Kid;
    bool jumpscareDefused;
    [SerializeField]
    JUMPSCAREIMAGE myImage;
    [SerializeField]
    Animator myAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && amHidden)
        {
            myAnim.SetBool("isRunning", true);
            amHidden = false;
            JumpPlayer();
        }
    }


    private void JumpPlayer()
    {
        JumpScare.Instance.JumpScarePlayer(myImage);
        StartCoroutine(WaitForResponse());
        PlayerActions.Instance.OnTryToSpook.AddListener(DefuseJumpscare);
    }

    private void DefuseJumpscare()
    {
        JumpScare.Instance.HideImage(myImage);
        jumpscareDefused = true;
        Kid.RunAway();
    }

    private IEnumerator WaitForResponse()
    {
        yield return new WaitForSeconds(reactionTime);
        PlayerActions.Instance.OnTryToSpook.RemoveListener(DefuseJumpscare);
        if (!jumpscareDefused)
        {
            PlayerActions.Instance.DropCandy();
            Kid.RobPlayer();
        }
        yield return new WaitForSeconds(1f);
        JumpScare.Instance.HideImage(myImage);
    }
}
