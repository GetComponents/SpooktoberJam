using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdultNPCBehaviour : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent myNavmesh;

    [SerializeField]
    GameObject CandyPrefab;
    public bool playerIsClose, AmDistracted;
    public Transform GoalPos;
    public bool CanBeScarred = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == GoalPos)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    public void GetSpooked()
    {
        if (CanBeScarred)
        {
            if (playerIsClose && AmDistracted)
            {
                DropCandy(12);
                Debug.Log("I AM REALLY SCARRED!!");
            }
            else if (playerIsClose || AmDistracted)
            {
                Debug.Log("I AM KINDA SCARRED!");
                DropCandy(6);
            }
            else
            {
                DropCandy(3);
                Debug.Log("I AM SLIGHTLY SCARRED");
            }
            CanBeScarred = false;
        }
    }

    private void DropCandy(int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            Instantiate(CandyPrefab, new Vector3(transform.localPosition.x + Random.Range(-1.5f, 1.5f), CandyPrefab.transform.lossyScale.y * 0.5f, transform.localPosition.z + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
        }
    }
}
