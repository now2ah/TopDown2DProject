using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int DOOR_NUMBER = 0;

    public static void ChangeScene(string sceneName, int doorNumber)
    {
        DOOR_NUMBER = doorNumber;
        Debug.Log(sceneName + " / " + doorNumber);
        //SceneManager.LoadScene(sceneName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");

        foreach (var enter in enters)
        {
            GameObject door = enter;

            if (door.TryGetComponent<Exit>(out Exit exit))
            {
                if (DOOR_NUMBER == exit.doorNumber)
                {
                    float x = door.transform.position.x;
                    float y = door.transform.position.y;

                    switch(exit.direction)
                    {
                        case eExitDirection.RIGHT: x += 1; break;
                        case eExitDirection.UP: y += 1; break;
                        case eExitDirection.LEFT: y += 1; break;
                        case eExitDirection.DOWN: y += 1; break;
                    }
                }
            }
            
        }
    }
}
