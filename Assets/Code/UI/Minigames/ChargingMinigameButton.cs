using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingMinigameButton : MonoBehaviour
{
    [SerializeField] private Text buttonText;

    public void SetButtonImage(SharedEnums.InputButton btn)
    {
        buttonText.text = btn.ToString();
    }
}
