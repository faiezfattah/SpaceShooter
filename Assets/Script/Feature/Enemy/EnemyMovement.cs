using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
     [SerializeField]
    private GameObject _enemyPrefab;
     [SerializeField]
    private float _minimumMoveTime;
     [SerializeField]
    private float _maximumMoveTime;
    private float _timeUntilMove;
    void Awake()
    {
        SetTimeUntilMove();
    }
    void Update()
    {
           if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up  * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down  * Time.deltaTime;
        } 
    }

    private void SetTimeUntilMove()
    {
        _timeUntilMove = Random.Range(_minimumMoveTime, _maximumMoveTime);
    }
}
