using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    #region Exposed

    [Header("Player Values")]
    [SerializeField]
    private float _startHP;
    public float m_DamageToEnemy;
    public float m_attackRange;
    public float m_attackCoolDown;

    #endregion


    #region Unity API

    private void Awake()
    {
        _currentHP = _startHP;
    }

    #endregion


    #region Main

    public void PlayerTakeDamage(float damage) => _currentHP -= damage;    

    #endregion


    #region Private

    private float _currentHP;

    #endregion
}
