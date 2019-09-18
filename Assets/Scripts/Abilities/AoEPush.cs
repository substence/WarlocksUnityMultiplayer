using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEPush : AbilityBehaviour
{
    private const string ButtonName = "Fire1";
    private float radius = 1.0f;
    private float force = 1000.0f;
    private float cooldown = 5.0f;
    private bool showDebug = false;

    private void Start()
    {
        ability = new Ability();
        ability.AddComponent(new ActivatableAbilityComponentWithCooldown(cooldown));
        ability.AddComponent(new ButtonActivation(ButtonName));
    }

    private void Update()
    {
        if (Input.GetButtonUp(ButtonName) && isLocalPlayer)
        {
            Activate();
        }        
    }

    void Activate()
    {
        Explode();
    }

    void Explode()
    {
        Vector3 epicenter = transform.position;
        Collider[] collisions = Physics.OverlapSphere(epicenter, radius);
        foreach (Collider collider in collisions)
        {
            if (collider.gameObject != gameObject)
            {
                Rigidbody rigidBody = collider.GetComponent<Rigidbody>();
                if (rigidBody != null)
                {
                    rigidBody.AddExplosionForce(force, epicenter, radius);
                    if (showDebug)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawWireSphere(epicenter, radius);
                    }
                }
            }
        }
    }
}