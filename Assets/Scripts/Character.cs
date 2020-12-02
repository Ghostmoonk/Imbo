using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    #region Components
    //CharacterController controller;
    Rigidbody rb;
    Collider col;
    Animator animator;
    #endregion

    Vector3 respawnPosition;
    Vector3 velocity;
    [Range(1, 150)]
    [SerializeField] float speed;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] AnimationCurve acceleration;
    [SerializeField] float accelerationTime;
    [Range(1f, 20f)]
    [SerializeField] float jumpForce;
    bool grounded;

    [SerializeField] LayerMask groundMask;

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        respawnPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0f)
            accelerationTime += Time.deltaTime;
        else
        {
            accelerationTime -= Time.deltaTime;
        }

        accelerationTime = Mathf.Clamp(accelerationTime, 0, acceleration[acceleration.length - 1].time);
        Debug.Log(acceleration[acceleration.length - 1].time);
        velocity.x = Mathf.Lerp(velocity.x, Input.GetAxis("Horizontal") * speed, acceleration.Evaluate(accelerationTime));


        //Gravity
        velocity.y -= gravity * Time.deltaTime;

        Vector3 leftColliderCorner = new Vector3(col.bounds.min.x, col.bounds.min.y + 0.0005f, col.bounds.center.z);
        Vector3 rightColliderCorner = new Vector3(col.bounds.max.x, col.bounds.min.y + 0.0005f, col.bounds.center.z);

        RaycastHit leftHit;
        RaycastHit rightHit;
        if (Physics.Raycast(leftColliderCorner, transform.TransformDirection(Vector3.down), out leftHit, 0.001f, groundMask) ||
                Physics.Raycast(rightColliderCorner, transform.TransformDirection(Vector3.down), out rightHit, 0.001f, groundMask))
        {
            //Debug.DrawRay(leftColliderCorner, transform.TransformDirection(Vector3.down) * leftHit.distance, Color.yellow);
            //Debug.DrawRay(rightColliderCorner, transform.TransformDirection(Vector3.down) * rightHit.distance, Color.red);
            grounded = true;
            if (velocity.y < 0f)
                velocity.y = 0f;
        }
        else
        {
            grounded = false;
        }
        //Debug.Log(velocity);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        //controller.Move(velocity * Time.deltaTime);

        rb.velocity = velocity;
        animator.SetFloat("MoveBlend", Mathf.InverseLerp(0, Mathf.Abs(Input.GetAxis("Horizontal") * speed), Mathf.Abs(velocity.x)));
        Debug.Log(Mathf.InverseLerp(0, Mathf.Abs(Input.GetAxis("Horizontal") * speed), Mathf.Abs(velocity.x)));
    }

    private void Jump()
    {
        velocity.y += jumpForce;
        Debug.Log(velocity);
    }

    public void Die()
    {
        Debug.Log("die");
        rb.detectCollisions = false;
        transform.position = respawnPosition;
        //transform.rotation = Quaternion.identity;
        rb.detectCollisions = true;
    }
}
