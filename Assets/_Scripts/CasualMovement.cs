using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private Animator anim;
    private Rigidbody rb;

    private Vector3 direction;

    //private int floatingAnim = Animator.StringToHash("Floating");
    //private int crawlingAnim = Animator.StringToHash("Crawling");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetFloat("nav_speed", 1);
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
        dir.z = 1;
        rb.MovePosition(transform.position + dir * (moveSpeed * Time.fixedDeltaTime));
    }
}
