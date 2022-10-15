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
        if (other.tag == "Player" && Npc.CanBeScarred)
        {
            if (PlayerActions.Instance.GetComponent<PlayerMovement>().isSprinting)
            {
                Npc.CanBeScarred = false;
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
