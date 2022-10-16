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
    public bool CanBeScared = true;
    [SerializeField]
    GameObject detourPoint;
    GameObject myDetourPoint;
    StoneScript bottle;
    [SerializeField]
    float spooked1Speed, spooked2Speed, spooked3Speed, panicSpeed;
    [SerializeField]
    ParticleSystem ps1, ps2, ps3;
    [SerializeField]
    float movementSpeed1, movementSpeed2, movementSpeed3;

 

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
            AmDistracted = false;
            //Pickup Bottle !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            myNavmesh.SetDestination(GoalPos.position);
        }
    }

    private void Despawn()
    {
        Destroy(transform.parent.gameObject);
    }

    public void PanicRun()
    {
        CanBeScared = false;
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
                AmDistracted = true;
                myDetourPoint = Instantiate(detourPoint, closestHit.position, Quaternion.identity);
                myNavmesh.SetDestination(closestHit.position);
            }
        }
    }

    public void GetSpooked()
    {
        if (CanBeScared)
        {
            if (playerIsClose && AmDistracted)
            {
                DropCandy(12);
                //ps3.Play();
                Debug.Log("I AM REALLY SCARED!!");
                myNavmesh.speed = spooked3Speed;
            }
            else if (playerIsClose || AmDistracted)
            {
                DropCandy(6);
                //ps2.Play();
                Debug.Log("I AM KINDA SCARED!");
                myNavmesh.speed = spooked2Speed;
            }
            else
            {
                DropCandy(3);
                //ps1.Play();
                Debug.Log("I AM SLIGHTLY SCARED");
                myNavmesh.speed = spooked1Speed;
            }
            myNavmesh.SetDestination(GoalPos.position);
            CanBeScared = false;
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
