using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class CarAgent : CartController
{
    private void FixedUpdate()
    {
        if(transform.position.y < -3)
        {
            Vector3 rndPos = Vector3.zero;
            transform.localPosition = rndPos;
            EndEpisode();
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = SpawnController.instance.GetRandomSpawnPoint();
    }
    /*
    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(team);
        sensor.AddObservation(movementController.canMove);
        sensor.AddObservation(movementController.maxSpeed);
        sensor.AddObservation(movementController.acceleration);
        sensor.AddObservation(movementController.rotationSpeed);
        sensor.AddObservation(movementController.isOnGround);
        sensor.AddObservation(skillController.skillTimeout);
        sensor.AddObservation(skillController.skillUseTime);
        
    }
    */
    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-0.000001f);

        if (hasFlag)
        {
            AddReward(0.001f);
        }

        if (vectorAction[0] > 0f)
        {
            Vector3 dir = new Vector3(vectorAction[1], 0, vectorAction[2]);
            dir = transform.position + dir;
            movementController.Move(dir);
        }

        if (vectorAction[3] > 0) skillController.UseSkill();
    }

    public override void HitGoalWithFlag()
    {
        //base.HitGoalWithFlag();
        AddReward(1f);
    }
    public override void Heuristic(float[] actionsOut)
    {
        if (!isPlayer) return;

        AddReward(-0.00001f);

        if (hasFlag)
        {
            AddReward(0.0001f);
        }

        if (actionsOut[0] > 0f)
        {
            Vector3 dir = new Vector3(actionsOut[1], 0, actionsOut[2]);
            dir = transform.position + dir;
            movementController.Move(dir);
        }

        if (actionsOut[3] > 0) skillController.UseSkill();
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Border"))
        {
            AddReward(-0.01f);
        }
    }
}
