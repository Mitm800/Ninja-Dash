using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    private bool isGravityInverse = false;
    private bool canChangeGravity = true;

    public GameObject bloodSplashEffect;

    [Header("Player Movement")]
    public float speed;
    public float jumpForce;
    public float gravityChangeCooldown;

    
    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayerMask;
    public bool isGrounded;

    [Header("Player Objects")]
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer sprite;
    private Animator player_animator;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player_animator = GetComponent<Animator>();
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTrap = null;
        
        foreach (GameObject trap in traps)
        {
            float distanceToTrap = Vector3.Distance(transform.position, trap.transform.position);
            if(distanceToTrap < 5.0f){
                shortestDistance = distanceToTrap;
                nearestTrap = trap;
            }

            if(nearestTrap != null && shortestDistance <= 5.0f){
                CamZoom.ZoomActive = true;
            } else {
                CamZoom.ZoomActive = false;
            }
        }


    }


    void Update()
    {
        InputGravityChange();
        if(Input.GetKeyDown(KeyCode.Space)){
            SoundManager.PlaySound("Jump");
        }
    }

    void FixedUpdate()
    {
       Move();
    }

      void InputGravityChange()
    {
        if (canChangeGravity == false) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.PlaySound("Inverse");
            ChangeGravity();
            canChangeGravity = false;
            Invoke(nameof(EnableGravityChange), gravityChangeCooldown);
        }
    }
    
    void EnableGravityChange()
    {
        canChangeGravity = true;
    }
    
    private void ChangeGravity(){
        rigidBody2D.gravityScale *= -1;
        isGravityInverse = !isGravityInverse;
        if(isGravityInverse){
            transform.eulerAngles = new Vector3(0, 0 ,180f);
            sprite.flipX = true;
        }else{
            transform.eulerAngles = Vector3.zero;
            sprite.flipX = false;    
        }
    }

    private void Move(){
        float y = 0;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        if(isGrounded){
            y = Input.GetAxisRaw("Jump");
            y *= isGravityInverse?-1:1;
            player_animator.SetInteger("CharacterState", 0);
        } else {
            player_animator.SetInteger("CharacterState", 1);
        }
        Vector2 move = new Vector2(0, y * jumpForce);
        rigidBody2D.AddForce(move);
        rigidBody2D.velocity = new Vector3 (speed, rigidBody2D.velocity.y, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void Die(){
        if(isGravityInverse){
            ChangeGravity();
        }
        SoundManager.PlaySound("Death");
        player_animator.Play("Death");
        speed = 0f;
        Instantiate(bloodSplashEffect, transform.position, Quaternion.identity);
        CamZoom.Instance.shakeCamera(10f, 0.1f);
        Invoke(nameof(LoadScene), 2.5f);
    }

    public void Win(){
        SoundManager.PlaySound("Victory");
        Invoke(nameof(LoadScene), 6.0f);
    }

    private void LoadScene(){
        SceneManager.LoadScene("Menu");
    }
}
