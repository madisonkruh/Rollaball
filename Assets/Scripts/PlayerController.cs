using System.Collections;
using System.Collections.Generic;

using UnityEngine;
// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // speed starting value 0
    public float speed = 0;

    // for the Text UI game objects
	public TextMeshProUGUI countText; // holds reference to UI text component on Count Text object
	public GameObject winTextObject; // holds reference to UI text component on Win Text object

    // will hold reference to Rigidbody being accessed
    private Rigidbody rb;

    // how many Pickup objects Player has collected
    private int count;

    private float movementX;
    private float movementY;

    public float threshold; // if player height below threshold, then trigger respawn

    // At the start of the game..
    void Start()
    {
        // reference to Rigidbody component attached to the Player sphere
        rb = GetComponent<Rigidbody>();

        // set intial value to 0
        count = 0;

        SetCountText();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        // gets Vector2 data from movementValue and stores it in Vector2 variable movementVector
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

        // if collect all 12 PickUp objects
		if (count >= 12) 
		{
            // activate Win Text
            winTextObject.SetActive(true);
		}
	}

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // add force to Rigidbody of Player sphere
        rb.AddForce(movement * speed);

        // if player falls below threshold then restart level
        if (transform.position.y < threshold){
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            // transform.position = new Vector3(0, 0, 0);
            // count = 0;

            // // reactivate(); // set all PickUp objects to active
            // GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("PickUp");
         
            // foreach (GameObject go in gameObjects) {
            //     go.SetActive(true);    
            // }  

            // SetCountText(); // update text displaying number of PickUps collected
        }
    }

    // detects contact between Player object and Pickup object without creating collision
    // called by Unity when Player object first touches a trigger collider
    // will be passed a reference to trigger collider that has been touched (other)
    private void OnTriggerEnter(Collider other)
    {
        // if other is tagged PickUp (ie. is a PickUp Prefab)
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false); // deactivate 
            count++;
            SetCountText(); // update text displaying number of PickUps collected
        }
    }
}