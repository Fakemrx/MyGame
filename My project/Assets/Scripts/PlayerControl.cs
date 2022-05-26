using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Joystick joystick;
    public float speed;
    public Rigidbody rb;
    public Animator animator;
    public GameObject boss;
    public GameObject legs;
    private bool _isHittingBoss = false;
    float x;
    float y;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        _isHittingBoss = PlayerDamage.isHittingBoss;
        x = joystick.Horizontal;
        y = joystick.Vertical;
        if (x != 0 && y != 0)
        {
            if (_isHittingBoss == true)
            {
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsHittingBoss", true);
            }
            else
            {
                animator.SetBool("IsIdle", false);
                animator.SetBool("IsHittingBoss", false);
            }
            if (x > 0)
            {
                animator.SetBool("IsRunningL", false);
                animator.SetBool("IsRunningR", true);
            }
            else
            {
                if (x < 0)
                {
                    animator.SetBool("IsRunningR", false);
                    animator.SetBool("IsRunningL", true);
                }
            }
            if (y < 0)
            {
                animator.SetBool("IsRunningF", false);
                animator.SetBool("IsRunningB", true);
            }
            else
            {
                if (y > 0)
                {
                    animator.SetBool("IsRunningB", false);
                    animator.SetBool("IsRunningF", true);
                }
            }
        }
        else
        {
            if (x == 0 && y == 0)
            {
                animator.SetBool("IsRunningR", false);
                animator.SetBool("IsRunningL", false);
                animator.SetBool("IsRunningB", false);
                animator.SetBool("IsRunningF", false);
                if (_isHittingBoss == true)
                {
                    animator.SetBool("IsIdle", false);
                    animator.SetBool("IsHittingBoss", true);
                }
                else
                {
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsHittingBoss", false);
                }
            }
        }
        var direction = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
        rb.velocity = rb.transform.TransformDirection(direction);
        transform.LookAt(boss.transform.position);
        boss.transform.LookAt(rb.transform.position);
    }
}
