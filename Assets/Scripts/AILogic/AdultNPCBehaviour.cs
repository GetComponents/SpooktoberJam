using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public Image reactionImage;
    public Sprite distractedSpr, spooked1Spr, spooked2Spr, spooked3Spr, spottedSpr, mehSpr, defaultSpr;
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
            ContinueRoute();
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
                if (myNavmesh.SetDestination(closestHit.position))
                {
                    if (myNavmesh.pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        AmDistracted = true;
                        myDetourPoint = Instantiate(detourPoint, closestHit.position, Quaternion.identity);
                    }
                }
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
                reactionImage.sprite = spooked3Spr;
                myNavmesh.speed = spooked3Speed;
            }
            else if (playerIsClose || AmDistracted)
            {
                DropCandy(6);
                //ps2.Play();
                Debug.Log("I AM KINDA SCARED!");
                reactionImage.sprite = spooked2Spr;
                myNavmesh.speed = spooked2Speed;
            }
            else
            {
                DropCandy(3);
                //ps1.Play();
                Debug.Log("I AM SLIGHTLY SCARED");
                reactionImage.sprite = spooked1Spr;
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

    private void ContinueRoute()
    {
        AmDistracted = false;
        reactionImage.sprite = defaultSpr;
        myNavmesh.SetDestination(GoalPos.position);
    }

    public IEnumerator ForgetBottle()
    {
        yield return new WaitForSeconds(2);
        if (bottle != null)
            Destroy(myDetourPoint.gameObject);
        myDetourPoint = null;
        ContinueRoute();
    }
}
