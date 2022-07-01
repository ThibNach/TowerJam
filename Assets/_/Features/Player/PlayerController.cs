using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected string forwardKey;
    public float speed;
    
    [SerializeField]
    protected Rect areaMoving;
    public Vector3 position;
    [HideInInspector]
    public float m_speedAtStart;
    public Animator _animator;

    private void Awake()
    {
        m_speedAtStart = speed;
    }
    private void Update()
    {
        MovePlayer();
        ClampPositionInArea();
        
    }

    protected void MovePlayer()
    {
        var axisHorizontal = Input.GetAxisRaw("Horizontal");
        var axisVertical = Input.GetAxisRaw("Vertical");
        _animator.SetFloat("xMov", Input.GetAxisRaw("Horizontal"));
        _animator.SetFloat("yMov", Input.GetAxisRaw("Vertical"));
        
        Vector3 moveHorizontal = transform.right * axisHorizontal;
        Vector3 moveVertical = transform.forward * axisVertical;
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        transform.Translate(velocity * Time.deltaTime);
    }

    protected void ClampPositionInArea()
    {
        position = transform.position;
        position.x = Mathf.Clamp(position.x, areaMoving.x, areaMoving.width);
        position.z = Mathf.Clamp(position.z, areaMoving.y, areaMoving.height);
        transform.position = position;
    }

    public void StartSpeedBuff(float modifier,float timeafterleave)
    {
        StartCoroutine(SpeedBuff(modifier, timeafterleave));
    }
    private IEnumerator SpeedBuff(float modifier, float timeafterleave)
    {
        speed *= modifier;
        yield return new WaitForSeconds(timeafterleave);
        speed = m_speedAtStart;
    }
}
