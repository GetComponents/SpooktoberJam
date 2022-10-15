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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && amHidden)
        {
            amHidden = false;
            JumpScare();
        }
    }


    private void JumpScare()
    {
        StartCoroutine(WaitForResponse());
        PlayerActions.Instance.OnTryToSpook.AddListener(DefuseJumpscare);
    }

    private void DefuseJumpscare()
    {
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
    }
}
