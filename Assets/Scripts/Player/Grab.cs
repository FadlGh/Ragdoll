using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] Rigidbody2D hand;

    private GameObject _currentlyHolding;
    private bool _canGrab;
    private FixedJoint2D _joint;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _canGrab = true;
        }
        else
        {
            _canGrab = false;
        }

        if (!_canGrab && _currentlyHolding != null)
        {
            FixedJoint2D[] joints = _currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
            {
                Destroy(joints[i]);
            }

            _joint = null;
            _currentlyHolding = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (_canGrab && _currentlyHolding == null && col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            _currentlyHolding = col.gameObject;
            _joint = _currentlyHolding.AddComponent<FixedJoint2D>();
            _joint.connectedBody = hand;
        }
    }
}