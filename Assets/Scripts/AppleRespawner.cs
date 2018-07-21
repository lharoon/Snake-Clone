using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRespawner : MonoBehaviour
{
    private bool hasSpawned = false;

    private void Update()
    {
        if (hasSpawned) return;

        if (!GameManager.hasStarted) return;

        transform.position = GetRandomPosition();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        hasSpawned = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false; // Hide graphic until position is set
        transform.position = GetRandomPosition();
        sprite.enabled = true;

        GetComponent<AudioSource>().Play();
    }

    Vector2 GetRandomPosition()
    {
        Vector2 randPos = new Vector2();
        RaycastHit2D hit;

        do
        {
            int randX = Random.Range(GameExtents.XMin, GameExtents.XMax);
            int randY = Random.Range(GameExtents.YMin, GameExtents.YMax);
            randPos = new Vector2(randX, randY);

            // Ignore own collider
            hit = Physics2D.Raycast(randPos, Vector2.zero, Physics2D.IgnoreRaycastLayer);

        } while (hit.collider != null);

        return randPos;
    }
}
