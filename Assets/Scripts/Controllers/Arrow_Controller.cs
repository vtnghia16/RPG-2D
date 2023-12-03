using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private string targetLayerName = "Player"; // Mục tiêu tấn công player


    [SerializeField] private float xVelocity; // Tốc độ của mũi tên theo X
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private CharacterStats stats;

    private void Update()
    {
        if (canMove)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void SetupArrow(float _speed, CharacterStats _stats)
    {
        xVelocity = _speed;
        stats = _stats;
    }

    // Xử lý va chạm khi mũi tên tiếp xúc với nhân vật
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);


            stats.DoDamage(collision.GetComponent<CharacterStats>());

            // Nếu va chạm quái vật thì clear arrow
            if (targetLayerName == "Enemy")
                Destroy(gameObject);

            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
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

        // Khi mắc kẹt clear arrow 5-7s
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
