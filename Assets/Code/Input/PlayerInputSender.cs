using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInputSender 
{
    public event Action<SharedEnums.InputButton> OnButtonDownEvent;
    public event Action<SharedEnums.InputButton> OnButtonUpEvent;
    public event Action<SharedEnums.InputButton> OnButtonHoldEvent;
}
