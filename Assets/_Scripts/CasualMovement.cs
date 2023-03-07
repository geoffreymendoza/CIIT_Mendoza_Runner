using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualMovement : MonoBehaviour
{
    private bool startGame = false;
    [SerializeField] private float moveSpeed = 4f;
    private Rigidbody rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(this.transform.forward * (forwardSpeed * Time.deltaTime));
        GetInput();
    }

    private void FixedUpdate() {
        Move(direction);
    }

    private void GetInput() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        direction = new Vector3(horizontalInput, 0, 0).normalized;
    }

    private void Move(Vector3 dir) {
        if (GameManager.CurrentState != GameState.InGame) return;
        dir.z = 1;
        rb.MovePosition(transform.position + dir * (moveSpeed * Time.fixedDeltaTime));
    }
}
