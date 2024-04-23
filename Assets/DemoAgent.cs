using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class DemoAgent : Agent
{
    public Transform Target;
    public Transform GreenZone;
    public float speedMultiplier = 0.1f;
    public float rotationMultiplier = 5;

    bool targetReached = false;
    bool targetHidden = false;

    void Start()
    {

    }

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(0, 0.5f, 3.29f);
            this.transform.localRotation = Quaternion.identity;
        }
        float maxZoneX = 5f; // Maximale waarde voor x-positie
        float maxZoneZ = 4f; // Maximale waarde voor z-positie

        Target.localPosition = new Vector3(Random.value * maxZoneX, 0.5f, Random.value * maxZoneZ);
        targetReached = false;
        targetHidden = false;
        Target.gameObject.SetActive(true); // Zorg ervoor dat het doel zichtbaar is bij het starten van een nieuwe episode
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!targetReached && !targetHidden)
        {
            // Acties, size = 2
            Vector3 controlSignal = Vector3.zero;
            controlSignal.z = actions.ContinuousActions[0];
            transform.Translate(controlSignal * speedMultiplier);

            transform.Rotate(0.0f, rotationMultiplier * actions.ContinuousActions[1], 0.0f);

            // Beloningen
            float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

            // Doel bereikt
            if (distanceToTarget < 1.42f)
            {
                // Verberg het doel en markeer dat het doel is geraakt
                SetReward(0.5f);
                Target.gameObject.SetActive(false);
                targetReached = true;
            }
        }
        else
        {
            // Naar groene zone bewegen
            Vector3 moveDirection = (GreenZone.position - transform.position).normalized;
            transform.Translate(moveDirection * speedMultiplier);

            // Beloningen voor het bereiken van de groene zone
            if (Vector3.Distance(transform.position, GreenZone.position) < 1.0f)
            {
                SetReward(1.0f);
                EndEpisode();
            }
        }

        // Van het platform gevallen?
        if (this.transform.localPosition.y < 0)
        {
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
