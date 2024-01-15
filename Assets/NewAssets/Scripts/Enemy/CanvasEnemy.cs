using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Canvas canvas;

    void Update()
    {
        // Kiểm tra xem enemy và canvas có tồn tại không
        if (enemy != null && canvas != null)
        {
            // Chuyển đổi vị trí của enemy từ thế giới sang màn hình
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);

            // Đặt vị trí của Canvas theo vị trí của enemy trên màn hình
            canvas.transform.position = enemy.transform.position;
        }
    }
}
