using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    
    public TextBoxManager tBoxManager;
    SpriteRenderer sprite;

    private const float speed = 4f;
    private const float verticalMovement = 0.25f;
    private const float lowestVerticalLimit = 0.25f; // players moves between 2.25 -> 3.0f in 0.25 intervals 
    private const float highestVerticalLimit = 0.75f;   // *4 steps/level*
    private const float leftHorizontalLimit = -9.75f;
    private const float rightHorizontalLimit = 6.75f;

    public bool canMove;

    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!canMove)
        {
            return;
        }
        Move();
	}


    void Move()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A) && gameObject.transform.position.x > leftHorizontalLimit)
            {
                AudioManager.Instance.FootStepSplashAudio();
                gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
                sr.flipX = true;
            }
            if (Input.GetKey(KeyCode.D) && gameObject.transform.position.x < rightHorizontalLimit)
            {
                AudioManager.Instance.FootStepSplashAudio();
                gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
                sr.flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.W) && gameObject.transform.position.y < highestVerticalLimit)
            {
                AudioManager.Instance.FootStepSplashAudio();
                if (sr.sortingOrder > 0)
                {
                    sr.sortingOrder--;
                }
                gameObject.transform.Translate(Vector2.up * verticalMovement);
            }
            if (Input.GetKeyDown(KeyCode.S) && gameObject.transform.position.y > lowestVerticalLimit)
            {
                AudioManager.Instance.FootStepSplashAudio();
                if (sr.sortingOrder < 4)
                {
                    sr.sortingOrder++;
                }
                gameObject.transform.Translate(Vector2.down * verticalMovement);
            }
        }
    }
}
