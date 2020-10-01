using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public FixedJoystick movementJoystick = null;

    private bool useSkill = false;

    [SerializeField]
    private GameObject target = null;
    void Update()
    {
        if (!movementJoystick || !target) return;


        if (movementJoystick.Direction.magnitude > 0f || useSkill)
        {
            float[] inputs = new float[4] { 1, movementJoystick.Direction.x, movementJoystick.Direction.y, useSkill ? 1 : 0 };
            target.SendMessage("Heuristic", inputs);
            useSkill = false;
        }
    }

    public void UseSkill()
    {

        useSkill = true;
    }
}
