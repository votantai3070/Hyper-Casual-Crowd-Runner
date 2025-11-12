using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed;

    private void Update()
    {
        TurnCoin();
    }

    private void TurnCoin()
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
