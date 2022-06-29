using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : MonoBehaviour
{
    #region Exposed

    [Header("Tower Values")]
    [SerializeField]
    private float _maxHP;
    [Space(5)]

    [Header("Building Values")]
    [SerializeField]
    private float _buildingTime;
    [SerializeField]
    private float _upgradeTime;
    [SerializeField]
    private int _woodBuildingCost;
    [SerializeField]
    private int _stoneBuildingCost;    
    [Space(5)]

    [Header("Attack Values")]
    [SerializeField]
    private float _fireRange;
    [SerializeField]
    private float _FireRate;
    [SerializeField]
    private float _damageModifier;
    [SerializeField]
    private float _modifierTime;

    #endregion


    #region Unity API

    private void Awake()
    {
        InitAttackRange();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBehavior>()) TargetEnemy(other.gameObject.GetComponent<EnemyBehavior>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBehavior>()) LeaveTarget(other.gameObject.GetComponent<EnemyBehavior>());
    }
    #endregion


    #region Main

    private void AttackEnemy()
    {
        if (_enemiesInAttackRange.Count > 0)
        {
            _enemiesInAttackRange[0]
        }
    }
    private void TargetEnemy(EnemyBehavior enemy)
    {
        _enemiesInAttackRange.Add(enemy);
    }
    private void LeaveTarget(EnemyBehavior enemy)
    {
        _enemiesInAttackRange.Remove(enemy);
    }

    private void InitAttackRange()
    {
        _aggroSphere = this.gameObject.AddComponent<SphereCollider>();
        _aggroSphere.isTrigger = true;
        _aggroSphere.radius = _fireRange;
        _aggroSphere.center = Vector3.zero;
    }

    #endregion


    #region Private

    private float _currentHP;
    private SphereCollider _aggroSphere;
    private List<EnemyBehavior> _enemiesInAttackRange = new List<EnemyBehavior>();

    #endregion
}