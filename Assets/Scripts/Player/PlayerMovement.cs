using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    public float speed;
    public float maxSpeed;
    public float jumpPower;

    [Header("Components")]
    public Rigidbody2D rb;
    public Rigidbody2D armRb;
    public Rigidbody2D upperArmRb;
    public Camera cam;
    public Animator am;

    [Header("Grounded Settings")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;

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


      
         armRb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, 100 * Time.fixedDeltaTime));
         upperArmRb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, 100 * Time.fixedDeltaTime));
   
        if (Input.GetButtonDown("Jump") & isGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower);
        }

    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        rb.AddForce(Vector2.right * speed * horizontal, ForceMode2D.Impulse);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}