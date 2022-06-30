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
    private float _attackSpeed;
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
        if (IsAggro()) MoveToTarget();
        else if (_pathCheckPoints.Count > 0) FollowPath();
        CheckPath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.GetComponent<BaseTower>()) _aggroList.Add(other.gameObject.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
        //{
        //    _aggroList.Remove(other.gameObject.transform);
        //}
    }

    #endregion


    #region Main

    private void EnemyMakeDamageToTower(BaseTower target)
    {
        target.TowerTakeDamage(_damagesToBase);
    }
    public void EnemyTakeDamage(float damage) => _currentHP -= damage;   
    public void StartDebuffDamage(float modifier,float time)
    {
        StartCoroutine(ModifieDamages(modifier, time));
    }
    private  IEnumerator ModifieDamages(float modifier, float time)
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
    private void FollowPath() => this.transform.position = Vector3.MoveTowards(transform.position, _pathCheckPoints[0].position, _enemySpeed * Time.deltaTime);
    private void MoveToTarget() => this.transform.position = Vector3.MoveTowards(transform.position, _aggroList[0].position, _enemySpeed * Time.deltaTime);
    private void InitAggroSphere()
    {
        _aggroCollider = this.gameObject.AddComponent<SphereCollider>();
        _aggroCollider.isTrigger = true;
        _aggroCollider.radius = _aggroRadius;
        _aggroCollider.center = Vector3.zero;
    }
    private bool IsAggro() => (_aggroList.Count > 0);
    public void DefinePath(List<Transform> path) => _pathCheckPoints = new List<Transform>(path);

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

    #endregion
}
