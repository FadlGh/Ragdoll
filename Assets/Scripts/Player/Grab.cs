using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Rigidbody2D hand;

    private GameObject currentlyHolding;
    private bool canGrab;
    private FixedJoint2D joint;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }

        if (!canGrab && currentlyHolding != null)
        {
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
            {
                Destroy(joints[i]);
            }

            joint = null;
            currentlyHolding = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (canGrab && currentlyHolding == null && col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            currentlyHolding = col.gameObject;
            joint = currentlyHolding.AddComponent<FixedJoint2D>();
            joint.connectedBody = hand;
        }
    }
}