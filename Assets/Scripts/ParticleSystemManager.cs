using UnityEngine;

public class ParticleSystemManager : MonoBehaviour, IObserver
{
    public GameObject drumParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint)
    {
        Instantiate(drumParticles, contactPoint, drumParticles.transform.rotation);
    }
}
