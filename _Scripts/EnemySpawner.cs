using UnityEngine;
//required to have this spawn from the server
using UnityEngine.Networking;

//Network behavior needed
public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;
    private Vector3 newSpawnPosition;
    public int numberOfEnemies;

    //Onstartserver - when the server starts random enemies generated at rando positions and rotations
    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var newSpawnPosition = new Vector3(Random.Range(-25.0f, 25.0f), 0.0f, Random.Range(-25.0f, 25.0f));

            var spawnPosition = transform.position + newSpawnPosition;

            var spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);

            GameObject enemy = Instantiate (enemyPrefab, spawnPosition, spawnRotation);

            //spawns the enemies from the server
            NetworkServer.Spawn(enemy);
        }
    }
}