using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private CanonType _canonType;
    #endregion


    #region Unity API

    private void Start()
    {
        _tower = GetComponentInParent<BaseTower>();
    }
    private void Update()
    {
        if (_tower.m_enemiesInAttackRange.Count > 0)
        {
            switch (_canonType)
            {
                case CanonType.CANON:
                    transform.LookAt(new Vector3(_tower.m_enemiesInAttackRange[0].transform.position.x-transform.position.x,0,0));
                    break;
                case CanonType.PLATEAU:
                    transform.LookAt(new Vector3(0, _tower.m_enemiesInAttackRange[0].transform.position.y-transform.position.y,0));
                    break;
                default:
                    break;
            }
            transform.LookAt(_tower.m_enemiesInAttackRange[0].transform.position);
        }
    }

    #endregion


    #region Private

    private BaseTower _tower;
    private enum CanonType
    {
        CANON,
        PLATEAU
    }

    #endregion
}
