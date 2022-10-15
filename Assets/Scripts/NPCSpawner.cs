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
            GameObject tmp = Instantiate(AdultNPCPRefab, AdultNPCParent);
            tmp.transform.position = spawnPos.position;
            tmp.GetComponent<NavMeshAgent>().SetDestination(goalPos.position);
            tmp.GetComponent<AdultNPCBehaviour>().GoalPos = goalPos;
        }
    }
}
