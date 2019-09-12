public class AbilityComponent : IAbilityComponent
{
    protected Ability _ability;
    public Ability ability
    {
        get { return _ability; }
    }

    public AbilityComponent(Ability ability = null)
    {
        if (ability != null)
        {
            SetAbility(ability);
        }
    }

    public void SetAbility(Ability ability)
    {
        _ability = ability;
        OnAbilitySet();
    }

    virtual protected void OnAbilitySet() { }
}
public interface IAbilityComponent
{
    void SetAbility(Ability ability);
    Ability ability { get; }
}
