using UnityEngine;

public class DrumColliders : MonoBehaviour
{
    public IObserver AudioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hand") {
            AudioManager.OnDrumHit(Vector3.down);
         }
    }
}
