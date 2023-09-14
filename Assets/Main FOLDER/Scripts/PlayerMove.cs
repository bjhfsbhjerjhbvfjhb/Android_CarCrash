using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerMove : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20f;
    private CharacterController controller;
    private Animator anim;
    private GameManager gameManager;

    public float JumpSpeed = 8.0f;
    public float startSpeed = 6.0f;
    public float speedUpgrade = 0.1f;
    private int speedToText = 40;

    bool isInSwipeArea;
    
    private float now;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    
    private float touchTimer = 0f;
    private float touchTdeg = 0.6f;

    public float jumpTime = 0f;

    public Animator animator;
    public Animator petAnimator;

    public Camera camera;
    
    public GameObject trailsPS;
    public GameObject trailsPS_Bike;

    public ParticleSystem splashPS;
    public TrailRenderer trailRendererWheel;

    public Material trailRender_default;
    public Material trailRender_bike;

    public AudioSource audioSource_Engine;
    
    public AudioClip driftSound;
    public AudioClip correctSound;

    public Animator cameraPlayerPoint;

    private TransformFollower _transformFollower;

    private bool isDrift;
    public bool isJump;
    public bool canJump = true;
    private bool isSpeedObj;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        
        moveDirection = transform.forward;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= startSpeed;

        speedToText = 20;

        UIManager.Instance.ResetScore();
        UIManager.Instance.SetStatus(Constants.StatusTapToStart);

        gameManager.GameState = GameState.Start;

        controller = GetComponent<CharacterController>();
        _transformFollower = camera.GetComponent<TransformFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.GameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
                {
                    gameManager.GameState = GameState.Playing;

                    cameraPlayerPoint.enabled = false;
                    _transformFollower.enabled = true;
                    
                    GameObject road = LeanPool.Spawn(RoadSpawnSystem.Instance.roadPrefab[Random.Range(0, RoadSpawnSystem.Instance.roadPrefab.Length)]);
                    road.transform.position = new Vector3(0,0,0);
                    LeanPool.Despawn(road, 4f);

                    UIManager.Instance.SetStatus(string.Empty);
                    
                    gameManager.startLocationObject.SetActive(false);
                    
                    StartCoroutine("SpeedUpgrade");
                }
                break;
            case GameState.Playing:
                if (isDrift)
                {
                    UIManager.Instance.IncreaseScoreTimer(Random.Range(8,16));
                }
                else
                {
                    UIManager.Instance.IncreaseScoreTimer(Random.Range(1,7));
                }
                

                CheckHeight();

                Swipe();
                
                if (controller.isGrounded && !isDrift)
                {
                    if (gameManager.checkCar == 2)
                    {
                        trailsPS_Bike.SetActive(true);
                    }
                    else
                    {
                        trailsPS.SetActive(true);
                    }
                }
                else
                {
                    if (gameManager.checkCar == 2)
                    {
                        trailsPS_Bike.SetActive(false);
                    }
                    else
                    {
                        trailsPS.SetActive(false);
                    }
                }
                
                audioSource_Engine.pitch = speedUpgrade / 2f;

                //apply gravity
                if (moveDirection.y >= -19.8f)
                {
                    moveDirection.y -= gravity * Time.deltaTime;
                }
                //move the player
                controller.Move(moveDirection * Time.deltaTime * speedUpgrade);

                break;
            case GameState.Dead:
                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
                {
                    //restart
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            default:
                break;
        }
    }
    
    public void Swipe()
    {
        if (!gameManager.CanSwipe && Input.GetMouseButton(0) && controller.isGrounded)
        {
            if (Time.time > touchTimer + touchTdeg)
            {
                /*_transformFollower.distance = Mathf.Lerp(4.2f, 1.6f, Time.deltaTime * 200f);
                _transformFollower.height = Mathf.Lerp(2f, 0.4f, Time.deltaTime * 200f);*/
                
                transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(0.3f, 0.3f, 0.3f), 4 * Time.deltaTime);

                /*if (transform.localScale.x <= 0.31f)
                {
                    transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }*/

                Time.timeScale = 0.7f;

                animator.SetBool("isTapDrift", true);
                isDrift = true;
            }
        }
        else
        {
            /*_transformFollower.distance = Mathf.Lerp(1.6f, 4.2f, Time.deltaTime * 200f);
            _transformFollower.height = Mathf.Lerp(0.4f, 2f, Time.deltaTime * 200f);*/
            
            transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(1f, 1f, 1f), 4 * Time.deltaTime);
            
            /*if (transform.localScale.x >= 0.99f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }*/
            
            Time.timeScale = 1f;
            
            isDrift = false;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
       
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            now = Vector3.Distance(firstPressPos, secondPressPos);
            
            currentSwipe.Normalize();
            
            //swipe upwards
            if(controller.isGrounded && currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f  && now >= 100 && canJump)
            {
                moveDirection.y = JumpSpeed;
                UIManager.Instance.IncreaseScore(Random.Range(10,20));

                isJump = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.carJump);
                
                trailRendererWheel.enabled = false;

                short jumpRand = (short)Random.Range(0, 2);
                if (jumpRand == 0)
                {
                    animator.SetTrigger("isJump");
                    petAnimator.SetTrigger("isPetJump");
                }
                else
                {
                    animator.SetTrigger("isJump2");
                    petAnimator.SetTrigger("isPetJump");
                }
            }
            //swipe down
            if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && now >= 30)
            {
                if (controller.isGrounded)
                {
                    UIManager.Instance.IncreaseScore(Random.Range(10,20));
                    StartCoroutine(TurnWheelTrailOnTime(0.6f));
                    animator.SetTrigger("isSpin");

                    short rand = (short)Random.Range(0, 2);
                    if (rand == 0)
                    {
                        petAnimator.SetTrigger("isDriftRoll");
                    }
                    else
                    {
                        petAnimator.SetTrigger("isDriftRoll2");
                    }
                }
                else if (!controller.isGrounded && isJump)
                {
                    moveDirection.y = -JumpSpeed;
                }
            }
            //swipe left
            if(gameManager.CanSwipe && controller.isGrounded && currentSwipe.x < 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && now >= 20)
            {
                animator.SetTrigger("isLeftSwipe");
                petAnimator.SetTrigger("isPetSwipeLeft");
                
                SoundManager.Instance.PlayOneShot(driftSound);
                SoundManager.Instance.PlayOneShot(correctSound);
                
                UIManager.Instance.SetScore(50);
                
                StartCoroutine(TurnWheelTrailOnTime(0.6f));
                        
                transform.Rotate(0, -90, 0);
                moveDirection = Quaternion.AngleAxis(-90, Vector3.up) * moveDirection;
                gameManager.CanSwipe = false;
            } 
                
            //swipe right
                
            if(gameManager.CanSwipe && controller.isGrounded && currentSwipe.x > 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && now >= 20)
            {
                animator.SetTrigger("isRightSwipe");
                petAnimator.SetTrigger("isPetSwipeRight");
                        
                SoundManager.Instance.PlayOneShot(driftSound);
                SoundManager.Instance.PlayOneShot(correctSound);
                        
                UIManager.Instance.SetScore(50);
                
                StartCoroutine(TurnWheelTrailOnTime(0.6f));
                        
                transform.Rotate(0, 90, 0);
                moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
                gameManager.CanSwipe = false;
            }

            if (animator.GetBool("isTapDrift"))
            {
                animator.SetBool("isTapDrift", false);
                touchTimer = Time.time;
            }
        }
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Road") && controller.isGrounded && isJump)
        {
            splashPS.Play();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.carLand);
            isJump = false;
        }
    }
    
    private void CheckHeight()
    {
        if (transform.position.y < -5)
        {
            gameManager.Die();
        }
    }

    public void JumpBoosterStart()
    {
        moveDirection.y = JumpSpeed * 1.4f;
    }

    public void TurnOffDrift()
    {
        if (animator.GetBool("isTapDrift"))
        {
            animator.SetBool("isTapDrift", false);
            touchTimer = Time.time;
        }
        
        transform.localScale = new Vector3(1f,1f,1f);
            
        Time.timeScale = 1f;
            
        isDrift = false;
    }

    IEnumerator TurnWheelTrailOnTime(float time)
    {
        trailRendererWheel.enabled = true;
        trailRendererWheel.emitting = true;
        yield return new WaitForSeconds(time);
        trailRendererWheel.emitting = false;
        yield return new WaitForSeconds(0.2f);
        trailRendererWheel.enabled = false;
    }

    IEnumerator SpeedUpgrade()
    {
        while(true)
        {
            if (speedUpgrade <= 5f)
            {
                if (speedUpgrade >= 2.4f && !isSpeedObj)
                {
                    gameManager.particleSpeed_Obj.SetActive(true);
                    gameManager.trailRedLine_Obj.SetActive(true);
                    isSpeedObj = true;
                }
                speedToText += Random.Range(5, 10);
                UIManager.Instance.SpeedText.text = speedToText + " км/ч";
                UIManager.Instance.SpeedShadowText.text = UIManager.Instance.SpeedText.text;
                speedUpgrade += 0.1f;
                JumpSpeed -= 0.16f;

                if (speedUpgrade <= 70)
                {
                    camera.fieldOfView += 0.5f;
                }

                if (UIManager.Instance.scoreTdeg > 0.5f)
                {
                    UIManager.Instance.scoreTdeg -= 0.2f;
                }
                yield return new WaitForSeconds(10f); 
            }
            else
            {
                StopCoroutine("SpeedUpgrade");
            }
        }
    }
}
