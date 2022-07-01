using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    #region Exposed

    [Header("Player Values")]
    [SerializeField]
    public float _startHP;
    public float m_DamageToEnemy;
    public float m_attackRange;
    public float m_attackCoolDown;
    public int m_nbLifes;

    #endregion


    #region Unity API

    private void Awake()
    {
        _currentHP = _startHP;
        _currentLifes = m_nbLifes;
    }

    #endregion


    #region Main

    public void PlayerTakeDamage(float damage) => _currentHP -= damage;    

    #endregion


    #region Private

    public float _currentHP;
    public int _currentLifes;

    #endregion
}
