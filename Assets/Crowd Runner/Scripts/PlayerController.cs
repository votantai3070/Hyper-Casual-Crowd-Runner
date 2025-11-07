using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    private CrowSystem crowdSystem;

    [Header("Setting")]
    [SerializeField] private float roadWidth;
    [SerializeField] private float moveSpeed;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Start()
    {
        crowdSystem = GetComponent<CrowSystem>();
    }

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

            //Debug.Log("xScreenDifference: " + xScreenDifference);

            xScreenDifference /= Screen.width; //Chuẩn hóa theo độ lớn màn hình - giữ độ mượt trên mọi phân giải
            xScreenDifference *= slideSpeed;

            //Debug.Log("xScreenDifference: " + xScreenDifference);

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            //Debug.Log("Road Width: " + roadWidth);
            //Debug.Log("Crowd Radius: " + crowdSystem.GetCrowdRadius());

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(),
                roadWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;
        }

    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
