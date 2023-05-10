using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float _MaximumSpeed = 1.0f;

    private Vector3 _velocity;
    private float _epsilon = .001f;

    // Start is called before the first frame update
    void Start()
    {
        _velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _velocity * _MaximumSpeed * Time.deltaTime;
    }

    public void SetVelocity(float speed)
    {
        _velocity = new Vector3(.0f, .0f, -1.0f) * Mathf.Clamp(speed, -1.0f, 1.0f);
        if (speed > .0f + _epsilon || speed < .0f - _epsilon)
            transform.forward = _velocity.normalized;
    }
}
