using UnityEngine;

public class ActivatableAbilityComponentWithCooldown : ActivatableAbilityComponent
{
    public float cooldown;
    private float _lastActivationTime;

    public ActivatableAbilityComponentWithCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    protected override bool CanActivate()
    {
        return base.CanActivate() && timeRemaining() <= 0.0f;
    }

    protected override void OnActivate()
    {
        base.OnActivate();
        _lastActivationTime = Time.time;
    }

    public float timeRemaining()
    {
        return Time.time - _lastActivationTime;
    }
}
