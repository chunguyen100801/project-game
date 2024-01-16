using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VoltageRect : MonoBehaviour
{    public Vector2 size = new Vector2(2f, 2f); // Kích thước của Rect
    public Color fillColor = Color.blue; // Màu cho hình chữ nhật
    public Color borderColor = Color.red; // Màu cho cạnh viền
    public TMP_Text GameText;
    public string message ;
    void updateTextAbove(){
        GameText.text = message;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi vào vùng trigger, vẽ Rect và chữ
            //DrawRect();
            updateTextAbove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi rời khỏi vùng trigger, xóa Rect và chữ
            ClearRect();
        }
    }

    private void DrawRect()
    {
        // Tính toán vị trí cho chữ
        Vector3 labelPosition = transform.position + new Vector3(0, size.y / 2 + 1.5f, 0);

        // Vẽ hình chữ nhật với cạnh viền tròn trong hàm OnDrawGizmos
        UnityEditor.Handles.DrawSolidRectangleWithOutline(
            new Vector3[] {
                transform.position + new Vector3(-size.x / 2, -size.y / 2, 0),
                transform.position + new Vector3(-size.x / 2, size.y / 2, 0),
                transform.position + new Vector3(size.x / 2, size.y / 2, 0),
                transform.position + new Vector3(size.x / 2, -size.y / 2, 0)
            },
            fillColor,
            borderColor
        );

        // Viết chữ lên hình chữ nhật
   //     UnityEditor.Handles.Label(labelPosition, "Hello, World!");
    }

    private void ClearRect()
    {
        // Xóa hình chữ nhật và chữ
      //  UnityEditor.Handles.ClearHandles();
    }

    // Hàm OnDrawGizmos được gọi khi Scene được vẽ trong Editor
    private void OnDrawGizmos()
    {
        // Bạn cũng có thể vẽ hình chữ nhật mặc định nếu cần thiết
        UnityEditor.Handles.DrawSolidRectangleWithOutline(
            new Vector3[] {
                transform.position + new Vector3(-size.x / 2, -size.y / 2, 0),
                transform.position + new Vector3(-size.x / 2, size.y / 2, 0),
                transform.position + new Vector3(size.x / 2, size.y / 2, 0),
                transform.position + new Vector3(size.x / 2, -size.y / 2, 0)
            },
            fillColor,
            borderColor
        );

        // Tính toán vị trí cho chữ
        Vector3 labelPosition = transform.position + new Vector3(0, size.y / 2 + 1.5f, 0);

        // Viết chữ lên hình chữ nhật mặc định
        UnityEditor.Handles.Label(labelPosition, "Hello, World!");
    }
}
