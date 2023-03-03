using System;
using UnityEngine;


public class MoveForward : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 8f;

    private void Update() {
        this.transform.Translate(this.transform.forward * (forwardSpeed * Time.deltaTime));
    }
}