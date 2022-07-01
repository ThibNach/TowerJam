using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Exposed

    public LayerMask m_layer;
    public Animator _animator;

    #endregion


    #region Unity API

    private void Start()
    {
        _values = GetComponent<PlayerValues>();
        _camera = GetComponentInChildren<Camera>();
        _playerBuild = GetComponent<PlayerBuild>();
        InitAttackRange();
    }
    private void Update()
    {
        ManageAttackList();
        _timerCoolDown += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _timerCoolDown > _values.m_attackCoolDown && !_playerBuild.buildingModeEnable)
        {
            Attack();
            _timerCoolDown = 0;
        }       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBehavior>() && !other.isTrigger)
        {
            _enemyInAttackRange.Add(other.gameObject.GetComponent<EnemyBehavior>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyBehavior>() && !other.isTrigger)
        {
            _enemyInAttackRange.Remove(other.gameObject.GetComponent<EnemyBehavior>());
        }
    }

    #endregion


    #region Main

    private void InitAttackRange()
    {
        _attackSphere = this.gameObject.AddComponent<SphereCollider>();
        _attackSphere.isTrigger = true;
        _attackSphere.radius = _values.m_attackRange;
        _attackSphere.center = Vector3.zero;
    }
    private void Attack()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, m_layer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            _attackDirection = (hit.point - transform.position);

        }
        StartCoroutine(AnimAttak());
        playerGraphic.transform.LookAt(new Vector3(_attackDirection.x, transform.position.y, _attackDirection.z));
        if (_enemyInAttackRange.Count > 0)
        {
            foreach (EnemyBehavior enemy in _enemyInAttackRange)
            {
                enemy.EnemyTakeDamage(_values.m_DamageToEnemy);
            }
        }
    }

    #endregion


    #region Utils

    private IEnumerator AnimAttak()
    {
        _animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1);
        _animator.SetBool("Attack", false);
    }

    private void ManageAttackList()
    {
        if (_enemyInAttackRange.Count > 0)
        {
            for (int i = 0; i < _enemyInAttackRange.Count; i++)
            {
                if (_enemyInAttackRange[i] == null) _enemyInAttackRange.RemoveAt(i);
            }
        }
    }

    #endregion


    #region Private

    private PlayerValues _values;
    private Vector3 _attackDirection;
    private Camera _camera;
    private SphereCollider _attackSphere;
    private List<EnemyBehavior> _enemyInAttackRange = new List<EnemyBehavior>();
    private float _timerCoolDown;
    private PlayerBuild _playerBuild;
    [SerializeField]
    protected GameObject playerGraphic;

    #endregion
}