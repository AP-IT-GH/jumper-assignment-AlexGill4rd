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

    private Vector3 obstaclePosLeft;
    private Vector3 obstaclePosRight;

    bool canJump = true;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        this.obstaclePosLeft = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 15);
        this.obstaclePosRight = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 15);
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

    public void OnWallHitCoin()
    {
        AddReward(-1f);
        EndEpisode();
    }
    public void OnAgentHitCoin()
    {
        AddReward(0.5f);
        ResetMyAgent();// Destroy the bar if it collides with a wall
    }
    public void OnWallHit()
    {
        AddReward(0.5f);
        ResetMyAgent();
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
    bool left = false;
    private void ResetMyAgent()
    {
        if (spawnedCoin != null)
            Destroy(spawnedCoin);
        if (spawnedObstacle != null)
            Destroy(spawnedObstacle);
        spawnedCoin = null;

        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);

        if (left)
        {
            spawnedObstacle = Instantiate(obstacle, obstaclePosLeft, Quaternion.identity, this.transform.parent);
            spawnedObstacle.GetComponent<MoveBar>().back = true; 
            left = false;
        }
        else
        {
            spawnedObstacle = Instantiate(obstacle, obstaclePosRight, Quaternion.identity, this.transform.parent);
            spawnedObstacle.GetComponent<MoveBar>().back = false; 
            left = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // Space key for jumping
    }
}
