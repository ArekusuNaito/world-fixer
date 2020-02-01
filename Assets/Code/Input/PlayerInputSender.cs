using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInputSender 
{
    public event Action<SharedEnums.InputButton> OnButtonPressEvent;
    public event Action<SharedEnums.InputButton> OnButtonReleaseEvent;
    public event Action<SharedEnums.InputButton> OnButtonEvent;
}
