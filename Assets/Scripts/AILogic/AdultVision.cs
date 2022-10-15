using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultVision : MonoBehaviour
{
    bool playerInRange;
    [SerializeField]
    LayerMask mixedLayer;
    [SerializeField]
    LayerMask playerLayer;
    [SerializeField]
    AdultNPCBehaviour Npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, PlayerActions.Instance.transform.position - transform.position, out hit, Mathf.Infinity, mixedLayer))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (playerInRange)
                {
                    Npc.CanBeScared = false;
                }
            }
        }
    }
}
