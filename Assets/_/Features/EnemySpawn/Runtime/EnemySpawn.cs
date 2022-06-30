using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawn : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private List<Transform> _checkPoints = new List<Transform>();

    #endregion


    #region Unity API
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    #endregion


    #region Main

    public void SpawnEnemy(EnemyBehavior enemy)
    {
        EnemyBehavior enemySpawned = Instantiate(enemy, transform.position, Quaternion.identity);
        enemySpawned.DefinePath(_checkPoints);
        enemySpawned.m_gameManager = _gameManager;
        _gameManager.AddEnemyToList(enemySpawned);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }

    #endregion


    #region Private

    private GameManager _gameManager;

    #endregion
}