using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target; // Đối tượng mà camera sẽ theo dõi
    public float smoothTime = 0.3f; // Thời gian để làm mượt chuyển động
    private Vector3 velocity = Vector3.zero; // Vận tốc hiện tại của camera, được sử dụng bởi SmoothDamp

    // Giới hạn vùng theo dõi của camera
    public Vector2 minXAndY;
    public Vector2 maxXAndY;

    void Start()
    {
        // Kiểm tra nếu target không được gán
        if (target == null)
        {
           target = FindObjectOfType<PlayerMoveMent>().gameObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Vị trí mục tiêu của camera, giữ nguyên trục Z để không thay đổi độ sâu
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Giới hạn vị trí của camera
            targetPosition.x = Mathf.Clamp(targetPosition.x, minXAndY.x, maxXAndY.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minXAndY.y, maxXAndY.y);

            // Làm mượt chuyển động của camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
