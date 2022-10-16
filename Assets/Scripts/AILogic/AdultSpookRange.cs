using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultSpookRange : MonoBehaviour
{
    [SerializeField]
    bool isInnerRing;

    [SerializeField]
    AdultNPCBehaviour Npc;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player" && Npc.CanBeScared)
        {
            if (PlayerActions.Instance.GetComponent<PlayerMovement>().isSprinting)
            {
                Npc.CanBeScared = false;
                Npc.reactionImage.sprite = Npc.spottedSpr;
                return;
            }
            if (isInnerRing)
            {
                Npc.playerIsClose = true;
            }
            else
            {
                PlayerActions.Instance.OnTryToSpook.AddListener(Npc.GetSpooked);
                PlayerActions.Instance.OnEnableSpookPrompt?.Invoke();
            }
        }
        if (other.tag == "Stone" && Npc.CanBeScared)
        {
            if (other.GetComponent<StoneScript>().AmFlying)
            {
                Npc.PanicRun();
                Npc.reactionImage.sprite = Npc.mehSpr;
            }
            else
            {
                Npc.GoToBottle(other.GetComponent<StoneScript>());
                Npc.reactionImage.sprite = Npc.distractedSpr;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isInnerRing)
        {
            if (isInnerRing)
            {
                Npc.playerIsClose = false;
            }
            else
            {
                PlayerActions.Instance.OnDisableSpookPrompt?.Invoke();
                PlayerActions.Instance.OnTryToSpook.RemoveListener(Npc.GetSpooked);
            }
        }
    }
}
