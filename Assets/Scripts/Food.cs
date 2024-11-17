using UnityEngine;

public class Food : MonoBehaviour
{
    // 拉 food area 進 foodArea
    public Collider2D foodArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collider is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);

        //Debug.Log(foodArea.bounds.min.x);
        //Debug.Log(foodArea.bounds.max.x);
        //Debug.Log(foodArea.bounds.min.y);
        //Debug.Log(foodArea.bounds.max.y);

        RandomPosition();
    }

    // 蘋果隨機位置
    void RandomPosition()
    {
        transform.position = new Vector3(
            Random.Range(foodArea.bounds.min.x, foodArea.bounds.max.x),
            Random.Range(foodArea.bounds.min.y, foodArea.bounds.max.y),
            0);
    }
}
