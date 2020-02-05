using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardInputSender : PlayerInputSender
{

    private void Update()
    {

        GetGamepad(this.playerIndex);
    }

    private void GetGamepad(SharedEnums.Player player)
    {
        if (Input.GetButtonDown($"{player.ToString()}-A"))
        {
            RaiseOnButtonDownEvent(SharedEnums.InputButton.A);
        }
        if (Input.GetButtonDown($"{player.ToString()}-B"))
        {
            RaiseOnButtonDownEvent(SharedEnums.InputButton.B);
        }
        if (Input.GetButtonDown($"{player.ToString()}-X"))
        {
            RaiseOnButtonDownEvent(SharedEnums.InputButton.X);
        }
        if (Input.GetButtonDown($"{player.ToString()}-Y"))
        {
            RaiseOnButtonDownEvent(SharedEnums.InputButton.Y);
        }

    }
}
