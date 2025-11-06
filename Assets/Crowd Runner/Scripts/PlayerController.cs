using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveSpeed;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Update()
    {
        MoveFoward();

        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            Debug.Log("xScreenDifference: " + xScreenDifference);

            xScreenDifference /= Screen.width; //Chuẩn hóa theo độ lớn màn hình - giữ độ mượt trên mọi phân giải
            xScreenDifference *= slideSpeed;

            Debug.Log("xScreenDifference: " + xScreenDifference);

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            transform.position = position;

            //transform.position = clickedPlayerPosition + Vector3.right * xScreenDifference;
        }

    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
