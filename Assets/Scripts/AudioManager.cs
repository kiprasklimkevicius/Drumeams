using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    public AudioClip drumSound;
    public AudioSource AudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrumHit(Vector3 velocity)
    {
        AudioSource.PlayOneShot(drumSound, velocity.normalized.magnitude);
        Debug.Log("played sound");
    }
}
