using System;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control the speed of the bar
    public float endPositionZ = -10f;
    private DemoAgent demoAgent;

    private void Start()
    {
        // Find the Agent GameObject within the same parent
        Transform parent = transform.parent;
        if (parent != null)
        {
            demoAgent = parent.GetComponentInChildren<DemoAgent>();
            if (demoAgent == null)
            {
                Debug.LogError("No DemoAgent found in the current parent.");
            }
        }
        else
        {
            Debug.LogError("MoveBar is not a child of any parent with an Agent.");
        }
    }

    private void Update()
    {
        // Move the bar backwards at the specified speed
        transform.Translate(Vector3.back * Mathf.Abs(speed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger appropriate actions when colliding with specific tags
        if (other.CompareTag("WallReward"))
        {
            if (demoAgent != null) demoAgent.OnWallHit();
        }
        if (other.CompareTag("Agent"))
        {
            if (demoAgent != null) demoAgent.OnAgentHit();
        }
    }
}
