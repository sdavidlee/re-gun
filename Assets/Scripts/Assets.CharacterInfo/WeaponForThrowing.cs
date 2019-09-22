using Assets.EnemyAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.CharacterInfo.Character;

public class WeaponForThrowing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var enemyHit = other.transform.GetComponent<Enemy>();
        if (enemyHit)
            enemyHit.EnemyActions.TakeHit(ThePlayer.Stats, AttackTypes.ThrowWeapon);
    }
}
