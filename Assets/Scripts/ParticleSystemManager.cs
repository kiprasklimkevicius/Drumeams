using UnityEngine;
using UnityEngine.VFX;

public class ParticleSystemManager : MonoBehaviour, IObserver
{
    public GameObject drumParticles;
    public Gradient gradient;
    public VisualEffect drumVFX = null;
    public ParticleSystem blowParticles;
    public ParticleSystem ps;
    public bool blowPlaying = false;
    float maxBlowVelocity = 5.0f;

    

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
        //TODO: Start coroutine to destroy particle (cause this is vfx need to do en manual)
    }

    public void OnBlowDetect(float blowForce)
    {
        if (!blowPlaying)
        {
            blowParticles.Play();
            blowPlaying = true;
        }
        var emission = blowParticles.emission;
        emission.rateOverTime = blowForce/2;
        
    }
    public void OnBlowFinished()
    {
        blowParticles.Stop();
        blowPlaying = false;
    }

    void SetGradientAlphas(Vector3 velocity)
    {
        // Blend alpha from opaque at 0% to transparent at 100%
        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(velocity.magnitude, 0.0f);
        alphas[1] = new GradientAlphaKey(0.0f, 1.0f);
        gradient.SetAlphaKeys(alphas);
    }
}
