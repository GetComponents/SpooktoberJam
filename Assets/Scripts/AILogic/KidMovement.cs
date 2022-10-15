using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidMovement : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent myMovement;

    [SerializeField]
    float runAwaySpeed;

    public KidSpawn MyBush;

    public void RunAway()
    {
        MyBush.Parent.eulerAngles = new Vector3(0, 0, 0);
        MyBush.ContainsChild = false;
        Debug.Log("AHHH I got spotted");
        GoToRandomGate();
    }

    public void RobPlayer()
    {
        MyBush.Parent.eulerAngles = new Vector3(0, 0, 0);
        MyBush.ContainsChild = false;
        myMovement.SetDestination(GameObject.FindGameObjectWithTag("DroppedCandy").transform.position);
    }

    private void GoToRandomGate()
    {
        myMovement.speed = runAwaySpeed;
        myMovement.SetDestination(NPCSpawner.Instance.allSpawnPoints[Random.Range(0, NPCSpawner.Instance.allSpawnPoints.Length)].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DroppedCandy")
        {
            Destroy(other.gameObject);
            StartCoroutine(FindNewCandy());
        }
        else if(other.tag == "SpawnPoint")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FindNewCandy()
    {
        yield return new WaitForEndOfFrame();
        if (GameObject.FindGameObjectWithTag("DroppedCandy") != null)
        {
            myMovement.SetDestination(GameObject.FindGameObjectWithTag("DroppedCandy").transform.position);
        }
        else
        {
            GoToRandomGate();
        }
    }
}
