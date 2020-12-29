using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    Rigidbody2D rigidbody;
    IsometricPlayerRenderer isometricPlayerRenderer;

    public Vector2 movementVector;
    void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        isometricPlayerRenderer = GetComponent<IsometricPlayerRenderer>();
        movementVector = new Vector2(0,0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 currentPos = rigidbody.position;
        movementVector = Vector2.ClampMagnitude(movementVector,1);
        Vector2 movement = movementVector * moveSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isometricPlayerRenderer.SetDirection(movement);
        rigidbody.MovePosition(newPos);
    }
}
