using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : BaseBullet
{
    #region Main

    protected override void ApplyDebuff(EnemyBehavior enemy)
    {
        enemy.StartDebuffDamage(_tower.m_damageModifier, _tower.m_modifierTime);
    }

    #endregion
}