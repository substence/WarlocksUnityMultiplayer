using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length >= 1)
            {
                bool areAllPlayersDead = true;
                foreach (var player in players)
                {
                    if (!IsPlayerDead(player))
                    {
                        areAllPlayersDead = false;
                        break;
                    }
                }
                if (areAllPlayersDead)
                {
                    RestartGame();
                }
            }
        }
    }

    private void RestartGame()
    {
        NetworkManager nm = GameManager.FindObjectOfType<NetworkManager>();
        nm.ServerChangeScene("Map");
        //SceneManager.LoadScene("Menu");
    }

    private bool IsPlayerDead(GameObject player)
    {
        if (player.transform.position.y < -1.0f)
        {
            return true;
        }
        return false;
    }
}
