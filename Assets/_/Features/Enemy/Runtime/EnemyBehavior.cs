using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    #region Exposed

    [Header("Enemy Behaviour")]
    [SerializeField]
    private float _MaxHP;
    [SerializeField]
    private float _enemySpeed;
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
    }
    private void Update()
    {
        if (IsAggro()) MoveToTarget();
        else if (_pathCheckPoints.Count > 0) FollowPath();
        CheckPath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower")) _aggroList.Add(other.gameObject.transform);


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Tower"))
        {
            _aggroList.Remove(other.gameObject.transform);
        }
    }

    #endregion


    #region Main

    public IEnumerator ModifieMoveSpeed(float modifier,float time)
    {
        float initialspeed = _enemySpeed;
        _enemySpeed /= modifier;
        yield return new WaitForSeconds(time);
        _enemySpeed = initialspeed;
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

    #endregion
}
