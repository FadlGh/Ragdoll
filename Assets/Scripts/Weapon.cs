using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponHolder;
    private Transform T;
    private Collider2D co;
    private Rigidbody2D rb;
    public GameObject parent;

    public float force;

    // Start is called before the first frame update
    void Start()
    {
        T = weaponHolder.GetComponent<Transform>();
        co = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        Collider2D[] colliders = parent.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != co)
            {
                Physics2D.IgnoreCollision(co, colliders[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform tempTransform = this.gameObject.GetComponent<Transform>();
        tempTransform.position = T.position; // Same position as weapon holder
        tempTransform.rotation = T.rotation; // Same rotation as weapon holder
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
        {
            if (collision.relativeVelocity.magnitude > 100)
            {
                if (!collision.gameObject.CompareTag("unbreakable"))
                {
                    print("ded");
                    collision.gameObject.GetComponent<HingeJoint2D>().enabled = false;
                }
            }
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
        }
    }
}
