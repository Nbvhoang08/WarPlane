using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cloud : MonoBehaviour
{
    public float speed = 5f;
public float offset = 2f; // Khoảng cách ngoài màn hình
private float initialPositionX;

void Awake()
{
    initialPositionX = transform.position.x;
}

void Start()
{
    MoveCloud();
}

void MoveCloud()
{
    float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    float targetPositionX = -screenWidth - offset;
    float startPositionX = screenWidth + offset;

    // Tính toán khoảng cách và thời gian di chuyển
    float distance = Mathf.Abs(transform.position.x - targetPositionX);
    float moveTime = distance / speed;

    // Di chuyển đám mây
    transform.DOMoveX(targetPositionX, moveTime).SetEase(Ease.Linear).OnComplete(() =>
    {
        // Kiểm tra xem đối tượng có bị hủy không trước khi đặt lại vị trí
        if (this != null && transform != null)
        {
            // Đặt lại vị trí của đám mây ở phía bên phải màn hình
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
            // Gọi lại hàm MoveCloud để tiếp tục di chuyển
            MoveCloud();
        }
    });
}
}
