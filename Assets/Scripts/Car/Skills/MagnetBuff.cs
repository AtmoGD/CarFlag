using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : SkillController
{
    public float magnetDistance = 3f;

    private bool isActive = false;

    private void FixedUpdate()
    {
        if (!isActive) return;

        for (int i = 0; i <= 360; i += 10)
        {
            Vector3 direction = GetDirectionFromAngle(i, Vector3.up, transform.forward);

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, magnetDistance))
            {
                if (hit.transform.CompareTag("Flag"))
                {
                    hit.transform.SendMessage("ChangeTarget", this.GetComponent<CartController>());
                    StopAllCoroutines();
                    EndSkill();
                }
            }
        }
    }
    /*
    public void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Flag"))
        {
            other.SendMessage("ChangeTarget", this.GetComponent<CartController>());
            StopAllCoroutines();
            EndSkill();
        }
    }
    */
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
    Vector3 GetDirectionFromAngle(float degrees, Vector3 axis, Vector3 zerothDirection)
    {
        Quaternion rotation = Quaternion.AngleAxis(degrees, axis);
        return (rotation * zerothDirection);
    }
}
