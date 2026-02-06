using UnityEngine;

public class PhysicsHand : MonoBehaviour
{
    public Transform trackedTransform = null;
    public Rigidbody rb = null;
    public IObserver audioManager = null;

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
            audioManager.OnDrumHit(rb.linearVelocity);
            Debug.Log("Collided with drum");
        }
    }

}
