using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner Instance;
    [SerializeField]
    public Transform[] allSpawnPoints;
    [SerializeField]
    Transform[] allPathpoints;

    [SerializeField]
    Transform AdultNPCParent;
    [SerializeField]
    GameObject AdultNPCPRefab;
    public float SpawnCD;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnNPC());
    }


    private IEnumerator SpawnNPC()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnCD);
            Transform spawnPos, goalPos;
            spawnPos = allSpawnPoints[Random.Range(0, allSpawnPoints.Length)];
            do
            {
                goalPos = allSpawnPoints[Random.Range(0, allSpawnPoints.Length)];
            } while (goalPos == spawnPos);
            //GameObject tmp = Instantiate(AdultNPCPRefab, AdultNPCParent);
            GameObject tmp = Instantiate(AdultNPCPRefab, spawnPos.position, Quaternion.identity);
            tmp.transform.position = spawnPos.position;
            tmp.transform.position = new Vector3(spawnPos.position.x, 1.1f, spawnPos.position.z);
            tmp.GetComponent<NavMeshAgent>().SetDestination(goalPos.position);
            //NavMeshHit closestHit;
            //if (NavMesh.SamplePosition(tmp.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            //{

            //    Debug.Log("Found CLose Position");
            //    gameObject.transform.position = closestHit.position;
            //}
            //else
            //    Debug.LogError("Could not find position on NavMesh!");
            tmp.GetComponent<AdultNPCBehaviour>().GoalPos = goalPos;
        }
    }
}
