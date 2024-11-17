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

        // �C������t�׹w�]1�A0.1f ����C10��
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

        //transform.Translate(direction);��������
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
        // �z�L Tag ��� Unity ���������C������A���I��h������
        if (collision.CompareTag("food"))
        {
            //Instantiate(bodyPrefab);

            // �[���C�����󪺦P�ɡA���K�]�w��@�}�l����m�b���⨭�W�M���ਤ�סA�ѨM�@�}�l�b�C���@�ɥ�������bug
            bodies.Add(Instantiate(bodyPrefab
                , transform.position //�����e��m
                , Quaternion.identity)); //�L���ਤ��(t,Q�����P�ɨϥ�)

            gameUI.AddScore();

            gameAudio.PlayEatSound();
        }

        //�Y����h�C�������A�D�^�����I
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
