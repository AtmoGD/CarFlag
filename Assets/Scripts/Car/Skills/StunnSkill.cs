using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnSkill : SkillController
{
    public float stunnTime = 1.3f;

    bool isActive = false;
    int team = 0;
    CartController stunnedCar = null;
    private void Start()
    {
        team = GetComponent<CartController>().GetTeam();
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;

        if (collision.transform.CompareTag("RightCar") || collision.transform.CompareTag("LeftCar"))
        {
            CartController collisionController = collision.transform.GetComponent<CartController>();
            if (collisionController.GetTeam() != this.team)
            {
                stunnedCar = collisionController;
                EndSkill();
                StartCoroutine(StunnCar());
            }
        }

    }
    public override void UseSkill()
    {
        if (!canUseSkill) return;

        isActive = true;

        base.UseSkill();
    }

    public override void EndSkill()
    {
        isActive = false;

        base.EndSkill();
    }

    IEnumerator StunnCar()
    {
        MovementController mc = stunnedCar.transform.GetComponent<MovementController>();
        mc.SetCanMove(false);

        yield return new WaitForSeconds(stunnTime);

        mc.SetCanMove(true);
        stunnedCar = null;
    }
}
