using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerInputSender : PlayerInputSender
{
    private void Update()
    {
        CheckForKey(KeyCode.Q);
        CheckForKey(KeyCode.W);
        CheckForKey(KeyCode.E);
        CheckForKey(KeyCode.R);
    }

    private void CheckForKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            RaiseOnButtonDownEvent(GetInputButtonFromKey(key));
        }
        if (Input.GetKeyUp(key))
        {
            RaiseOnButtonUpEvent(GetInputButtonFromKey(key));
        }
        if (Input.GetKey(key))
        {
            RaiseOnButtonHoldEvent(GetInputButtonFromKey(key));
        }
    }

    private SharedEnums.InputButton GetInputButtonFromKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Q:
                return SharedEnums.InputButton.A;
            case KeyCode.W:
                return SharedEnums.InputButton.B;
            case KeyCode.E:
                return SharedEnums.InputButton.X;
            case KeyCode.R:
                return SharedEnums.InputButton.Y;
            default:
                throw new System.NotImplementedException();
        }
    }
}
