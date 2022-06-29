using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected string forwardKey;
    [SerializeField]
    protected float speed;
    
    [SerializeField]
    protected Rect areaMoving;
    public Vector3 position;

    private void Update()
    {
        MovePlayer();
        ClampPositionInArea();
    }

    protected void MovePlayer()
    {
        var axisHorizontal = Input.GetAxisRaw("Horizontal");
        var axisVertical = Input.GetAxisRaw("Vertical");
        
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
}
