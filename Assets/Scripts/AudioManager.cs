using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    public AudioClip drumSound;
    public AudioSource audioSource;
    public AudioClip blowSound;
    public float loudestDrumSound = 10.0f;
    bool blowPlaying = false;
    float maxBlowVelocity = 100.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint)
    {
        audioSource.PlayOneShot(drumSound, velocity.magnitude);
        Debug.Log("Velocity magnitude: " + velocity.magnitude);
        Debug.Log("played sound");
    }

    public void OnBlowDetect(float blowForce)
    {
        if (!blowPlaying)
        {
            blowPlaying = true;
            audioSource.Play(0);
        }
        audioSource.volume = blowForce / maxBlowVelocity;
    }
    public void OnBlowFinished()
    {
        audioSource.Stop();
        blowPlaying = false;
    }
}
