using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class DemoAgent : Agent
{
    public float force = 15f;
    public Transform reset;
    public GameObject obstacle;
    public GameObject coin;

    private GameObject spawnedCoin;
    private GameObject spawnedObstacle;

    private Rigidbody rb;

    private Vector3 obstaclePos;

    bool canJump = true;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        this.obstaclePos = obstacle.transform.position;
        ResetMyAgent();
        canJump = true;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (actionBuffers.DiscreteActions[0] == 1 && canJump)
        {
            UpForce();
            AddReward(-0.1f);
            canJump = false;
        }
    }
    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
    }
    public void OnWallHit()
    {
        AddReward(0.5f);
        ResetMyAgent();
    }
    public void OnWallHitCoin()
    {
        AddReward(-0.5f);
        EndEpisode();
    }
    public void OnAgentHitCoin()
    {
        AddReward(1.0f);
        ResetMyAgent();// Destroy the bar if it collides with a wall
    }
    public void OnAgentHit()
    {
        AddReward(-1.0f);
        EndEpisode();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

    private void UpForce()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
    private void ResetMyAgent()
    {
        Destroy(spawnedCoin);
        Destroy(spawnedObstacle);
        spawnedCoin = null;

        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);

        bool spawnCoin = Random.value > 0.7f;

        if (spawnCoin)
        {
            spawnedCoin = Instantiate(coin, obstaclePos, Quaternion.identity);
        }
        else
        {
            spawnedObstacle = Instantiate(obstacle, obstaclePos, Quaternion.identity);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // Space key for jumping
    }
}
