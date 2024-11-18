using UnityEngine;
using UnityEngine.SceneManagement; // for 轉場

public class StartUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // 呼叫 Game 場景
        SceneManager.LoadScene(1); // Game場景編號為 1
    }
}
