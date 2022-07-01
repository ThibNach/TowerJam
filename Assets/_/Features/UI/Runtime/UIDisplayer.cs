using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplayer : MonoBehaviour
{
    #region Exposed
 
  

    [SerializeField]
    private TextMeshProUGUI _nbWaveDisplayer;
    [SerializeField]
    private TextMeshProUGUI _nbEnemiesAlive;
    [SerializeField]
    private TextMeshProUGUI _nbPlayerLife;
    [SerializeField]
    private TextMeshProUGUI _nbStone;
    [SerializeField]
    private TextMeshProUGUI _nbWood;
    [SerializeField]
    private Image _playerLifeBar;
    [SerializeField]
    private Image _baseLifeBar;
    [SerializeField]
    private TextMeshProUGUI _breakTimer;

    #endregion


    #region Unity API
    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerValues = FindObjectOfType<PlayerValues>();
        _baseValues = FindObjectOfType<BaseBehav>();
        _ressources = FindObjectOfType<RessourcesManager>();
    }

    private void Update()
    {
        _nbWaveDisplayer.text = _gameManager.m_waveNumber.ToString();
        _nbEnemiesAlive.text = _gameManager._livingEnemies.Count.ToString();
        _nbPlayerLife.text = _playerValues._currentLifes.ToString();
        _nbStone.text = _ressources.valueRessourceA.ToString();
        _nbWood.text = _ressources.valueRessourceB.ToString();
        if (_gameManager._isInBreakPhase)
        {
            _breakTimer.gameObject.SetActive(true);
            _breakTimer.text = (_gameManager._breakTimeBetweenWaves - _gameManager._breakPhaseTimer).ToString();
        }
        else _breakTimer.gameObject.SetActive(false);

        _playerLifeBar.fillAmount = _playerValues._currentHP / _playerValues._startHP;
        _baseLifeBar.fillAmount = _baseValues._currentHP / _baseValues._pvAtStart;
    }

    #endregion

    #region Private

    private PlayerValues _playerValues;
    private BaseBehav _baseValues;
    private GameManager _gameManager;
    private RessourcesManager _ressources;

    #endregion
}
