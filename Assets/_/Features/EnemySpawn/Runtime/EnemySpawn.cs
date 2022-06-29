using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class EnemySpawn : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private List<Transform> _checkPoints = new List<Transform>();

        #endregion


        #region Main

        public void SpawnEnemy(EnemyBehavior enemy)
        {
            EnemyBehavior enemySpawned = Instantiate(enemy, transform.position, Quaternion.identity);
            enemySpawned.DefinePath(_checkPoints);
            GameManager.AddEnemyToList(enemySpawned);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, .5f);
        }


        #endregion
    }
