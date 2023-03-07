using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool startingTiles = false;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject balloon;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float yOffsetMultiplier = 2f;
    
    private GameObject currentInteractable;
    

    // Start is called before the first frame update
    void Start() {
        SpawnInteractable();
    }

    public void SpawnInteractable() {
        if (startingTiles) {
            startingTiles = false;
            return;
        }
        
        float randomChance = Random.value;
        Debug.Log(randomChance);
        if (randomChance <= 0.5f) 
            return;
        //TODO object pooling
        if (currentInteractable != null) 
            Destroy(currentInteractable);
        if (randomChance >= 0.75f) {
            //SPAWN balloon
            currentInteractable = Instantiate(balloon, spawnPoint);
            currentInteractable.transform.position += Vector3.up * yOffsetMultiplier; 
            return;
        }
        //SPAWN Obstacle
        int randIdx = Random.Range(0, obstacles.Length - 1);
        currentInteractable = Instantiate(obstacles[randIdx], spawnPoint);
    }
}
