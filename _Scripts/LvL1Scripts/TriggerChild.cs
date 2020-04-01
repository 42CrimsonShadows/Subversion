using UnityEngine;

public class TriggerChild : MonoBehaviour {

    [SerializeField]
    private GameObject trigger;

    private AIEvent aiScript;

	// Use this for initialization
	void Start () {
        aiScript = GetComponentInParent<AIEvent>();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("First Event Trigger in Child Trigger");
        aiScript.ObectTriggered(trigger);
    }
}
