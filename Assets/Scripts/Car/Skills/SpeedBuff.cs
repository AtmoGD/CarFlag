using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class SpeedBuff : SkillController
{
    [SerializeField]
    protected float speedMultiplier = 1.5f;


    private MovementController movementController = null;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
    }

    public override void UseSkill()
    {
        if (!canUseSkill) return;

        movementController.maxSpeed *= speedMultiplier;
        movementController.rotationSpeed *= speedMultiplier;

        base.UseSkill();
    }

    public override void EndSkill()
    {
        movementController.maxSpeed *= (1 / speedMultiplier);
        movementController.rotationSpeed *= (1 / speedMultiplier);

        base.EndSkill();
    }
}
