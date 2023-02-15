using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private int bonusScore;
    [SerializeField] private float timeToDestroy;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TakeBonus(collision.gameObject);
        }
    }

    private void TakeBonus(GameObject P)
    {
        Player player = P.GetComponent<Player>();
        player.AddScore(bonusScore);
        anim.SetBool("IsCollected", true);
        Invoke("Delete", timeToDestroy);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
