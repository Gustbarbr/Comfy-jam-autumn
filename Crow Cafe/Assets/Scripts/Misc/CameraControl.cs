using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private static CameraControl instance;

    public Transform player;

    void Awake()
    {
        // Impedir duplicação
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (player == null)
        {
            PlayerControl p = FindObjectOfType<PlayerControl>();
            if (p != null)
                player = p.transform;
        }

        if (player != null)
            transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
