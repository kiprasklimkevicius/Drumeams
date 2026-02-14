using Unity.XR.CoreUtils;
using UnityEngine;

public class PhysicsHand : MonoBehaviour
{

    public float fastest_hand = 10.0f; // TODO: balance this value

    public Transform trackedTransform = null;
    public Rigidbody body = null;
    public IObserver audioManager = null;
    public IObserver particleManager = null;

    public WebSocketManager webSockets = null;

    public float positionStrength = 25.0f;

    public float positionThreshold = 0.005f;
     public float maxDistance = 1f;
     public float rotationStrength = 30;
     public float rotationThreshold = 10f;

    void FixedUpdate()
     {
          var distance = Vector3.Distance(trackedTransform.position, body.position);
          if (distance > maxDistance || distance < positionThreshold)
          {
               body.MovePosition(trackedTransform.position);
          }
          else
          {
               var vel = (trackedTransform.position - body.position).normalized * positionStrength * distance;
               body.linearVelocity = vel;
          }

          float angleDistance = Quaternion.Angle(body.rotation, trackedTransform.rotation);
          if (angleDistance < rotationThreshold)
          {
               body.MoveRotation(trackedTransform.rotation);
          }
          else
          {
               float kp = (6f * rotationStrength) * (6f * rotationStrength) * 0.25f;
               float kd = 4.5f * rotationStrength;
               Vector3 x;
               float xMag;
               Quaternion q = trackedTransform.rotation * Quaternion.Inverse(transform.rotation);
               q.ToAngleAxis(out xMag, out x);
               x.Normalize();
               x *= Mathf.Deg2Rad;
               Vector3 pidv = kp * x * xMag - kd * body.angularVelocity;
               Quaternion rotInertia2World = body.inertiaTensorRotation * transform.rotation;
               pidv = Quaternion.Inverse(rotInertia2World) * pidv;
               pidv.Scale(body.inertiaTensor);
               pidv = rotInertia2World * pidv;
               body.AddTorque(pidv);
          }
     }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        trackedTransform = transform;
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        particleManager = GameObject.Find("ParticleSystemManager").GetComponent<ParticleSystemManager>();
        Debug.Log(audioManager);
        webSockets = GameObject.Find("WebSocketManager").GetComponent<WebSocketManager>();
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
            audioManager.OnDrumHit(body.linearVelocity.normalized, contactPoint);
            particleManager.OnDrumHit(body.linearVelocity.normalized, contactPoint);
            webSockets.DrumHit(255);

            Debug.Log("Collided with drum");
        }
    }

}
