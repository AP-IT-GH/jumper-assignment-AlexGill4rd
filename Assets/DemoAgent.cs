using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class DemoAgent : Agent
{
    public float speedMultiplier = 1;
    public float jumpForce = 10f;
    public LayerMask obstacleLayer;
    RayPerceptionSensorComponent3D rayPerceptionSensor;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rayPerceptionSensor = GetComponent<RayPerceptionSensorComponent3D>();
    }

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(0, 0.5f, 3.29f);
            this.transform.localRotation = Quaternion.identity;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add observations from the Ray Perception Sensor 3D
        sensor.AddObservation(rayPerceptionSensor.ObservationStacks);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.y = actions.ContinuousActions[0];
        transform.Translate(controlSignal * speedMultiplier);

        // Reward the agent based on the observation
        if (GetCumulativeReward() <= 0f)
        {
            SetReward(-0.001f);
        }
        else
        {
            SetReward(0.001f);
        }

        if (this.transform.localPosition.y < 0)
        {
            SetReward(-1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }
}
