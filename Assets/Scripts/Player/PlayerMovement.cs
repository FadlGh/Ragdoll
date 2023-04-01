using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    public float speed;
    public float maxSpeed;

    [Header("Components")]
    public Rigidbody2D rb;
    public Rigidbody2D armRb;
    public Rigidbody2D upperArmRb;
    public Camera cam;

    private float horizontal;
    private Animator am;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0) am.Play("Right Walk");
        if (horizontal < 0) am.Play("Left Walk");
        if (horizontal == 0) am.Play("Idle");

        Vector3 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 difference = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;

        if (Input.GetMouseButton(0))
        {
            armRb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, 100 * Time.fixedDeltaTime));
            upperArmRb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, 100 * Time.fixedDeltaTime));
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        rb.AddForce(Vector2.right * speed * horizontal, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * -Mathf.Abs(horizontal) / 8, ForceMode2D.Impulse);
    }
}
