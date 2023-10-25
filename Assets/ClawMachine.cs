using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine : MonoBehaviour
{
    public GameObject objectToSpawn; // The object you want to spawn.
    public Transform parentObject;   // The parent object where objects should spawn inside.
    public float minSpawnX;    // Minimum X position.
    public float maxSpawnX;     // Maximum X position.
    public float minSpawnZ;    // Minimum Z position.
    public float maxSpawnZ;     // Maximum Z position.
    public Vector2 forbiddenXRange; // Forbidden X range.
    public Vector2 forbiddenZRange; // Forbidden Z range.
    public int numberOfObjectsToSpawn; // Number of objects to spawn.
    
    private void Start()
    {
        SpawnRandomObjects();
    }

    private void SpawnRandomObjects()
    {
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 spawnPosition;

            // Generate random positions within the specified range and avoid forbidden areas.
            do
            {
                float randomX = Random.Range(minSpawnX, maxSpawnX);
                float randomZ = Random.Range(minSpawnZ, maxSpawnZ);

                spawnPosition = new Vector3(randomX, 3f, randomZ) + parentObject.position;
            } while (IsPositionInForbiddenArea(spawnPosition));

            // Instantiate the object at the generated position.
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, parentObject);
        }
    }

    private bool IsPositionInForbiddenArea(Vector3 position)
    {
        return position.x >= forbiddenXRange.x + parentObject.position.x && position.x <= forbiddenXRange.y + parentObject.position.x &&
               position.z >= forbiddenZRange.x + parentObject.position.z && position.z <= forbiddenZRange.y + parentObject.position.z;
    }
}
