using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // Kéo đối tượng Car vào đây
    public Vector3 offset;       // Khoảng cách cố định giữa Camera và xe
    public float smoothSpeed = 5f; // Độ mượt khi bám theo

    void Start()
    {
        // Tự động tính khoảng cách ban đầu nếu bạn đã đặt Camera ở vị trí đẹp
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    // Dùng LateUpdate để Camera di chuyển sau khi xe đã di chuyển xong
    void LateUpdate()
    {
        if (target == null) return;

        // Vị trí mới mà Camera cần tới (chỉ thay đổi vị trí, KHÔNG xoay theo xe)
        Vector3 desiredPosition = target.position + offset;
        
        // Di chuyển mượt mà đến vị trí đó
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}


