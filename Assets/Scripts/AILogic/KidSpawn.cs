using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSpawn : MonoBehaviour
{
    public Transform Parent;
    [SerializeField]
    GameObject kidPrefab;
    [SerializeField]
    int shakeAmount;
    [SerializeField]
    float shakeCD;
    [SerializeField]
    bool AmTrashcan;
    public float spawnChance;
    public bool ContainsChild;
    private GameObject currentKid;


    private void Start()
    {
        CrushLogic.Instance.OnSegmentIncrease.AddListener(IncreaseLevel);
    }

    private void IncreaseLevel()
    {
        switch (CrushLogic.Instance.GameSegment)
        {
            case 0:
                spawnChance = 0;
                break;
            case 1:
                if (AmTrashcan)
                {
                    spawnChance = 0.3f;
                }
                break;
            case 2:
                spawnChance = 0.4f;
                break;
            default:
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !ContainsChild && Random.Range(0f, 1f) < spawnChance)
        {
            ContainsChild = true;
            currentKid = Instantiate(kidPrefab, transform.position, Quaternion.identity);
            StartCoroutine(Shake());
            currentKid.GetComponentInChildren<KidMovement>().MyBush = this;
        }
        else if (other.tag == "Kid" && ContainsChild)
        {
            Destroy(currentKid);
            ContainsChild = false;
        }
    }

    private IEnumerator Shake()
    {
        while (ContainsChild)
        {
            for (int j = 0; j < 10; j++)
            {
                Parent.eulerAngles = new Vector3(Random.Range(-12f, 12f), 0, Random.Range(-12f, 12f));
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(shakeCD);
        }
    }
}
