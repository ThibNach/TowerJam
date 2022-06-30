using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTotem : BaseTotem
{
    #region Exposed

    [SerializeField]
    private float _speedModifier;
    [SerializeField]
    private float _totemAreaSize;
    [SerializeField]
    private float _timeBuffStayAfterLeaveArea;

    #endregion


    #region Unity API

    private void Awake()
    {
        InitSphere();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && !other.isTrigger && other.gameObject.GetComponent<PlayerController>().speed == other.gameObject.GetComponent<PlayerController>().speed)
        {
            other.GetComponent<PlayerController>().StartSpeedBuff(_speedModifier, 0.01f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && !other.isTrigger && other.gameObject.GetComponent<PlayerController>().speed == other.gameObject.GetComponent<PlayerController>().speed)
        {
            other.GetComponent<PlayerController>().StartSpeedBuff(_speedModifier, _timeBuffStayAfterLeaveArea);
        }
    }

    #endregion


    #region Main

    private void InitSphere()
    {
        _area = this.gameObject.AddComponent<SphereCollider>();
        _area.isTrigger = true;
        _area.radius = _totemAreaSize;
        _area.center = Vector3.zero;
    }

    #endregion


        #region Private

    private SphereCollider _area;

    #endregion
}
