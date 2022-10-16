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
    public bool CrushInRange;
    StoneScript stoneInRange;
    StoneScript stoneInHand;
    [SerializeField]
    public Transform hand;
    [SerializeField]
    AudioSource[] booSound;
    [SerializeField]
    Animator myAnim;


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
        if (other.tag == "Crush")
        {
            CrushInRange = true;
        }
        if (other.tag == "Stone")
        {
            stoneInRange = other.GetComponent<StoneScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Crush")
        {
            CrushInRange = false;
        }
        if (other.tag == "Stone")
        {
            stoneInRange = null;
        }
    }

    public void TryToSpook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //myAnim.SetBool("isScaring", true);
            //AnimWait("isScaring");
            booSound[Random.Range(0, booSound.Length)].Play();
            OnTryToSpook?.Invoke();
        }
    }

    private IEnumerator AnimWait(string _bool)
    {
        yield return new WaitForEndOfFrame();
        myAnim.SetBool(_bool, false);
    }

    public void OnPerformAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CrushInRange)
            {
                CrushLogic.Instance.TalkWithCrush();
                return;
            }
            if (stoneInHand != null)
            {
                stoneInHand.ThrowStone(transform.forward);
                stoneInHand = null;
                return;
            }
            if (stoneInRange != null)
            {
                stoneInHand = stoneInRange;
                stoneInRange = null;
                Debug.Log(stoneInHand.gameObject.name);
                stoneInHand.GetPickedUp();
                return;
            }
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
