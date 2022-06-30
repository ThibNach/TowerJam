using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyBehavior : MonoBehaviour
{
    #region Exposed

    [Header("Enemy Behaviour")]
    [SerializeField]
    private float _MaxHP;
    public float _enemySpeed;
    [Space(5)]

    [Header("Damages")]
    [SerializeField]
    private float _damagesToPlayer;
    [SerializeField]
    private float _damagesToTower;
    [SerializeField]
    private float _damagesToBase;
    [SerializeField]
    private float _attackCoolDown;
    [SerializeField]
    private float _attackRange;
    [Space(5)]

    [Header("Aggro")]
    [SerializeField]
    private float _aggroRadius;

    #endregion


    #region Unity API

    private void Awake()
    {
        InitAggroSphere();
        _damageModifier = 1;
        m_speedAtStart = _enemySpeed;
        _currentHP = _MaxHP;
    }
    private void Update()
    {
        _timerAttackCoolDown += Time.deltaTime;
        if (!_isAttacking)
        {
            if (IsAggro()) MoveToTarget();
            else if (_pathCheckPoints.Count > 0) FollowPath();
            CheckPath();
        }
        EnemyAttack();
        ManageAggroList();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<PlayerValues>() && !other.isTrigger) ||( other.GetComponent<BaseTower>() && !other.isTrigger)) _aggroList.Add(other.gameObject.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.GetComponent<PlayerValues>() && !other.isTrigger) || (other.GetComponent<BaseTower>() && !other.isTrigger)) _aggroList.Remove(other.gameObject.transform);
    }

    #endregion


    #region Main

    private void EnemyAttack()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, _attackRange))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _attackRange, Color.yellow);
            if (_hit.collider.gameObject.GetComponent<BaseTower>() && !_hit.collider.isTrigger)
            {
                _isAttacking = true;
                EnemyMakeDamageToTower(_hit.collider.gameObject.GetComponent<BaseTower>());
            }
            else if (_hit.collider.gameObject.GetComponent<PlayerValues>() && !_hit.collider.isTrigger)
            {
                _isAttacking = true;
                EnemyMakeDamageToPlayer(_hit.collider.gameObject.GetComponent<PlayerValues>());
            }
            else _isAttacking = false;
        }
    }
    private void EnemyMakeDamageToPlayer(PlayerValues target)
    {
        if (_timerAttackCoolDown > _attackCoolDown)
        {
            target.PlayerTakeDamage(_damagesToPlayer);
            _timerAttackCoolDown = 0;
        }
    }
    private void EnemyMakeDamageToTower(BaseTower target)
    {
        if (_timerAttackCoolDown > _attackCoolDown)
        {
            target.TowerTakeDamage(_damagesToTower);
            _timerAttackCoolDown = 0;
        }
    }
    public void EnemyTakeDamage(float damage)
    {
        _currentHP -= damage * _damageModifier;
        if (_currentHP <= 0) EnemyIsDead();
    }
    public void StartDebuffDamage(float modifier, float time)
    {
        StartCoroutine(ModifieDamages(modifier, time));
    }
    private IEnumerator ModifieDamages(float modifier, float time)
    {
        if (_damageModifier == 1)
        {
            _damageModifier *= modifier;
        }
        yield return new WaitForSeconds(time);
        _damageModifier = 1;
    }
    public void StartDebuffSpeed(float modifier, float time)
    {
        StartCoroutine(ModifieSpeed(modifier, time));
    }
    private IEnumerator ModifieSpeed(float modifier, float time)
    {
        if (_enemySpeed == m_speedAtStart)
        {
            _enemySpeed /= modifier;
        }
        yield return new WaitForSeconds(time);
        _enemySpeed = m_speedAtStart;
    }
    private void CheckPath()
    {
        if (_pathCheckPoints.Count > 0)
            if (transform.position == _pathCheckPoints[0].position) _pathCheckPoints.Remove(_pathCheckPoints[0]);
    }
    private void FollowPath()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, _pathCheckPoints[0].position, _enemySpeed * Time.deltaTime);
        this.gameObject.transform.LookAt(_pathCheckPoints[0].position);
    }
    private void MoveToTarget()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, _aggroList[0].position, _enemySpeed * Time.deltaTime);
        this.gameObject.transform.LookAt(_aggroList[0].position);
    }
    private void InitAggroSphere()
    {
        _aggroCollider = this.gameObject.AddComponent<SphereCollider>();
        _aggroCollider.isTrigger = true;
        _aggroCollider.radius = _aggroRadius;
        _aggroCollider.center = Vector3.zero;
    }
    private bool IsAggro() => (_aggroList.Count > 0 && _aggroList[0] != null);
    public void DefinePath(List<Transform> path) => _pathCheckPoints = new List<Transform>(path);

    #endregion


    #region Utils
    private void EnemyIsDead() => Destroy(gameObject);
    private void ManageAggroList()
    {
        if (_aggroList.Count > 0)
        {
            for (int i = 0; i < _aggroList.Count; i++)
            {
                if (_aggroList[i] == null) _aggroList.RemoveAt(i);
            }
        }
    }

    #endregion


    #region Private

    private float _currentHP;
    private Vector3 _LastPositionInPath;
    private List<Transform> _aggroList = new List<Transform>();
    private SphereCollider _aggroCollider;
    private List<Transform> _pathCheckPoints;
    [HideInInspector]
    public float _damageModifier;
    [HideInInspector]
    public float m_speedAtStart;
    private RaycastHit _hit;
    private bool _isAttacking;
    private float _timerAttackCoolDown;

    #endregion
}
