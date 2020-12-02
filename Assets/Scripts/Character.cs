using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Character : MonoBehaviour
{
    Vector3 respawnPosition;
    Vector3 velocity;
    [Range(1, 150)]
    [SerializeField] float speed;
    [SerializeField] float gravity;
    [Range(1f, 20f)]
    [SerializeField] float jumpForce;
    //CharacterController controller;
    Rigidbody rb;
    Collider col;
    bool grounded;

    [SerializeField] LayerMask groundMask;

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        respawnPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }


    private void Update()
    {
        velocity.x = Input.GetAxis("Horizontal") * speed;
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
        transform.rotation = Quaternion.identity;
        rb.detectCollisions = true;
    }
}
