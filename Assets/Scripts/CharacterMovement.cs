using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _MaximumSpeed = 1.0f;
    [SerializeField]
    private float _JumpSpeed = 1.0f;

    [SerializeField]
    private float _Gravity = -1.0f;

    private Vector3 _velocity;
    private float _epsilon = .001f;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        // initializations
        _velocity = Vector3.zero;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_characterController.isGrounded)
            _velocity.y += -.01f;
        else
            _velocity.y += _Gravity * Time.deltaTime;

        _characterController.Move(_velocity * _MaximumSpeed * Time.deltaTime);
    }

    public void SetVelocity(float speed)
    {
        _velocity = new Vector3(.0f, .0f, -1.0f) * Mathf.Clamp(speed, -1.0f, 1.0f);
        if (speed > .0f + _epsilon || speed < .0f - _epsilon)
            transform.forward = _velocity.normalized;
    }

    public void Jump()
    {
        _velocity.y = _JumpSpeed;
    }

    public bool IsJumping()
    {
        return !_characterController.isGrounded;
    }
}
