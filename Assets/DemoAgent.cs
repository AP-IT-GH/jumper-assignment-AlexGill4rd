using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class DemoAgent : Agent
{
    public Transform obstacle;

    public float jumpReward = 0.1f; // Small +reward for not jumping
    public float jumpPenalty = -0.1f; // Small -penalty for jumping
    public float obstacleCollisionPenalty = -1.0f; // Large -penalty for colliding with obstacle
    public float obstacleSuccessReward = 0.5f; // Medium +reward for successfully passing the obstacle

    private Rigidbody rb;
    private Vector3 initialAgentPosition;
    private Vector3 initialObstaclePosition;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialAgentPosition = this.transform.position;
        initialObstaclePosition = obstacle.transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        this.transform.position = initialAgentPosition;
        obstacle.position = initialObstaclePosition;
        canJump = true; // Reset canJump flag
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // No observations needed, as we only have discrete actions
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Take action from the agent
        int action = Mathf.FloorToInt(actionBuffers.DiscreteActions[0]);

        if (canJump && action == 1) // Jump action
        {
            Debug.Log("Jumped! " + jumpPenalty);
            canJump = false;
            AddReward(jumpPenalty); // Small -penalty for jumping
           this.GetComponent<Rigidbody>()
                .AddForce(
                new Vector3(0, 7, 0),
                ForceMode.Impulse
            );
        }
        else
        {
            AddReward(jumpReward); // Small +reward for not jumping
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Contains("Obstacle"))
        {
            Debug.Log("Touched obstacle! -1f");
            AddReward(obstacleCollisionPenalty); // Large -penalty for colliding with obstacle
            EndEpisode();
        }
    }

    void Update()
    {
        if (obstacle.localPosition.z <= -20)
        {
            Debug.Log("Obstacle success! +0.5f");
            AddReward(obstacleSuccessReward); // Medium +reward for successfully passing the obstacle

            this.transform.position = initialAgentPosition;
            obstacle.position = initialObstaclePosition;
            canJump = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // Space key for jumping
    }
}
