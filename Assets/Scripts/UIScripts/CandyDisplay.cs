using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CandyDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI candyText;

    void Start()
    {
        PlayerActions.Instance.OnCandyChanged.AddListener(UpdateCandyUI);
        UpdateCandyUI();
    }

    private void UpdateCandyUI()
    {
        candyText.text = $"Candy: {PlayerActions.Instance.CandyAmount} / {CrushLogic.Instance.CandyRequirement}";
    }
}
