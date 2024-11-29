using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameUI gameUI;
    public GameAudio gameAudio;

    Vector3 direction;
    public float speed; //基本速度
    private float currentSpeed; //當前速度
    public float acceleration;  //加速度

    public Transform bodyPrefab;
    public List<Transform> bodies = new List<Transform>();

    public Dictionary<KeyCode, Vector3> directionMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log(transform.position);

        // 遊戲執行速度預設1，0.1f 為放慢10倍
        //Time.timeScale = speed;

        RestStage();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    private void FixedUpdate()
    {
        for (int i = bodies.Count - 1; i > 0; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }
    }

    // 蛇前進
    void Movement()
    {
        // W S A D 移動
        foreach (var entry in directionMap)
        {
            KeyCode key = entry.Key;
            Vector3 dir = entry.Value;

            if (Input.GetKeyDown(key) && direction != -dir)
            {
                direction = dir;
                
            }
        }
        transform.Translate(direction * currentSpeed * Time.fixedDeltaTime);

        MovementSpeedUp();
    }

    // 蛇加速
    void MovementSpeedUp()
    {
        if (Input.GetKey(KeyCode.Space))  //按下Space加速
        {
            currentSpeed += acceleration;
            transform.Translate(direction * currentSpeed * Time.fixedDeltaTime);
            //Debug.Log("按下Space: " + currentSpeed);
        }
        else if (Input.GetKeyUp(KeyCode.Space)) //放開Space減速
        {
            currentSpeed = speed;
            //Debug.Log("放開Space: "+currentSpeed);
        }
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

        currentSpeed = speed; //設置初始速度

        //WSAD控制方向
        directionMap = new Dictionary<KeyCode, Vector3>
        {
            { KeyCode.W, Vector3.up },
            { KeyCode.S, Vector3.down},
            { KeyCode.A, Vector3.left},
            { KeyCode.D, Vector3.right}
        };

        for (int i = 1; i < bodies.Count; i++)
        {
            Destroy(bodies[i].gameObject);
        }
        bodies.Clear();
        bodies.Add(transform);

        gameUI.ResetScore();
    }


}
