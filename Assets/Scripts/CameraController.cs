using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    public const float MAX_HORIZONTAL_OFFSET = 5.0f;
    public const float MAX_VERTICAL_OFFSET = 2.5f;

    void Start()
    {
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 thisPosition = transform.position;
        Vector3 targetPosition = Target.transform.position;
        float x = Mathf.Clamp(thisPosition.x, targetPosition.x - MAX_HORIZONTAL_OFFSET, targetPosition.x + MAX_HORIZONTAL_OFFSET);
        float y = Mathf.Clamp(thisPosition.y, targetPosition.y - MAX_VERTICAL_OFFSET, targetPosition.y + MAX_VERTICAL_OFFSET);
        transform.position = new Vector3(x, y, thisPosition.z);
    }
}
