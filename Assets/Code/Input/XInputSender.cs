using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class XInputSender : PlayerInputSender
{

    private GamePadState lastFrameGamepadState;
    private GamePadState gamepadState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInputFromGamepad();
    }

    void GetInputFromGamepad()
    {

        this.lastFrameGamepadState = gamepadState;
        gamepadState = GamePad.GetState((PlayerIndex)playerIndex);
        CheckForGamepadButton(gamepadState.Buttons.A, lastFrameGamepadState.Buttons.A, SharedEnums.InputButton.A);
        CheckForGamepadButton(gamepadState.Buttons.B, lastFrameGamepadState.Buttons.B, SharedEnums.InputButton.B);
        CheckForGamepadButton(gamepadState.Buttons.X, lastFrameGamepadState.Buttons.X, SharedEnums.InputButton.X);
        CheckForGamepadButton(gamepadState.Buttons.Y, lastFrameGamepadState.Buttons.Y, SharedEnums.InputButton.Y);
    }

    private void CheckForGamepadButton(ButtonState button, ButtonState lastFrameButton, SharedEnums.InputButton inputButton)
    {
        //ButtonDown = Pressed this frame, but NOT released last frame.
        if (button == ButtonState.Pressed && lastFrameButton == ButtonState.Released)
        {
            Debug.Log("Pressed");
            RaiseOnButtonDownEvent(inputButton);
        }
    }
}
