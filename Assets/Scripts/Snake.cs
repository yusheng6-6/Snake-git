using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameUI gameUI;
    public GameAudio gameAudio;

    Vector3 direction;
    public float speed;

    public Transform bodyPrefab;

    public List<Transform> bodies = new List<Transform>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(transform.position);

        // 遊戲執行速度預設1，0.1f 為放慢10倍
        Time.timeScale = speed;

        RestStage();
    }

    // Update is called once per frame
    void Update()
    {
        // W S A D
        if(Input.GetKeyDown(KeyCode.W) && direction != Vector3.down)
        {
            //Debug.Log("W");
            //transform.Translate(Vector3.up);

            direction = Vector3.up; //0,1,0
        }
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.up)
        {
           // Debug.Log("S");
            //transform.Translate(Vector3.down);

            direction = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.right)
        {
            //Debug.Log("A");
            //transform.Translate(Vector3.left);

            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.left)
        {
            //Debug.Log("D");
            //transform.Translate(Vector3.right);

            direction = Vector3.right;
        }

        //transform.Translate(direction);順順移動
    }

    private void FixedUpdate()
    {
        for (int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }

        transform.Translate(direction);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 透過 Tag 找到 Unity 中對應的遊戲物件，有碰到則長身體
        if (collision.CompareTag("food"))
        {
            //Instantiate(bodyPrefab);

            // 加載遊戲物件的同時，順便設定其一開始的位置在角色身上和旋轉角度，解決一開始在遊戲世界正中央的bug
            bodies.Add(Instantiate(bodyPrefab
                , transform.position //角色當前位置
                , Quaternion.identity)); //無旋轉角度(t,Q必須同時使用)

            gameUI.AddScore();

            gameAudio.PlayEatSound();
        }

        //若撞牆則遊戲結束，蛇回中心點
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("game over");

            RestStage();

            gameAudio.ReplayBackgroundMusic();
        }
    }

    void RestStage()
    {
        transform.position = Vector3.zero;
        direction = Vector3.zero;

        for (int i = 1; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }
        bodies.Clear();
        bodies.Add(transform);

        gameUI.ResetScore();
    }
}
