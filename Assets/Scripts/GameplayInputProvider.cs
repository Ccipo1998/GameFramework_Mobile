using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputProvider : InputProvider
{
    #region Delegate
    public OnFloatDelegate OnMove;
    public OnVoidDelegate OnJump;
    public OnVoidDelegate OnPause;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Move;

    [SerializeField]
    private InputActionReference _Jump;

    [SerializeField]
    private InputActionReference _Pause;

    private void OnEnable()
    {
        _Move.action.Enable();
        _Jump.action.Enable();
        _Pause.action.Enable();

        _Move.action.performed += MovePerfomed;
        _Jump.action.performed += JumpPerfomed;
        _Pause.action.performed += PausePerformed;
    }

    private void OnDisable()
    {
        _Move.action.Disable();
        _Jump.action.Disable();
        _Pause.action.Disable();

        _Move.action.performed -= MovePerfomed;
        _Jump.action.performed -= JumpPerfomed;
        _Pause.action.performed -= PausePerformed;
    }

    private void MovePerfomed(InputAction.CallbackContext obj)
    {
        float value = obj.action.ReadValue<float>();
        OnMove?.Invoke(value);
    }

    private void JumpPerfomed(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
    }
}
