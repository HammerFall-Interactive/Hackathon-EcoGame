using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMainSceneLoader : MonoBehaviour
{
    public bool shouldReturnToMain = false;
    public float timeToWait = 5f;
    private float timer = 0;

    void Update()
    {
        if (shouldReturnToMain)
        {
            if (timer > timeToWait)
            {
                SceneManager.LoadScene("MainScene");
                return;
            }
            timer += Time.deltaTime;
            return;
        }
    }
}
