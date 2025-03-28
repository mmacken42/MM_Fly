using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private FlyingGameLogicManager gameLogicManager;

    void Start()
    {
        gameLogicManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlyingGameLogicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            gameLogicManager.IncrementScore();
        }
    }
}
