using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // spped starting value 0
    public float speed = 0;

    // will hold reference to Rigidbody being accessed
    private Rigidbody rb;

    // how many Pickup objects Player has collected
    private int count;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        // reference to Rigidbody component attached to the Player sphere
        rb = GetComponent<Rigidbody>();

        // set intial value to 0
        count = 0;
    }

    private void OnMove(InputValue movementValue)
    {
        // gets Vector2 data from movementValue and stores it in Vector2 variable movementVector
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // add force to Rigidbody of Player sphere
        rb.AddForce(movement * speed);
    }

    // detects contact between Player object and Pickup object without creating collision
    // called by Unity when Player object first touches a trigger collider
    // will be passed a reference to trigger collider that has been touched (other)
    private void OnTriggerEnter(Collider other)
    {
        // if other is tagged PickUp (ie. is a PickUp Prefab)
        if(other.gameObject.CompareTag("PickUp")){
            // deactivate 
            other.gameObject.SetActive(false);
        }
    }
}