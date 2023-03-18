using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimator : MonoBehaviour
{

    Vector2 movement;
    private Animator animator;

    Vector2 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        //Flip code
        if(previousPos.x > transform.position.x){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if(previousPos.x < transform.position.x){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }       
        previousPos = transform.position;
    }
}
