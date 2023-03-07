using System;
using UnityEngine;


public class MoveForward : MonoBehaviour
{
    
    [SerializeField] private float forwardSpeed = 8f;

    private void Update() {
        if (GameManager.CurrentState != GameState.InGame) return;
        this.transform.Translate(this.transform.forward * (forwardSpeed * Time.deltaTime));
    }
}