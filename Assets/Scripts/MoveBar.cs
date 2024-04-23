using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float amplitude = 10f; // The amount of movement to each side
    public float speed = 2f; // The speed of oscillation
    private float initialX; // Initial x position of the bar

    void Start()
    {
        // Store the initial x position of the bar
        initialX = transform.position.x;
    }

    void Update()
    {
        // Calculate the horizontal movement using a sine wave
        float horizontalMovement = Mathf.Sin(Time.time * speed) * amplitude;

        // Set the new position of the bar
        transform.position = new Vector3(initialX + horizontalMovement, transform.position.y, transform.position.z);
    }
}
