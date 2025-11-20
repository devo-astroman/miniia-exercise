using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody _rbody;
    [SerializeField] private float _speed = 10f;

    private bool _goForward = false;
    private bool _goBackward = false;
    private bool _goLeft = false;
    private bool _goRight = false;

    // Update is called once per frame
    void Update()
    {
        if (_goForward)
        {
            _rbody.AddForce(Vector3.forward * _speed, ForceMode.Acceleration);
        }
        else if (_goBackward)
        {
            _rbody.AddForce(Vector3.forward * _speed *-1, ForceMode.Acceleration);
        }
        else if (_goLeft)
        {
            _rbody.AddForce(Vector3.right * _speed *-1, ForceMode.Acceleration);
        }
        else if (_goRight)
        {
            _rbody.AddForce(Vector3.right * _speed, ForceMode.Acceleration);
        }       

    }


    public void MoveForward()
    {
        _goForward = true;
        _goBackward = false;
        _goLeft = false;
        _goRight = false;
    }

    public void MoveBackward()
    {
        _goForward = false;
        _goBackward = true;
        _goLeft = false;
        _goRight = false;
    }

    public void MoveLeft()
    {
        print("Move Left");
        _goForward = false;
        _goBackward = false;
        _goLeft = true;
        _goRight = false;
    }

    public void MoveRight()
    {
        _goForward = false;
        _goBackward = false;
        _goLeft = false;
        _goRight = true;
    }

    public void Stop()
    {
        _goForward = false;
        _goBackward = false;
        _goLeft = false;
        _goRight = false;
    }
}
