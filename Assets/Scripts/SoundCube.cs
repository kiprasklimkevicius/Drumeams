using UnityEngine;

public class SoundCube : MonoBehaviour
{   
    public IObserver audioManager = null;
    public IObserver particleManager = null;

    public WebSocketManager webSockets = null;

    public float curr_y_velocity = 0;
    public float prev_y_velocity = 0;

    bool firstBounce = false;

    float bounceHeight = 0;

    private Rigidbody rb = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        particleManager = GameObject.Find("ParticleSystemManager").GetComponent<ParticleSystemManager>();
        Debug.Log(audioManager);
        webSockets = GameObject.Find("WebSocketManager").GetComponent<WebSocketManager>();
    }

    // Update is called once per frame
    void Update()
    {
        prev_y_velocity = curr_y_velocity;
        curr_y_velocity = rb.linearVelocity.y;
        // if (transform.position.y < -1 && rb.linearVelocity.y < 0) Bounce();
        // if (rb.linearVelocity.magnitude > 10)
        // {
        //     // scale linear velocity to be 10
        //     rb.linearVelocity = rb.linearVelocity.normalized * 3;
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("floor"))
        {
            if (!firstBounce) bounceHeight = prev_y_velocity;
            // Reverse Y velocity vector aka bounce
            Bounce();
            Vector3 contactPoint = collision.contacts[0].point;
            audioManager.OnDrumHit(Vector3.down, contactPoint);
            particleManager.OnDrumHit(Vector3.down, contactPoint);
            
            Debug.Log("Collided with floor");
        }
    }

    private void Bounce()
    {
        if(transform.position.y < 0 || rb.linearVelocity.y > 0) return;
        rb.AddForce(Vector3.up * Mathf.Abs(bounceHeight), ForceMode.Impulse);
    }
}
