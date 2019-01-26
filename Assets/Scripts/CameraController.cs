using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public const float MAX_HORIZONTAL_OFFSET = 5.0f;
    public const float MAX_VERTICAL_OFFSET = 2.5f;

    private Camera _camera;

    public GameObject Target;

    public Vector2 Bounds1;
    public Vector2 Bounds2;

    void Start()
    {
        _camera = GetComponent<Camera>();
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

        // camera bounds
        if (Bounds1 != Bounds2)
        {
            float verticalExtents = _camera.orthographicSize;
            float horizontalExtents = verticalExtents * Screen.width / Screen.height;
            Vector2 min = new Vector2(Mathf.Min(Bounds1.x, Bounds2.x), Mathf.Min(Bounds1.y, Bounds2.y));
            Vector2 max = new Vector2(Mathf.Max(Bounds1.x, Bounds2.x), Mathf.Max(Bounds1.y, Bounds2.y));

            if (horizontalExtents * 2 > max.x - min.x)
            {
                x = (max.x + min.x) / 2;
            }
            else
            {
                x = Mathf.Clamp(x, min.x + horizontalExtents, max.x - horizontalExtents);
            }

            if (verticalExtents * 2 > max.y - min.y)
            {
                y = (max.y + min.y) / 2;
            }
            else
            {
                y = Mathf.Clamp(y, min.y + verticalExtents, max.y - verticalExtents);
            }
        }

        transform.position = new Vector3(x, y, thisPosition.z);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 min = new Vector2(Mathf.Min(Bounds1.x, Bounds2.x), Mathf.Min(Bounds1.y, Bounds2.y));
        Vector3 max = new Vector2(Mathf.Max(Bounds1.x, Bounds2.x), Mathf.Max(Bounds1.y, Bounds2.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(min.x, min.y, -10), new Vector3(min.x, max.y, -10));
        Gizmos.DrawLine(new Vector3(min.x, min.y, -10), new Vector3(max.x, min.y, -10));
        Gizmos.DrawLine(new Vector3(max.x, max.y, -10), new Vector3(min.x, max.y, -10));
        Gizmos.DrawLine(new Vector3(max.x, max.y, -10), new Vector3(max.x, min.y, -10));
    }
}
