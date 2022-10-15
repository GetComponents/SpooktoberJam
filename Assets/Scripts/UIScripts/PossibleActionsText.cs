using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PossibleActionsText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textPrompt;

    void Start()
    {
        PlayerActions.Instance.OnEnableSpookPrompt.AddListener(EnableSpookPrompt);
        PlayerActions.Instance.OnDisableSpookPrompt.AddListener(DisableSpookPrompt);
    }

    private void EnableSpookPrompt()
    {
        textPrompt.text = "Press SPACE to Spook";
    }

    private void DisableSpookPrompt()
    {
        textPrompt.text = "";
    }
}
