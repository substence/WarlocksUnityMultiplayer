using System;

public class Ability : ITickable
{
    IAbilityComponent[] components;

    public void AddComponent(IAbilityComponent component)
    {
        component.SetAbility(this);
    }

    public IAbilityComponent GetComponent(Type componentType)
    {
        foreach (var component in components)
        {   
            if (componentType.IsInstanceOfType(component))
            {
                return component;
            }
        }
        return null;
    }

    public void Tick()
    {
        foreach (var component in components)
        {
            if (component is ITickable)
            {
                (component as ITickable).Tick();
            }
        }
    }
}

