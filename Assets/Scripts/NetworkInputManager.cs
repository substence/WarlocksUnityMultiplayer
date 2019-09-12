using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class NetworkInputManager : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var inputManager = GetComponent<ThirdPersonUserControl>();
        if (inputManager)
        {
            inputManager.enabled = true;
        }
    }
}
