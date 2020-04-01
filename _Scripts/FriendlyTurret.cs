using UnityEngine;
using UnityEngine.Networking;

public class FriendlyTurret : NetworkBehaviour
{
    public Transform partToRotate;

    private Vector3 startRotation;

    [SerializeField]
    private PVECanonGun pveTurretCanonScript;

    [SerializeField]
    private Transform currentTarget;

    [SerializeField]
    private Animator turretAnim;

    [SerializeField]
    private string enemyTag = "EnemyE-Frame";

    [SerializeField]
    private float range = 125f;

    [SerializeField]
    private float turnSpeed = 10f;

    void Start()
    {
        pveTurretCanonScript = GetComponent<PVECanonGun>();
        turretAnim = GetComponent<Animator>();

        //check for target 2 times a second
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void UpdateTarget()
    {
        //array of enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //really big distance to start with
        float shortestDistance = Mathf.Infinity;
        //closest enemy found so far
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            currentTarget = nearestEnemy.transform;
        }
        else
        {
            currentTarget = null;
            pveTurretCanonScript.PVETurretStopFire();
        }
    }

    private void LateUpdate()
    {
        if (currentTarget == null)
        {
            if (turretAnim.enabled == false)
            {
                partToRotate.rotation = Quaternion.Euler(0f, 90f, 0f);
                //turn on the animation
                turretAnim.enabled = true;
            }
            return;
        }

        if (currentTarget != null)
        {
            //turn off the animation
            turretAnim.enabled = false;

            //get the direction we need to point
            Vector3 currentTargetDirection = currentTarget.position - partToRotate.position;
            //how to turn in order to get to the direction
            Quaternion targetRotation = Quaternion.LookRotation(currentTargetDirection);
            //actual rotation, smoothed by lerp over a time, times the turn speed, converted to euler angles
            Vector3 actualRotation = Quaternion.Lerp(partToRotate.rotation, targetRotation, Time.deltaTime * turnSpeed).eulerAngles;
            //set the rotating part's rotation to the new angle
            partToRotate.rotation = Quaternion.Euler(0f, actualRotation.y, 0f);


            //if the time interval for fireRate is up again...
            if ((Time.time - pveTurretCanonScript.pveLastTurretMachineGunFireTime) > pveTurretCanonScript.pveTurretMachineGunFireRate)
            {
                //reset fireRate interval
                pveTurretCanonScript.pveLastTurretMachineGunFireTime = Time.time;
                //fire the weapon by calling the PVECanonGun script fire method
                pveTurretCanonScript.PVETurretFireCanon();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
