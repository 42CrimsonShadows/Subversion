using UnityEngine;

public class BulletHits : MonoBehaviour {

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] audioClipArray;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
