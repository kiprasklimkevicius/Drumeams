using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    public AudioClip drumSound;
    public AudioSource AudioSource;
    public float loudestDrumSound = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint)
    {
        AudioSource.PlayOneShot(drumSound, velocity.magnitude);
        Debug.Log("Velocity magnitude: " + velocity.magnitude);
        Debug.Log("played sound");
    }
}
