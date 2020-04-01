using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DefendEvent : NetworkBehaviour
{
    [SerializeField]
    private GameObject player;

    public GameObject eSpawner1;
    public GameObject eSpawner2;
    public GameObject eSpawner3;
    public GameObject eSpawner4;

    // Update is called once per frame
    void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Starting Event and waiting 3 seconds");
            StartCoroutine("StartEvent");         
    }

    private void OnTriggerStay(Collider other)
    {
        ///continue to countdown the data retreival clock
    }

    private void OnTriggerExit(Collider other)
    {
        ///reset the data retreival clock
    }

    private void BeginDefendEvent()
    {
        BeginSpawningEnemies();
    }

    private void BeginSpawningEnemies()
    {
        Debug.Log("turning on and spawning enemies");

        eSpawner1.SetActive(true);
        eSpawner1.GetComponent<EnemySpawner>().OnStartServer();
        eSpawner2.SetActive(true);
        eSpawner2.GetComponent<EnemySpawner>().OnStartServer();
        eSpawner3.SetActive(true);
        eSpawner3.GetComponent<EnemySpawner>().OnStartServer();
        eSpawner4.SetActive(true);
        eSpawner4.GetComponent<EnemySpawner>().OnStartServer();
    }   

    IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(3f);
        BeginDefendEvent();
    }
}
