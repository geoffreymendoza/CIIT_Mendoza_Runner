using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{ 
    [Header("Right Collider")]
    [SerializeField] private Vector3 rightColliderSize = Vector3.one;
    [SerializeField] private Vector3 rightOffsetSize = Vector3.one;
    [Header("Left Collider")]
    [SerializeField] private Vector3 leftColliderSize = Vector3.one;
    [SerializeField] private Vector3 leftOffsetSize = Vector3.one;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 currentPos = this.transform.position;
        Collider[] cols = Physics.OverlapBox(currentPos + rightOffsetSize, rightColliderSize / 2);
        if (cols.Length > 0) {
            foreach (var c in cols) {
                Debug.Log(c.gameObject.name);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        var currentPos = this.transform.position;
        Gizmos.DrawWireCube(currentPos + rightOffsetSize, rightColliderSize);
        Gizmos.DrawWireCube(currentPos + leftOffsetSize, leftColliderSize);
    }
}
