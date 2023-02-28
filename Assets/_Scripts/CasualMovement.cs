using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasualMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 6f;
    [SerializeField] private float moveSpeed = 4f;
    private Animator anim;

    //private int floatingAnim = Animator.StringToHash("Floating");
    //private int crawlingAnim = Animator.StringToHash("Crawling");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("nav_speed", 1);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(this.transform.forward * (forwardSpeed * Time.deltaTime));

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0).normalized;
        transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }
}
