using Unity.VisualScripting;
using UnityEngine;

public class PlayerBirdScript : MonoBehaviour
{
    //player attributes
    public float flapStrength = 20f;
    
    //separate sprites for the bird's wings
    public SpriteRenderer wingsUp;
    public SpriteRenderer wingsDown;
    private float wingFlapRate = 0.25f;
    private float wingFlapTimer = 0f;

    //local player components
    private Rigidbody2D myRigidbody;
    private AudioSource sfxFlapWings;

    //game logic ref
    private FlyingGameLogicManager gameLogicManager;

    void Start()
    {
        gameLogicManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlyingGameLogicManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        sfxFlapWings = GetComponent<AudioSource>();

        //fly up once on Start so player doesn't begin falling immediately on play
        FlyUp();
    }

    void Update()
    {
        if (!gameLogicManager.GetIsGameOver())
        {
            //if user hits Spacebar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FlyUp();
            }

            //spawn a new obstacle each time our timer reaches threshold value
            if (wingFlapTimer < wingFlapRate)
            {
                wingFlapTimer += Time.deltaTime;
            }
            else
            {
                //switch visible wings sprites
                if (wingsDown.enabled)
                {
                    AnimateWingsUp();
                }
                else
                {
                    AnimateWingsDown();
                }

                wingFlapTimer = 0f;
            }

            //lock player rotation while they are alive
            transform.rotation = Quaternion.identity;

            //if player leaves screen at top or bottom, game over
            if (transform.position.y > 28.5f || transform.position.y < -28.5f)
            {
                gameLogicManager.GameOver();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameLogicManager.GetIsGameOver())
        {
            gameLogicManager.GameOver();
        }
    }

    void FlyUp()
    {
        //fly up
        myRigidbody.linearVelocity = Vector2.up * flapStrength;

        //sfx for input/flying
        sfxFlapWings.Play();
    }

    void AnimateWingsUp()
    {
        wingsUp.enabled = true;
        wingsDown.enabled = false;
    }

    void AnimateWingsDown()
    {
        wingsDown.enabled = true;
        wingsUp.enabled = false;
    }
}
