using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpriteChange : MonoBehaviour
{
    [SerializeField]
    Material[] allCandyMaterials;

    [SerializeField]
    MeshRenderer mr;

    private void Start()
    {
        mr.material = allCandyMaterials[Random.Range(0, allCandyMaterials.Length)];
    }
}