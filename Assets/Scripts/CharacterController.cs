using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
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

    private float _epsilon = .0001f;
    private PauseViewController _pauseViewController;

    private void Awake() {
        _InputProvider = PlayerController.Instance.GetInput<GameplayInputProvider>(_IdProvider.Id);
        PlayerController.Instance.EnableInputProvider(_IdProvider.Id);
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
        StartCoroutine(Jump());
    }

    private IEnumerator Jump()
    {
        // first we disable jump input
        _InputProvider.OnJump -= JumpCharacter;
        // then we start jump animation and audio
        _Animator.SetTrigger("Jump");

        // manage audio
        _FootstepsAudio.mute = true;
        _JumpAudio.Play();

        // wait to enter in the jumping state
        yield return new WaitUntil(() => _Animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") || _Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpingIdle"));

        _Animator.SetTrigger("Jump");

        // wait to exit from the jumping state
        yield return new WaitUntil(() => !_Animator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") && !_Animator.GetCurrentAnimatorStateInfo(0).IsName("JumpingIdle"));

        // manage audio and enable again jumping input
        _FootstepsAudio.mute = false;
        _InputProvider.OnJump += JumpCharacter;
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
        PlayerController.Instance.DisableInputProvider(_IdProvider.Id);

        if (_pauseViewController) // == null
            return;

        Time.timeScale = .0f;

        _pauseViewController = Instantiate(_PauseViewPrefab);
    }
}
