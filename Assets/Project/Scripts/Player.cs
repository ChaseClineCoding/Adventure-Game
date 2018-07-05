using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Visuals")]
    public GameObject model;

    [Header("Movement")]
    public float movingVelocity;
    public float jumpVelocity;
    public float rotatingSpeed = 2f;

    private bool canJump;
    private Rigidbody playerRigidbody;
    private Quaternion targetModelRotation;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();

        targetModelRotation = Quaternion.Euler(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        // Raycast to check if player can jump
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f))
        {
            canJump = true;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, rotatingSpeed * Time.deltaTime);

        ProcessInput();
	}

    void ProcessInput()
    {
        // Resetting velocity on XZ plane
        playerRigidbody.velocity = new Vector3(
            0,
            playerRigidbody.velocity.y,
            0);

        // Move on the XZ plane
        if (Input.GetKey("right"))
        {
            playerRigidbody.velocity = new Vector3(
               movingVelocity,
               playerRigidbody.velocity.y,
               playerRigidbody.velocity.z);

            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey("left"))
        {
            playerRigidbody.velocity = new Vector3(
               -movingVelocity,
               playerRigidbody.velocity.y,
               playerRigidbody.velocity.z);

            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetKey("up"))
        {
            playerRigidbody.velocity = new Vector3(
               playerRigidbody.velocity.x,
               playerRigidbody.velocity.y,
               movingVelocity);

            targetModelRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey("down"))
        {
            playerRigidbody.velocity = new Vector3(
               playerRigidbody.velocity.x,
               playerRigidbody.velocity.y,
               -movingVelocity);

            targetModelRotation = Quaternion.Euler(0, 0, 0);
        }

        // Check for jumps
        if (canJump && Input.GetKeyDown("space"))
        {
            canJump = false;
            playerRigidbody.velocity = new Vector3(
                playerRigidbody.velocity.x,
                jumpVelocity,
                playerRigidbody.velocity.z);
        }
    }
}
