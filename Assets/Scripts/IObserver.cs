using UnityEngine;

public interface IObserver
{

    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint);
    public void OnBlowDetect(float blowForce);
    public void OnBlowFinished();
}
