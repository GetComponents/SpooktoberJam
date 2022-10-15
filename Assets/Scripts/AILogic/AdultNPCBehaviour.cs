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
    [SerializeField]
    GameObject detourPoint;
    GameObject myDetourPoint;
    StoneScript bottle;
    [SerializeField]
    float spooked1Speed, spooked2Speed, spooked3Speed, panicSpeed;


    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == GoalPos)
        {
            Despawn();
        }
        if (other.gameObject == myDetourPoint)
        {
            Destroy(other.gameObject);
            if (bottle != null)
                Destroy(bottle.transform.parent.gameObject);
            myDetourPoint = null;
            //Pickup Bottle !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            myNavmesh.SetDestination(GoalPos.position);
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    public void PanicRun()
    {
        CanBeScarred = false;
        myNavmesh.speed = panicSpeed;
    }

    public void GoToBottle(StoneScript _bottle)
    {
        NavMeshHit closestHit;
        bottle = _bottle;
        if (NavMesh.SamplePosition(_bottle.transform.position, out closestHit, 500f, NavMesh.AllAreas))
        {
            if (myDetourPoint == null)
            {
                myDetourPoint = Instantiate(detourPoint, closestHit.position, Quaternion.identity);
                myNavmesh.SetDestination(closestHit.position);
            }
        }
    }

    public void GetSpooked()
    {
        if (CanBeScarred)
        {
            if (playerIsClose && AmDistracted)
            {
                DropCandy(12);
                Debug.Log("I AM REALLY SCARRED!!");
                myNavmesh.speed = spooked3Speed;
            }
            else if (playerIsClose || AmDistracted)
            {
                DropCandy(6);
                Debug.Log("I AM KINDA SCARRED!");
                myNavmesh.speed = spooked2Speed;
            }
            else
            {
                DropCandy(3);
                Debug.Log("I AM SLIGHTLY SCARRED");
                myNavmesh.speed = spooked1Speed;
            }
            CanBeScarred = false;
        }
    }

    private void DropCandy(int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            Instantiate(CandyPrefab, new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), CandyPrefab.transform.lossyScale.y * 0.5f, transform.position.z + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
        }
    }
}
