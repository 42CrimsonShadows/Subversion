using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class localPlayerController : NetworkBehaviour {

	[Header("VR Camera Stuff")]
	public GameObject ovrCamRig;
	public Animator animator;
	public Transform leftHand;
	public Transform rightHand;
	public Camera leftEye;
	public Camera rightEye;

	//protected with "private" but will show up in the inspector
	[Header("Ground Check Stuff")]
	[SerializeField]
	private float JumpHeight = 20f;
	[SerializeField]
	private bool airBorn;
	[SerializeField]
	private float GroundDistance = 1f;
	[SerializeField]
	private LayerMask Ground;
	[SerializeField]
	private bool _isGrounded = true;
	[SerializeField]
	private Transform _groundChecker;

	[Header("Speed Attributes")]
	[SerializeField]
	private float speed = 20;
	[SerializeField]
	private float BoostSpeed = 50;

    //used to trigger speedboost
    public bool speedBoosted = false;
    //the current speed of the e-frame
    public float currentSpeed;
    //wait time on the speedBoost
    private bool speedBoostCooldown = false;

    private Rigidbody _body;
	private Vector3 _inputs = Vector3.zero;
	Vector3 pos;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

    private GameObject engDownBtn;
    private GameObject engUpBtn;
    private GameObject gunDownBtn;
    private GameObject gunUpBtn;
    private GameObject shdDownBtn;
    private GameObject shdUpBtn;
    private GameObject mslDownBTn;
    private GameObject mslUpBtn;

    // Use this for initialization
    void Start ()
	{
		_body = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();

		_groundChecker = transform.GetChild(0);

		pos = transform.position;

		//RegisterPlayerName();

        //get refs to buttons
        engDownBtn = GameObject.Find("ENGdown");
        engDownBtn.SetActive(false);
        gunDownBtn = GameObject.Find("GunDown");
        gunDownBtn.SetActive(false);
        shdDownBtn = GameObject.Find("SHDdown");
        shdDownBtn.SetActive(false);
        mslDownBTn = GameObject.Find("MSLdown");
        mslDownBTn.SetActive(false);
        engUpBtn = GameObject.Find("ENGUpButton");
        gunUpBtn = GameObject.Find("GunUpButton");
        shdUpBtn = GameObject.Find("SHDUpButton");
        mslUpBtn = GameObject.Find("MSLUpButton");
    }
	
	// Update is called once per frame
	void Update ()
	{
		//if (!isLocalPlayer)
		//{
		//	Destroy(ovrCamRig);
		//	AssignRemoteLayer();
		//}
		//else
		//{
			//keep camera good when another player joins
			//if (leftEye.tag != "MainCamera")
			//{
			//	leftEye.tag = "MainCamera";
			//	leftEye.enabled = true;
			//}
			//if (rightEye.tag != "MainCamera")
			//{
			//	rightEye.tag = "MainCamera";
			//	rightEye.enabled = true;
			//}

			//keep hand positions good when another player joins
			leftHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
			rightHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
			leftHand.localPosition = InputTracking.GetLocalPosition(Node.LeftHand);
			rightHand.localPosition = InputTracking.GetLocalPosition(Node.RightHand);

			//handle position and rotation of the player
			Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

			////(triggers walking sound and animation)
			if (primaryAxis.y != 0f || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q))
			{
				animator.SetBool("isWalking", true);
			}
			else
			{
				animator.SetBool("isWalking", false);
			}      

			//update pos every frame (seems to slow PC a bit)
			pos = transform.position;


			_isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

			if (_isGrounded)
			{
				if (airBorn)
				{
					SoundManager.Instance.PlayOneShot(SoundManager.Instance.landingBoom);
				}

				airBorn = false;
				Debug.Log("Currently Grounded");
			}
			if(!_isGrounded)
			{
				airBorn = true;
				Debug.Log("The Mech is NOT Currently Grounded!!!");
				pos += (Physics.gravity * 0.7f);
			}


			if (Input.GetButtonDown("Jump") && _isGrounded)
			{
				airBorn = true;
				//_body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * Physics.gravity.y), ForceMode.VelocityChange);
			}

            //if speedboost triggered...
            if (speedBoosted == true)
            {
                //go fast
                currentSpeed = BoostSpeed;
            }
            else
            {
                //go regular speed
                currentSpeed = speed;
            }

			if (primaryAxis.y > 0f || Input.GetKey(KeyCode.W))
			{
				//animator.SetBool("isWalking", true);

				if (primaryAxis.y > 0f)
				{
					pos += (primaryAxis.y * transform.forward * Time.deltaTime * currentSpeed);
				}
				if (Input.GetKey(KeyCode.W))
				{
					pos += (transform.forward * Time.deltaTime * currentSpeed);
				}
			}
			else if (primaryAxis.y < 0f || Input.GetKey(KeyCode.S))
			{
				animator.SetBool("isWalkBack", true);

				if (primaryAxis.y < 0f)
				{
					pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime * currentSpeed);
				}
				if (Input.GetKey(KeyCode.S))
				{
					pos += (-transform.forward * Time.deltaTime * currentSpeed);
				}
			}
			else
			{
				animator.SetBool("isWalkBack", false);
			}

			if (primaryAxis.x > 0f || Input.GetKey(KeyCode.D))
			{
				//animator.SetBool("isWalking", true);

				if (primaryAxis.x > 0f)
				{
					pos += (Mathf.Abs(primaryAxis.x) * transform.right * Time.deltaTime * currentSpeed);
				}
				if (Input.GetKey(KeyCode.D))
				{
					pos += (transform.right * Time.deltaTime * currentSpeed);
				}
			}
			else if (primaryAxis.x < 0f || Input.GetKey(KeyCode.A))
			{
				//animator.SetBool("isWalking", true);

				if (primaryAxis.x < 0f)
				{
					pos += (Mathf.Abs(primaryAxis.x) * -transform.right * Time.deltaTime * currentSpeed);
				}
				if (Input.GetKey(KeyCode.A))
				{
					pos += (-transform.right * Time.deltaTime * currentSpeed);
				}
			}

			transform.position = pos;

			Vector3 euler = transform.rotation.eulerAngles;
			Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

			if (Input.GetKey(KeyCode.E))
			{
				euler.y += .5f;
			}

			if (Input.GetKey(KeyCode.Q))
			{
				euler.y -= .5f;
			}

			euler.y += secondaryAxis.x;
			transform.rotation = Quaternion.Euler(euler);

			//maybe set local rotation too?
			//transform.localRotation = Quaternion.Euler(euler);

			//suicide kill test
			if (Input.GetKeyDown(KeyCode.K))
			{
				var healthScript = GetComponent<Health>();
				if (healthScript != null)
				{
					Debug.Log("Self Destruct Initiated");
					Debug.Log("Current Health is now " + healthScript.currentHealth);
					healthScript.RpcTakeDamage(100);
				}
			}
		//}
	}

	private void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void RegisterPlayerName()
	{
		//get the player ID from the network and assign the name to the tranform
		string _ID = "Player" + GetComponent<NetworkIdentity>().netId;
		transform.name = _ID;
	}

    public void SpeedBoost()
    {
        if(speedBoostCooldown != true)
        {
            StartCoroutine("TenSecSpeedBoost");
        }
    }
        
    IEnumerator TenSecSpeedBoost()
    {
        engUpBtn.SetActive(false);
        engDownBtn.SetActive(true);
        speedBoosted = true;

        yield return new WaitForSeconds(10);
        speedBoosted = false;
        engUpBtn.SetActive(true);
        engDownBtn.SetActive(false);
        speedBoostCooldown = true;
    }
}
