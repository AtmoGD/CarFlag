using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public static FlagController instance;

    public GameObject activeSystem = null;
    public GameObject endSystem = null;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }else
        {
            instance = this;
        }
    }
    public float lockTime = 1f;

    private CartController target = null;
    private bool isLocked = false;

    private void FixedUpdate()
    {
        if (!target) return;

        transform.position = Vector3.Lerp(transform.position,  target.transform.position, Time.deltaTime * 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("LeftCar") || other.CompareTag("RightCar")) && !target)
        {
            target = other.GetComponentInParent<CartController>();
            target.SetHasFlag(true);
            target.EnemyCollision += ChangeTarget;
            StartCoroutine(Lock());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("LeftGoal") || other.CompareTag("RightGoal")) && target)
        {
            GoalController goalController = other.GetComponent<GoalController>();
            if (goalController.GetTeam() != target.GetTeam())
            {
                target.HitGoalWithFlag();
                GameController.instance.EndGame(target);
                Reset();
            }
        }
    }

    public void ChangeTarget(CartController newTarget)
    {
        if (isLocked) return;
        
        if(target)
        {
            target.SetHasFlag(false);
            target.EnemyCollision -= ChangeTarget;
        }

        target = newTarget;
        target.SetHasFlag(true);
        target.transform.SendMessage("AddReward", 0.1f);
        target.EnemyCollision += ChangeTarget;

        StartCoroutine(Lock());
    }

    public void Reset()
    {
        if (target)
        {
            target.SetHasFlag(false);
            target.EnemyCollision -= ChangeTarget;
        }

        target = null;

        StopAllCoroutines();
        StartCoroutine(ResetSnitch());
    }

    IEnumerator ResetSnitch()
    {
        activeSystem.SetActive(false);
        endSystem.SetActive(true);

        foreach(ParticleSystem obj in endSystem.GetComponentsInChildren<ParticleSystem>())
        {
            obj.Play();
        }

        yield return new WaitForSeconds(2.5f);

        target = null;
        activeSystem.SetActive(true);
        endSystem.SetActive(false);
        transform.position = Vector3.zero;
    }
    IEnumerator Lock()
    {
        isLocked = true;
        yield return new WaitForSeconds(lockTime);
        isLocked = false;
    }
}
