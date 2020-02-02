using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetButtonsUI : MonoBehaviour
{
    [SerializeField] private PlanetButtonUI btnA_UI;
    [SerializeField] private PlanetButtonUI btnB_UI;
    [SerializeField] private PlanetButtonUI btnX_UI;
    [SerializeField] private PlanetButtonUI btnY_UI;

    public void StartBlinking(SharedEnums.InputButton btn)
    {
        switch (btn)
        {
            case SharedEnums.InputButton.A:
                btnA_UI.StartBlinkAnimation();
                break;
            case SharedEnums.InputButton.B:
                btnB_UI.StartBlinkAnimation();
                break;
            case SharedEnums.InputButton.X:
                btnX_UI.StartBlinkAnimation();
                break;
            case SharedEnums.InputButton.Y:
                btnY_UI.StartBlinkAnimation();
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public void StopBlinking()
    {
        btnA_UI.StopBlinkAnimation();
        btnB_UI.StopBlinkAnimation();
        btnX_UI.StopBlinkAnimation();
        btnY_UI.StopBlinkAnimation();
    }
}
