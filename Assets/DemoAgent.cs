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

    private bool jump = true;
    public float jumpForce;
    [SerializeField] private GameObject prefabToSpawn;
    public override void OnEpisodeBegin()
    {
        jump = true;

        this.transform.position = initialAgentPosition;
        obstacle.position = initialObstaclePosition;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.ContinuousActions[0] > 0 && jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            jump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-1f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            AddReward(1f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Floor"))
        {
            jump = true;
        }
    }

    void Update()
    {
        if (obstacle.localPosition.z <= -10)
        {
            Debug.Log("Obstacle success! +0.5f");
            AddReward(obstacleSuccessReward);

            this.transform.position = initialAgentPosition;
            obstacle.position = initialObstaclePosition;
            canJump = true;
        }
        if (this.transform.localPosition.y > 10)
        {
            Debug.Log("Jumped to high! +0.5f");
            AddReward(-1);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
    }
}
