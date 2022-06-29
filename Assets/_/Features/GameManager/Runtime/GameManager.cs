using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class GameManager : MonoBehaviour
    {
        #region Exposed

        [SerializeField]
        private List<EnemySpawn> _enemySpawns = new List<EnemySpawn>();
        [SerializeField]
        private List<WaveObject> _firstWaveSetUp = new List<WaveObject>();

        #endregion


        #region Unity API

        private void Awake()
        {
            InitArrays();
        }
        private void Update()
        {
            IncrementArrayWithTime(_timers);
            HandleEnemySpawn();
        }

        #endregion


        #region Main

        private void InitArrays()
        {
            _timers = new float[_firstWaveSetUp.Count];
            ClearFloatArray(_timers);
            _nbSpawned = new int[_firstWaveSetUp.Count];
            ClearIntArray(_nbSpawned);
        }
        private void HandleEnemySpawn()
        {
            for (int i = 0; i < _firstWaveSetUp.Count; i++)
            {
                if (_firstWaveSetUp[i].m_enemyType != null && _firstWaveSetUp[i].m_nbSpawn > _nbSpawned[i] && _firstWaveSetUp[i].m_cdSpawn < _timers[i])
                {
                    _enemySpawns[Random.Range(0, _enemySpawns.Count)].SpawnEnemy(_firstWaveSetUp[i].m_enemyType);
                    _timers[i] = 0;
                    _nbSpawned[i]++;
                }
            }
        }

        #endregion


        #region Utils

        private void ClearFloatArray(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        private void ClearIntArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        private void IncrementArrayWithTime(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] += Time.deltaTime;
            }
        }

        public static void AddEnemyToList(EnemyBehavior enemy)
        {
            _livingEnemies.Add(enemy);
        }
        #endregion


        #region Private

        private float[] _timers;
        private int[] _nbSpawned;
        private static List<EnemyBehavior> _livingEnemies = new List<EnemyBehavior>();

        #endregion
    }
