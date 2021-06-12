using UnityEngine;

public class SharedObjects : MonoBehaviour
{
    public Camera cam;
    public Player player;

    public static SharedObjects instance;

    void Awake()
    {
        instance = this;
    }
}