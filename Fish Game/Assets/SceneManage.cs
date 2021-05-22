using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStartButtonPress()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void onGameEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }
}
