using UnityEngine;
using UnityEngine.SceneManagement; // for ���

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
        // �I�s Game ����
        SceneManager.LoadScene(1); // Game�����s���� 1
    }
}
