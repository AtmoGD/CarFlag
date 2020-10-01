using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField]
    private int team = 0;

    public int GetTeam() { return team; }
}
