using UnityEngine;

public class ButtonActivation : AbilityComponent, ITickable
{
    public string button;
    private ActivatableAbilityComponent activatableComponent;

    public ButtonActivation(string button)
    {
        this.button = button;
    }

    public ButtonActivation(Ability ability, string button) : base(ability)
    {
        this.button = button;
    }

    // Start is called before the first frame update
    public void Tick()
    {
        if (activatableComponent != null)
        {
            if (Input.GetButtonUp(button))
            {
                activatableComponent.Activate();
            }
        }
        else
        {
            if (ability != null)
            {
                //activatableComponent = ability.GetComponent(ActivatableAbilityComponent);
            }
        }
    }
}
public interface ITickable
{
    void Tick();
}
