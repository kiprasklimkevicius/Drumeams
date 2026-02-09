using UnityEngine;

public interface IObserver
{
    public void OnDrumHit(Vector3 velocity, Vector3 contactPoint);
}
