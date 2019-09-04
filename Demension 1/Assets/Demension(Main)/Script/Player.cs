using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public GameObject obj;
    public Animator animator;
    public float speed = 5f;
    public float rotatespeed = 1f;
    Rigidbody rigd;
    float h;
    float v;
    Vector3 movement;
    private AudioSource audio;
    public AudioClip Walk_S;

    private void Awake()
    {
        rigd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = Walk_S;
        this.audio.loop = false;
    }
     void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        AnimationUpdate();
    }
    void FixedUpdate()
    {
            Turn();
            Run();
    }

    void Run()
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;

        rigd.MovePosition(transform.position + movement);
    }
    void Turn()
    {
        if (h == 0 && v == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement);
        rigd.rotation = Quaternion.Slerp(rigd.rotation, newRotation,rotatespeed *Time.deltaTime);
    }
    void AnimationUpdate()
    {
        if (h == 0 && v == 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isLumber", true);
        }
        else
        {
            animator.SetBool("isLumber", false);
        }
    }
}