using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            player.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
