using UnityEngine;

public enum eExitDirection
{
    RIGHT,
    LEFT,
    DOWN,
    UP
}

public class Exit : MonoBehaviour
{
    public string sceneName = "";
    public int doorNumber = 0;
    public eExitDirection direction = eExitDirection.DOWN;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RoomManager.ChangeScene(sceneName, doorNumber);

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
