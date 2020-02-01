using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class DebugPlayerInputSender : PlayerInputSender
{
    public SharedEnums.Player player;
    private GamePadState lastFrameGamepadState;
    private GamePadState gamepadState;
    private void Update()
    {

        this.lastFrameGamepadState = gamepadState;
        gamepadState = GamePad.GetState((PlayerIndex)player);
        CheckForKey(KeyCode.Q);
        CheckForKey(KeyCode.W);
        CheckForKey(KeyCode.E);
        CheckForKey(KeyCode.R);
        CheckForGamepadButton(gamepadState.Buttons.A,lastFrameGamepadState.Buttons.A,SharedEnums.InputButton.A);
        CheckForGamepadButton(gamepadState.Buttons.B, lastFrameGamepadState.Buttons.B, SharedEnums.InputButton.B);
        CheckForGamepadButton(gamepadState.Buttons.X, lastFrameGamepadState.Buttons.X, SharedEnums.InputButton.X);
        CheckForGamepadButton(gamepadState.Buttons.Y, lastFrameGamepadState.Buttons.Y, SharedEnums.InputButton.Y);
    }

    private void GetGamepad()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button0))Debug.Log("A1");
        if (Input.GetKeyDown(KeyCode.Joystick2Button0)) Debug.Log("A2");
    }
    private void CheckForGamepadButton(ButtonState button, ButtonState lastFrameButton,SharedEnums.InputButton inputButton)
    {
        //ButtonDown = Pressed this frame, but NOT released last frame.
        if(button == ButtonState.Pressed && lastFrameButton == ButtonState.Released)
        {
            Debug.Log("Pressed");
            RaiseOnButtonDownEvent(inputButton);
        }
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
