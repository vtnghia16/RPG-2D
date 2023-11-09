using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player"; // Mục tiêu tấn công player


    [SerializeField] private float xVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            collision.GetComponent<CharacterStats>()?.TakeDamage(damage);
            StuckInto(collision);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StuckInto(collision);
        }
    }

    // Bị mắc kẹt vào
    private void StuckInto(Collider2D collision)
    {
        // Không gây sát thương khi arrow mắc kẹt vào nhân vật
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;

        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject, Random.Range(5, 7));
    }

    // Xoay mũi tên
    public void FlipArrow()
    {
        if (flipped)
            return;


        xVelocity = xVelocity * -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetLayerName = "Enemy";
    }
}
