using Unity.XR.CoreUtils;
using UnityEngine;

public class PhysicsHand : MonoBehaviour
{

    public float fastest_hand = 10.0f; // TODO: balance this value

    public Transform trackedTransform = null;
    public Rigidbody rb = null;
    public IObserver audioManager = null;
    public IObserver particleManager = null;

    public float positionStrength = 15.0f;

    private void FixedUpdate()
    {
        var vel = (trackedTransform.position - rb.position).normalized
            * positionStrength * Vector3.Distance(trackedTransform.position, rb.position);
        rb.linearVelocity = vel;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trackedTransform = transform;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        particleManager = GameObject.Find("ParticleSystemManager").GetComponent<ParticleSystemManager>();
        Debug.Log(audioManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("drum"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            audioManager.OnDrumHit(rb.linearVelocity.normalized, contactPoint);
            particleManager.OnDrumHit(rb.linearVelocity.normalized, contactPoint);


            Debug.Log("Collided with drum");
        }
    }

}
