using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded;

    static float _restartCd;
    void Update()
    {
        _restartCd -= Time.deltaTime;
        Animator.Update();
        if (Input.GetKeyDown(KeyCode.R) && _restartCd < 0f)
            Restart();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B))
            SharedObjects.instance.player.pulseNextFrame = true;
        if (Input.GetKeyDown(KeyCode.Space)) 
            Asteroid.Create();
#endif
    }

    public static void Restart()
    {
        Debug.Log($"restart");
        SceneManager.LoadScene(0);
        Animator.Invoke(SectorMap.Clear).In(0.1f);
        _restartCd = 3f;
    }
}