using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    [SerializeField]
    private idContainer _IdProvider;
    private GameplayInputProvider _InputProvider;

    [SerializeField]
    private PauseViewController _PauseViewPrefab;

    [SerializeField]
    private CharacterMovement _Movement;
    [SerializeField]
    private Animator _Animator;
    [SerializeField]
    private AudioSource _FootstepsAudio;
    [SerializeField]
    private AudioSource _JumpAudio;

    [SerializeField]
    private string _OnPauseFlowEvent;

    private float _epsilon = .0001f;
    
    private void Awake() {
        _InputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);
        //PlayerController.Instance.EnableInputProvider(_IdProvider.Id);
        //_characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _InputProvider.OnMove += MoveCharacter;
        _InputProvider.OnJump += JumpCharacter;
        _InputProvider.OnPause += PauseGame;
    }

    private void OnDisable()
    {
        _InputProvider.OnMove -= MoveCharacter;
        _InputProvider.OnJump -= JumpCharacter;
        _InputProvider.OnPause -= PauseGame;
    }

    private void JumpCharacter()
    {
        // cant jump
        if (_Movement.IsJumping())
            return;

        StartCoroutine(HandleJumpAnimationAndAudio());
    }

    private IEnumerator HandleJumpAnimationAndAudio()
    {
        // jump movement
        _Movement.Jump();
        // trigger jump animation
        _Animator.SetBool("IsJumping", true);
        // disable movement audio
        _FootstepsAudio.mute = true;
        // jump audio
        _JumpAudio.Play();

        // wait for the character controller to be updated
        yield return new WaitUntil(() => _Movement.IsJumping());
        // wait for the return on the ground
        yield return new WaitUntil(() => !_Movement.IsJumping());

        // trigger exit from jump animation
        _Animator.SetBool("IsJumping", false);
        // enable audio again
        _FootstepsAudio.mute = false;
    }

    private void MoveCharacter(float value)
    {
        _Movement.SetVelocity(value);
        if (value > .0f + _epsilon || value < .0f - _epsilon)
        {
            _Animator.SetBool("IsMoving", true);
            _FootstepsAudio.Play();
        }
        else
        {
            _Animator.SetBool("IsMoving", false);
            _FootstepsAudio.Stop();
        }
    }

    private void PauseGame()
    {
        _Movement.SetVelocity(.0f);
        Time.timeScale = .0f;

        BoltFlowSystem.Instance.TriggerFSMevent(_OnPauseFlowEvent);
    }
}
