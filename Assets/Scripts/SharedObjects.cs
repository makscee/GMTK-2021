using UnityEngine;

public class SharedObjects : MonoBehaviour
{
    public Camera cam;
    public Player player;
    public InputHandler inputHandler;
    public GameManager gameManager;
    public EndScreen endScreen;
    public SoundsPlayer soundsPlayer;

    public static SharedObjects instance;

    void Awake()
    {
        instance = this;
    }
}