using Script.Feature.Bullet;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerShooting : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BulletPool bulletPool;
    [ReadOnly] bool canFire;
    [ReadOnly] float timer;
    [ReadOnly] float shootInterval;
    PlayerMovement _playerMovement;

    // Update is called once per frame
    void Start() {
        inputReader.ShootingEvent += ShootEvent;
    }
    void Update() {
        if(!canFire) {
            timer += Time.deltaTime;
            if(timer > shootInterval) {
                canFire = true;
                timer = 0;
            }
        }
    }

    void ShootEvent() {
        if (canFire) {
            bulletPool.BulletRequest(transform.position, _playerMovement.dir);
        }
    }

    void OnValidate() {
        _playerMovement = GetComponent<PlayerMovement>();
    }
    void OnDisable() {
        inputReader.ShootingEvent += ShootEvent;
    }
}

