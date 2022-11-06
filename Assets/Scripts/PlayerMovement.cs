using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator am;
    public Rigidbody2D rb;
    public float speed;
    private float frictionAmount = 0.3f;
    public Transform groundCheck;
    public LayerMask ground;
    private float horizontal;
    public float attackForce;

    [Header("Body Parts")]
    public GameObject Hand;
    private Rigidbody2D Hrb;
    private Transform Htm;
    public GameObject Torso;
    public Rigidbody2D Trb;

    //Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        Hrb = Hand.GetComponent<Rigidbody2D>();
        Htm = Hand.GetComponent<Transform>();
        Trb = Torso.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal != 0)
        {
            if (horizontal > 0 & IsGrounded())
            {
                am.Play("Stickman_Walk");
                rb.AddForce(Vector2.right * speed);
            }
            else if (horizontal < 0 & IsGrounded())
            {
                am.Play("Stickman_Walk_Left");
                rb.AddForce(Vector2.left * speed);
            }
        }
        else
        {
            am.Play("Stickman_Idle");
            //then we use either the friction amount(~ 0.2) or our velocity
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            //applies force against movement direction
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, ground); //return true or false based on the player state(grounded or in air)
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed & IsGrounded()) //To prevent flying glitch
        {

            Hrb.AddForce(Vector2.up * attackForce); //Swing the arm;
            Trb.AddForce(Vector2.down * attackForce); //To prevent jumping glitch
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, 1f);
    }
}
