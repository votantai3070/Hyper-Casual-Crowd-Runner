using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Elements")]
    private CrowdSystem crowdSystem;
    private PlayerAnimation playerAnimation;

    [Header("Setting")]
    [SerializeField] private float roadWidth;
    [SerializeField] private float moveSpeed;
    private bool canMove;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        crowdSystem = GetComponent<CrowdSystem>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveFoward();
            ManageControl();
        }
    }

    private void GameStateChangedCallBack(GameState gameState)
    {
        if (gameState == GameState.Game)
            StartMoving();

        else if (gameState == GameState.GameOver)
            StopMoving();

    }

    private void StartMoving()
    {
        canMove = true;

        playerAnimation.Run();
    }

    private void StopMoving()
    {
        canMove = false;

        playerAnimation.Idle();
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
