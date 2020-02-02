using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInputSender : MonoBehaviour
{
    public event Action<SharedEnums.InputButton> OnButtonDownEvent;
    public event Action<SharedEnums.InputButton> OnButtonUpEvent;
    public event Action<SharedEnums.InputButton> OnButtonHoldEvent;

    protected void RaiseOnButtonDownEvent(SharedEnums.InputButton btn)
    {
        if (OnButtonDownEvent != null)
            OnButtonDownEvent(btn);
    }

    protected void RaiseOnButtonUpEvent(SharedEnums.InputButton btn)
    {
        if (OnButtonUpEvent != null)
            OnButtonUpEvent(btn);
    }

    protected void RaiseOnButtonHoldEvent(SharedEnums.InputButton btn)
    {
        if (OnButtonHoldEvent != null)
            OnButtonHoldEvent(btn);
    }

}
