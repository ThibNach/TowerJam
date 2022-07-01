using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehav : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    public float _pvAtStart;

    #endregion

    #region Unity API

    private void Awake()
    {
        _currentHP = _pvAtStart;
    }

    #endregion

    #region Main

    public void BaseTakeDamage(float damage)
    {
        _currentHP -= damage;
        if (_currentHP <= 0) GameOver();
    }
private void GameOver()
    {

    }

    #endregion

    #region Private

    public float _currentHP;

    #endregion
}

