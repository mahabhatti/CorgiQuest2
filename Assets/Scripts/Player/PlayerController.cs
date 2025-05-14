using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Coroutine moveCoroutine;
    public LayerMask solidObjectsLayer;
    public LayerMask validGroundLayer;
    
    private bool isMoving;
    private Vector2 input;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // void Start() // Called before first frame
    // {
    //     
    // }

    private void Update() // Called once per frame
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                
                var targetPos = transform.position + new Vector3(input.x, input.y, 0);
                
                if (IsWalkable(targetPos))
                    if (moveCoroutine == null)
                        moveCoroutine = StartCoroutine(Move(targetPos));
            }
        }
        
        animator.SetBool("isMoving", isMoving);
    }
    
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
    
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
    
        isMoving = false;
        moveCoroutine = null;
    }
    
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        } else if (Physics2D.OverlapCircle(targetPos, 0.1f, validGroundLayer) == null)
        {
            return false;
        }

        return true;
    }
    
    public void CancelMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
            isMoving = false;
        }
    }
}

