using UnityEngine;

public class holdableObj : MonoBehaviour
{
    public float breakSpeed = 6f;
    public ParticleSystem breakEffect;

    // bu flag sayesinde bir kez kırılmış olur
    private bool hasBroken = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBroken) return;  

        if (collision.relativeVelocity.magnitude >= breakSpeed)
        {
            hasBroken = true;  // bir daha işlem yapma

            if (breakEffect != null)
            {
                ContactPoint contact = collision.contacts[0];
                var effectInstance = Instantiate(breakEffect, contact.point, Quaternion.LookRotation(contact.normal));
                var main = effectInstance.main;
                Destroy(effectInstance.gameObject, main.duration + main.startLifetime.constantMax);
            }

            angryBar.brokenCount++;
            Destroy(gameObject);
        }
    }
}