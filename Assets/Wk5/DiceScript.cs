using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPos;
    private bool hasLanded;
    private bool hasThrown;
    private int diceValue;
    [SerializeField] private DiceSide[] dsObjects;
    [SerializeField] private Vector2 torqueValue;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        initialPos = transform.position;
        dsObjects = GetComponentsInChildren<DiceSide>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            RollDice();

        if(rb.IsSleeping() && !hasLanded && hasThrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else if(rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            RollAgain();
        }
    }

    private void RollDice()
    {
        if (hasThrown && hasLanded)
        {
            ResetDice();
            return;
        }
        hasThrown = true;
        rb.useGravity = true;
        rb.AddTorque(RandomTorqueAmount(torqueValue),
            RandomTorqueAmount(torqueValue),
            RandomTorqueAmount(torqueValue));
    }

    private void RollAgain()
    {
        ResetDice();
        hasThrown = true;
        rb.useGravity = true;
        rb.AddTorque(RandomTorqueAmount(torqueValue),
            RandomTorqueAmount(torqueValue),
            RandomTorqueAmount(torqueValue));
    }

    private float RandomTorqueAmount(Vector2 torque)
    {
        return Random.Range(torque.x, torque.y);
    }

    private void ResetDice()
    {
        transform.position = initialPos;
        hasThrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }
}
