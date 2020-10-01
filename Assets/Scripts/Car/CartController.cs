using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(SkillController))]
public class CartController : Agent
{
    public delegate void OnCollisionWithEnemyCart(CartController controller);
    public event OnCollisionWithEnemyCart EnemyCollision;

    protected MovementController movementController = null;
    protected SkillController skillController = null;

    protected bool hasFlag = false;

    [SerializeField]
    protected int team = 0;
    [SerializeField]
    protected bool isPlayer = true;
    [SerializeField]
    //protected bool canMove = true;

    private void Awake()
    {
        movementController = GetComponent<MovementController>();
        skillController = GetComponent<SkillController>();
    }

    public virtual void HitGoalWithFlag()
    {
        Debug.Log("Wuuhuuu");
    }
    //public void SetCanMove(bool state) { canMove = state; }
    public void SetHasFlag(bool state) { hasFlag = state; }
    public int GetTeam() { return team; }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (team == 0)
        {
            if (collision.transform.CompareTag("RightCar"))
            {
                CartController collisionController = collision.transform.GetComponent<CartController>();
                if (collisionController.GetTeam() != this.team && hasFlag)
                {
                    EnemyCollision?.Invoke(collisionController);
                }
            }
        }
        else
        {
            if (collision.transform.CompareTag("LeftCar"))
            {
                CartController collisionController = collision.transform.GetComponent<CartController>();
                if (collisionController.GetTeam() != this.team && hasFlag)
                {
                    EnemyCollision?.Invoke(collisionController);
                }
            }
        }
        
    }

}
