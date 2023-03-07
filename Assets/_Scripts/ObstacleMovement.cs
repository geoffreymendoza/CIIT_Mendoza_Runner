using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private Transform selectedTransform;
    [Header("Rotation Stuff")]
    [SerializeField] private bool canRotate;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private float rotateDuration;
    [SerializeField] private int loopCount;
    [SerializeField] private LoopType loopType;

    [Header("Movement Stuff")]
    [SerializeField] private bool canMove;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float moveDuration; //LRDuration

    // Start is called before the first frame update
    void Start()
    {
        if (canRotate)
            selectedTransform.DOLocalRotate(rotation, rotateDuration, RotateMode.LocalAxisAdd).SetLoops(loopCount, loopType).SetEase(Ease.Linear);
        if (canMove)
            StartMovement();
    }

    private void StartMovement()
    {
        selectedTransform.DOLocalMove(endPosition, moveDuration).OnComplete(() => RestartMovement()).SetEase(Ease.Linear);
    }

    private void RestartMovement()
    {
        selectedTransform.DOLocalMove(startPosition, moveDuration).OnComplete(() => StartMovement()).SetEase(Ease.Linear);
    }
}
