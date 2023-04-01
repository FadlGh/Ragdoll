using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private bool hold;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hold)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
            if (rb != null)
            {
                fj.connectedBody = rb;
            }
        }
    }
}
