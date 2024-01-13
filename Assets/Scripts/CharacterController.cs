using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    Vector2 motionVector;
    public Vector2 saved_motionVector;
    Rigidbody2D rigidbody2d;
    Animator animator;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        UpdateMovementVector();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Moving();
        UpdateSavedXY();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * speed;
    }

 
    private void Moving()
    {
        if (isMoving())
        {
            animator.SetBool("isMoving", true);
            saved_motionVector = motionVector;
            animator.SetFloat("X", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("Y", Input.GetAxisRaw("Vertical"));
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetFloat("X", saved_motionVector.x);
            animator.SetFloat("Y", saved_motionVector.y);
        }
    }

    private void UpdateSavedXY()
    {
        animator.SetFloat("last_X", saved_motionVector.x);
        animator.SetFloat("last_Y", saved_motionVector.y);
    }

    private bool isMoving()
    {
        if (motionVector != Vector2.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateMovementVector()
    {
        motionVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }
}
