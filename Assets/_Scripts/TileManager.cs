using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Transform[] tiles;
    private int currentIdx;
    private int lastIdx;
    [SerializeField] private float nextPositionThreshold = 20f;
    [SerializeField] private float nextWaitTime = 1f;
    private WaitForSeconds waitTime;
    private Vector3 nextTilePos;
    private Vector3 lastTilePos;
    private Coroutine mainCoroutine;
    
    private void Awake() {
        Initialize();
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }
    
    private void OnDestroy() {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state) {
        switch (state) {
            case GameState.Menu:
                break;
            case GameState.Initialize:
                break;
            case GameState.InGame:
                HaltMainCoroutine();
                mainCoroutine = StartCoroutine(nameof(GoToNextPoint));
                break;
            case GameState.Pause:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                HaltMainCoroutine();
                break;
        }
    }

    private void HaltMainCoroutine() {
        if(mainCoroutine != null)
            StopCoroutine(mainCoroutine);
    }

    private void Initialize() {
        waitTime = new WaitForSeconds(nextWaitTime);
        lastIdx = tiles.Length - 1;
    }

    private IEnumerator GoToNextPoint() {
        yield return waitTime;
        GetNextPosition();
        AssignPosition();
        mainCoroutine = StartCoroutine(nameof(GoToNextPoint));
    }

    private void GetNextPosition() {
        lastTilePos = tiles[lastIdx].transform.position;
        nextTilePos = lastTilePos;
        nextTilePos.z += nextPositionThreshold;
        lastIdx = IncrementIndex(lastIdx, tiles.Length);
    }

    private void AssignPosition() {
        tiles[currentIdx].transform.position = nextTilePos;
        //TODO more efficient way
        if (tiles[currentIdx].TryGetComponent(out Spawner spawner)) {
            spawner.SpawnInteractable();
        }
        if (tiles[currentIdx].TryGetComponent(out PortalChanger portalChanger)) {
            portalChanger.RandomAssignPortals();
        }
        currentIdx = IncrementIndex(currentIdx, tiles.Length);
    }

    private int IncrementIndex(int index, int arrayLength) {
        index++;
        if (index >= arrayLength)
            index = 0;
        return index;
    }
}
