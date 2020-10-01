using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    public float skillTimeout = 3.5f;

    [SerializeField]
    public float skillUseTime = 3.5f;

    protected bool canUseSkill = true;
    public virtual void UseSkill()
    {
        if (!canUseSkill) return;

        canUseSkill = false;
        StartCoroutine(WaitTillEndSkill());
    }

    public virtual void EndSkill()
    {
        StartCoroutine(WaitTillCanUseSkill());
    }
    IEnumerator WaitTillEndSkill()
    {
        yield return new WaitForSeconds(skillUseTime);
        EndSkill();
    }

    IEnumerator WaitTillCanUseSkill()
    {
        yield return new WaitForSeconds(skillTimeout);
        canUseSkill = true;
    }
}
