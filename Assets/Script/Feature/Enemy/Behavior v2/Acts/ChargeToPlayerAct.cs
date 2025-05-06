using UnityEngine;

public class ChargeToPlayerAct : EnemyBehavior {
    float _chargingSpeed;
    public ChargeToPlayerAct(float chargingSpeed) {
        _chargingSpeed = chargingSpeed;
    }
    public override void OnUpdate(BehaviorActor actor) {
        Vector3 directionToPlayer = (actor.Player.position - actor.transform.position).normalized;

        if (directionToPlayer.sqrMagnitude > 0) {
            actor.transform.position += _chargingSpeed * Time.deltaTime * directionToPlayer;
        }

        float distanceToPlayer = Vector3.Distance(actor.transform.position, actor.Player.position);
        float stoppingDistance = 0.1f; 

        if (distanceToPlayer < stoppingDistance) {
            Debug.Log($"{actor.gameObject.name} boinked the player ");
        }
    }
}
