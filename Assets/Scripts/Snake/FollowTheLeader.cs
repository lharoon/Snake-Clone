using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public GameObject leader;

    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = leader.transform.position;
    }

    public void UpdateTransform()
    {
        #region UpdatePosition
        transform.position = nextPosition;
        //transform.rotation = leader.transform.rotation;

        if (leader == null) Debug.LogError("leader is null!");
        else
            nextPosition = leader.transform.position;
        #endregion

        // Update layer so that snake head can collide with body
        gameObject.layer = 0; // Spawns @ "Spawn"
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}