using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileDrive : NetworkBehaviour {

	public float speed = 30f;
	public int damage = 20;

    [SerializeField]
    private GameObject explosion;

	// Use this for initialization
	void Start () {
		//Coroutines are threads that happen at certain intervals, and run at designated times
        //when the Missile gets instantiated call the deathTimer() coroutine method
		StartCoroutine("deathTimer");
	}
	
	// Update is called once per frame
	void Update () {
		//move the missile forward at speed times the time between frames
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	IEnumerator deathTimer()
	{
        //if the missile didn't hit the player after 10 seconds it should selfdestruct
		yield return new WaitForSeconds(10);
		Destroy(gameObject);
	}

    void OnCollisionEnter(Collision collider)
    {
        //if the colliding gameobject has the EnemyEframe script....
        if(collider.gameObject.tag == "EnemyE-Frame")
        {
            //call the RpcTakeDamage method on the Health script and pass it the damage amount of the missile
            collider.gameObject.GetComponent<Health>().RpcTakeDamage(damage);
            Debug.Log("Missile HIT!!!!!");
        }

        //TO DO:
        //instantiate missile explosion and sound

        ExplodeMissile();

        //destroy the missile
        Destroy(gameObject);
    }

    void ExplodeMissile()
    {
        //instantiate the prefab
        GameObject missileExplosion = Instantiate(explosion);
        //give it a position and rotation that equals the last missile position;
        missileExplosion.transform.position = transform.position;
        //missile.transform.rotation = missleSpawnPoint1.transform.rotation;


    }

}
