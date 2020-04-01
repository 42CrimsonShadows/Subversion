using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : NetworkBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;
    private NetworkStartPosition[] startPositions;
    //private ScreenFade fadeCamera;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    [SerializeField]
    private GameObject deathEffect;

    void Start()
    {
        if (isLocalPlayer)
        {
            startPositions = FindObjectsOfType<NetworkStartPosition>();

            if(startPositions != null)
            {
                Debug.Log("Found " + startPositions.Length + " spawnpoints");
            }
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        //if (!isServer)
        //    return;

        currentHealth -= _amount;

        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                //spawn death effect
                GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
            
                if (GetComponent<localPlayerController>() != null)
                {
                    SceneManager.LoadScene("HoH-Quarters");
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                currentHealth = maxHealth;
                // called on the Server, invoked on the Clients
                //StartCoroutine("Respawning");
                //CmdRespawn();
                RpcRespawn();
            }

            //fadeCamera.GetComponentInChildren<ScreenFade>().Fade(FadeType.Out);
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    //IEnumerator Respawning()
    //{
    //    yield return new WaitForSeconds(3);
    //    RpcRespawn();
    //}

    //Rpc = make sure method is called an all clients
    //[Command]
    //void CmdRespawn()
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Respawn");
            //Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            //If there is a spawn point array and the array is not empty, pick one at random
            if (startPositions != null && startPositions.Length > 0)
            {
                spawnPoint = startPositions[Random.Range(0, startPositions.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}
