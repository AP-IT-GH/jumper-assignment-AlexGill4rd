using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class DemoAgent : Agent
{
    public float force = 15f;
    public Transform reset = null;
    public GameObject obstacle = null;
    private Rigidbody rb = null;

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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") == true)
        {
            AddReward(-1.0f);
            EndEpisode();
        }
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
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
        this.obstacle.transform.position = obstaclePos;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; // Space key for jumping
    }
}
