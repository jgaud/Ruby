using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public ParticleSystem smokeEffect;
    public float speed = 3.0f;
    public float distance = 4.0f;

    Rigidbody2D rigidbody2d;
    float distanceTraveled = 0.0f;
    bool isGoingUp = true;
    bool broken = true;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2d.position;

        if (distanceTraveled < distance)
        {
            if (isGoingUp)
            {
                position.y = position.y + speed * Time.deltaTime; //Going up
                animator.SetFloat("Move Y", 1);
            }
            else
            {
                position.y = position.y + speed * Time.deltaTime * -1; //Going down
                animator.SetFloat("Move Y", -1);
            }
            distanceTraveled += speed * Time.deltaTime;
        }
        else
        {
            isGoingUp = !isGoingUp;
            distanceTraveled = 0;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
