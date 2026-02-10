using UnityEngine;
using UnityEngine.VFX;

public class ParticleSystemManager : MonoBehaviour, IObserver
{
    public GameObject drumParticles;
    public Gradient gradient;
    public VisualEffect drumVFX = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drumVFX = drumParticles.GetComponent<VisualEffect>();
        gradient = new Gradient();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint)
    {
        SetGradientAlphas(velocity);
        drumVFX.SetGradient("ColorOverLifeAlpha", gradient); //TODO: Testing

        Instantiate(drumParticles, contactPoint, drumParticles.transform.rotation);
    }

    void SetGradientAlphas(Vector3 velocity)
    {
        // Blend alpha from opaque at 0% to transparent at 100%
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(velocity.magnitude/10, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);
        gradient.SetAlphaKeys(alphas);
    }
}
