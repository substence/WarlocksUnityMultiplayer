using UnityEngine.Events;

public class ActivatableAbilityComponent : AbilityComponent
{
    public UnityEvent activated = new UnityEvent();
    private bool _isActive = false;
    public bool isActive()
    {
        return _isActive;
    }

    protected virtual bool CanActivate()
    {
        return true;
    }

    public bool Activate()
    {
        if (!CanActivate())
        {
            return false;
        }
        OnActivate();
        _isActive = true;
        activated.Invoke();
        return true;
    }

    protected virtual void OnActivate()
    {

    }

    public bool Deactivate()
    {
        _isActive = false;
        OnDeactivate();
        return true;
    }

    protected virtual void OnDeactivate()
    {

    }
}