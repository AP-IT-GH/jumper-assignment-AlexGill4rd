using System;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control the speed of the bar
    public float endPositionZ = -10f;
    private DemoAgent demoAgent;

    private void Start()
    {
        GameObject agentObject = GameObject.FindWithTag("Agent");
        demoAgent = agentObject.GetComponent<DemoAgent>();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Mathf.Abs(speed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallReward"))
        {
            demoAgent.OnWallHit();
        }
        if (other.CompareTag("Agent"))
        {
            demoAgent.OnAgentHit();
        }
    }
}
