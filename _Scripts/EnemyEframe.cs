using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyEframe : NetworkBehaviour {

	private NavMeshAgent agent;
    public Animator pveAnimator;
    public PVEGun pveGunSCript;

    [SerializeField]
    private Transform currentTarget;

    //the player gameobject and it's location
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject friendlyTurrent;

    private Vector3 distanceToPlayer;
    private Vector3 distaneToFriendlyTurret;

	//time in milliseconds when alien should update path
	public float navigationUpdate;
	//how much time since last update
	private float navigationTime = 0;

    public float visualRange = 300f;   
    public float chaseRange = 200f; 
    public float attackRange = 100f;


    void Start ()
	{
        //gets a ref to the NavmeshAgent so you can acces it
        agent = GetComponent<NavMeshAgent>();
        pveAnimator = GetComponent<Animator>();
        pveGunSCript = GetComponent<PVEGun>();

        if (agent == null)
        {
            Debug.LogError("The nav mesh component is not attached to " + gameObject.name);
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (friendlyTurrent == null)
        {
            friendlyTurrent = GameObject.FindWithTag("FriendlyTurrent");
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null || friendlyTurrent != null)
        {
            float playerDistance = Vector3.Distance(player.transform.position, transform.position);
            float turretDistance = Vector3.Distance(friendlyTurrent.transform.position, transform.position);

            if (turretDistance < playerDistance)
            {
                //set TURRET as target
                currentTarget = friendlyTurrent.transform;

                if (turretDistance < visualRange)
                {
                    Vector3 targetRotation = new Vector3(friendlyTurrent.transform.position.x, transform.position.y, friendlyTurrent.transform.position.z);
                    transform.LookAt(targetRotation);

                    Debug.Log("I see " + currentTarget);

                    if (turretDistance < chaseRange)
                    {
                        //agent.isStopped = false;

                        Debug.Log("I'm chasing " + currentTarget);
                        EframeWalk();

                        if (turretDistance < attackRange)
                        {
                            Debug.Log("I'm attacking " + currentTarget);
                            //EframeAttack();

                            if ((Time.time - pveGunSCript.pveLastMachineGunFireTime) > pveGunSCript.pveMachineGunFireRate)
                            {
                                pveGunSCript.pveLastMachineGunFireTime = Time.time;
                                pveGunSCript.PVEFireMachineGun();
                            }
                        }
                        else
                        {
                            pveGunSCript.PVEStopFire();
                        }
                    }
                }
            }
            if (turretDistance > playerDistance)
            {
                //set PLAYER as taget
                currentTarget = player.transform;

                if (playerDistance < visualRange)
                {

                    Vector3 targetRotation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    transform.LookAt(targetRotation);

                    Debug.Log("I see " + currentTarget);

                    if (playerDistance < chaseRange)
                    {
                        //agent.isStopped = false;

                        Debug.Log("I'm chasing " + currentTarget);
                        EframeWalk();

                        if (playerDistance < attackRange)
                        {
                            Debug.Log("I'm attacking " + currentTarget);
                            //EframeAttack();
                            if ((Time.time - pveGunSCript.pveLastMachineGunFireTime) > pveGunSCript.pveMachineGunFireRate)
                            {
                                pveGunSCript.pveLastMachineGunFireTime = Time.time;
                                pveGunSCript.PVEFireMachineGun();
                            }

                            EframeStopWalk();
                        }
                        else
                        {
                            //EframeStopAttack();
                            pveGunSCript.PVEStopFire();
                        }
                    }
                    else
                    {
                        EframeStopWalk();
                    }
                }
            }
            if (playerDistance > chaseRange && turretDistance > chaseRange)
            {
                currentTarget = this.transform;
                //agent.isStopped = true;
            }
        }

        navigationTime += Time.deltaTime;

        if (navigationTime > navigationUpdate)
        {

            //assign the target position to the agent destination
            agent.SetDestination(currentTarget.transform.position);

            //set the nav time back to zero
            navigationTime = 0.5f;
        }
    }

    public void EframeAttack()
    {
        //pveAnimator.SetBool("Firing", true);         
    }

    public void EframeStopAttack()
    {
        //pveAnimator.SetBool("Firing", false);
    }

    public void EframeWalk()
    {
        pveAnimator.SetBool("isWalking", true);

    }
    public void EframeStopWalk()
    {
        pveAnimator.SetBool("isWalking", false);
    }
}
