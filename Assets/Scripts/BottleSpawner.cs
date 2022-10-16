using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject bottlePrefab;
    public GameObject myBottle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnBottle();
        }
    }

    private void SpawnBottle()
    {
        if (myBottle == null)
        {
            myBottle = Instantiate(bottlePrefab, transform.position, Quaternion.identity);
            myBottle.GetComponentInChildren<StoneScript>().MySpawner = this;
        }
    }
}
