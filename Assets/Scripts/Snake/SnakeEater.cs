using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEater : MonoBehaviour
{
    public GameObject bodyPartPrefab;

    public SnakeHealth snakeHealth;
    public ScoreManager scoreManager;

    public Sprite altBPSprite;

    private GameObject latestBodyPart;

    private int numBodyParts = 0;

    private void Start()
    {
        latestBodyPart = transform.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Apple"))
        {
            scoreManager.UpdateScore();

            // Spawn new body part
            bodyPartPrefab.GetComponent<FollowTheLeader>().leader = latestBodyPart;
            var bodyPart = Instantiate(bodyPartPrefab, transform.position, transform.rotation);
            numBodyParts++;
            if (numBodyParts % 2 == 0)
                bodyPart.GetComponent<SpriteRenderer>().sprite = altBPSprite;
            bodyPart.transform.parent = transform.parent; // Attach to Snake GO
            latestBodyPart = bodyPart;
        }

        if (collision.tag != "bodypart") return;
        snakeHealth.Health--;

        GetComponent<AudioSource>().Play();
    }
}
