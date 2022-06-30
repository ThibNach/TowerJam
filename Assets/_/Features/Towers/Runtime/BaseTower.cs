using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{

    #region Exposed 

    [Header("Tower Values")]
    [SerializeField]
    protected float _maxHP;
    [SerializeField]
    protected float _detectionRange;
    [Space(5)]

    [Header("Building Values")]
    [SerializeField]
    protected float _buildingTime;
    [SerializeField]
    protected float _upgradeTime;
    [SerializeField]
    protected int _woodBuildingCost;
    [SerializeField]
    protected int _stoneBuildingCost;
    [Space(5)]

    [Header("Attack Values")]
    public float m_bulletSize;
    public float m_FireRate;
    public float m_damageModifier;
    public float m_modifierTime;
    public float m_bulletSpeed;
    [SerializeField]
    protected BaseBullet _bulletPrefab;

    #endregion


    #region Unity API

    public virtual void Awake()
    {
        InitAttackRange();
    }
    public virtual void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > m_FireRate) LaunchBullet();
        _currentHP = _maxHP;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBehavior>()) TargetEnemy(other.gameObject.GetComponent<EnemyBehavior>());

    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBehavior>()) LeaveTarget(other.gameObject.GetComponent<EnemyBehavior>());
    }

    #endregion

    #region Main

    protected virtual void LaunchBullet()
    {
        if (m_enemiesInAttackRange.Count > 0)
        {
            for (int i = 0; i < m_enemiesInAttackRange.Count; i++)
            {
                if (m_enemiesInAttackRange[i]._damageModifier == 1)
                {
                    BaseBullet bullet = Instantiate(_bulletPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    bullet.SetTarget(m_enemiesInAttackRange[i]);
                    bullet.SetValues(this);
                    _timer = 0;
                    break;
                }
            }
        }
    }
    protected virtual void TargetEnemy(EnemyBehavior enemy) => m_enemiesInAttackRange.Add(enemy);
    protected virtual void LeaveTarget(EnemyBehavior enemy) => m_enemiesInAttackRange.Remove(enemy);
    public virtual void TowerTakeDamage(float damage) => _currentHP -= damage;
    protected virtual void InitAttackRange()
    {
        _aggroSphere = this.gameObject.AddComponent<SphereCollider>();
        _aggroSphere.isTrigger = true;
        _aggroSphere.radius = _detectionRange;
        _aggroSphere.center = Vector3.zero;
    }
    #endregion


    #region Private

    protected float _currentHP;
    protected SphereCollider _aggroSphere;
    [HideInInspector]
    public List<EnemyBehavior> m_enemiesInAttackRange = new List<EnemyBehavior>();
    protected float _timer;

    #endregion

}