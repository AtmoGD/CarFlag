using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public List<Transform> wheels = new List<Transform>();
    public Transform centerOfMass = null;
    public float tireRaycastLength = 0.4f;
    public bool isOnGround = false;

    public float maxSpeed = 5f;
    public float rotationSpeed = 5f;
    public float acceleration = 0.5f;
    public bool canMove = true;

    private Rigidbody rb;
    private int groundLayer = 1 << 8;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        isOnGround = CarIsOnGround();

        if(!isOnGround)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime);
        }
    }

    public void SetCanMove(bool state) { canMove = state; }

    public void Move(Vector3 dir)
    {
        if (!canMove) return;

        rb.centerOfMass = rb.transform.InverseTransformPoint(centerOfMass.position);

        if (!isOnGround) return;
       
        Vector3 targetDirection = (dir - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

        Vector3 velocity = rb.velocity;
        Vector3 finalVelocity = Vector3.Lerp(velocity, transform.forward.normalized * maxSpeed, acceleration * Time.deltaTime);
        rb.velocity = finalVelocity;
    }

    private bool CarIsOnGround()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            Transform current = wheels[i];
            if (!Physics.Raycast(current.position, Vector3.down, tireRaycastLength, groundLayer))
            {
                return false;
            }
        }
        return true;
    }
   
}
