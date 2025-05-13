using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public GameObject player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Tekan K buat simulate tembak
        {
            player.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
