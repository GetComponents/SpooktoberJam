using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public static PlayerActions Instance;

    public int CandyAmount
    {
        get => candyAmount;
        set
        {
            if (value != candyAmount)
            {
                candyAmount = value;
                OnCandyChanged?.Invoke();
            }
        }
    }
    [SerializeField]
    private int candyAmount;
    [HideInInspector]
    public UnityEvent OnCandyChanged;
    [HideInInspector]
    public UnityEvent OnEnableSpookPrompt, OnDisableSpookPrompt;
    [HideInInspector]
    public UnityEvent OnTryToSpook;
    [SerializeField]
    GameObject droppedCandyPrefab;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Candy")
        {
            CandyAmount++;
            Destroy(other.gameObject);
        }
    }

    public void TryToSpook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnTryToSpook?.Invoke();
        }
    }

    public void DropCandy()
    {
        for (int i = 0; i < Mathf.FloorToInt(CandyAmount / 3); i++)
        {
            CandyAmount--;
            Instantiate(droppedCandyPrefab, 
                new Vector3(transform.localPosition.x + Random.Range(-1.5f, 1.5f), droppedCandyPrefab.transform.lossyScale.y * 0.5f, transform.localPosition.z + Random.Range(-1.5f, 1.5f)), 
                Quaternion.identity);
        }
    }
}
