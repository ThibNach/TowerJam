using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    #region Unity API

    protected virtual void Start()
    {
        _modifierArea = GetComponent<SphereCollider>();
        _modifierArea.radius = _tower.m_bulletSize;
    }
    protected virtual void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, (_tower.m_bulletSpeed * Time.deltaTime));
        if (transform.position == _target.transform.position) Destroy(this.gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBehavior>() && !other.isTrigger)
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            ApplyDebuff(enemy);
        }
    }

    #endregion


    #region Main

    public virtual void SetTarget(EnemyBehavior target)
    {
        _target = target;
    }
    public virtual void SetValues(BaseTower tower)
    {
        _tower = tower;
    }
    protected abstract void ApplyDebuff(EnemyBehavior enemy);

    #endregion


    #region Private

    protected SphereCollider _modifierArea;
    protected BaseTower _tower;
    protected EnemyBehavior _target;

    #endregion
}