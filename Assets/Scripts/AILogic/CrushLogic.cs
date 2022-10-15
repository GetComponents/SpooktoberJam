using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrushLogic : MonoBehaviour
{
    public static CrushLogic Instance;

    [SerializeField]
    TextMeshProUGUI myText;
    [SerializeField]
    private string myDialogue;

    public int CandyRequirement;

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
        if (other.tag == "Player")
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    myText.text = $"...What I would do to get {CandyRequirement} candies...";
                    break;
                case 1:
                    myText.text = $"PLEASE tell me you have at least {CandyRequirement} candies!!!";
                    break;
                case 2:
                    myText.text = $"You know how COOL it would be if you had {CandyRequirement} candies??";               
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myText.text = "";
        }
    }

    public void TalkWithCrush()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                myText.text = $"Bruh, that's only like {Mathf.FloorToInt(PlayerActions.Instance.CandyAmount * Random.Range(0.6f, 0.8f))} candies";
                break;
            case 1:
                myText.text = $"What am I supposed to do with {Mathf.FloorToInt(PlayerActions.Instance.CandyAmount * Random.Range(0.6f, 0.8f))} candies?";
                break;
            case 2:
                myText.text = $"Nah, that ain't {CandyRequirement} candies, you need at least {CandyRequirement - (Mathf.FloorToInt(PlayerActions.Instance.CandyAmount * Random.Range(1.2f, 1.4f)))}";
                break;
            default:
                break;
         }
    }

    public void GiveCandy()
    {
        myText.text = "OMG THANKIUEHBJWICSKJCSKJ";
        PlayerActions.Instance.CandyAmount = 0;
    }
}
