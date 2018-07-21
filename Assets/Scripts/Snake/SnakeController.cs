using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float tick = 0.3f;
    public bool isContinuous = false;
    public float speed = 5f;

    public SnakeHealth snakeHealth; // Could alternatively check game over

    enum Dir { Up, Down, Left, Right, None };
    private Dir reqDir = Dir.None;
    private Vector2 direction = Vector2.zero;

    private bool wrap = false;
    private Vector2 newPosOnCol = Vector2.zero;
    private string boundaryId = "";

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (!isContinuous)
            StartCoroutine(MoveDiscreetly());
    }

    private void Update()
    {
        ChangeDir();

    }

    private void FixedUpdate()
    {
        if (!isContinuous) return;

        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    IEnumerator MoveDiscreetly()
    {
        const int faceUp = 0, faceDown = 180, faceRight = 90, faceLeft = -90;

        while (snakeHealth.Health > 0)
        {
            #region Move head
            switch (reqDir)
            {
                case Dir.Up:
                    direction = Vector2.up;
                    transform.rotation = Quaternion.AngleAxis(faceUp, Vector3.forward);
                    break;
                case Dir.Down:
                    direction = Vector2.down;
                    transform.rotation = Quaternion.AngleAxis(faceDown, Vector3.forward);
                    break;
                case Dir.Left:
                    direction = Vector2.left;
                    transform.rotation = Quaternion.AngleAxis(faceRight, Vector3.forward);
                    break;
                case Dir.Right:
                    direction = Vector2.right;
                    transform.rotation = Quaternion.AngleAxis(faceLeft, Vector3.forward);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(boundaryId))
                CheckWrap();

            if (!wrap)
                transform.position = (Vector2)transform.position + direction;
            else
            {
                transform.position = newPosOnCol;
                wrap = false;
            }
            #endregion

            #region Move body
            foreach (Transform bodyPart in transform.parent)
            {
                if (bodyPart.tag == "bodypart")
                    bodyPart.GetComponent<FollowTheLeader>().UpdateTransform();
            }
            #endregion

            yield return new WaitForSeconds(tick);
        }
    }

    void ChangeDir()
    {
        if ((Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            && direction != Vector2.down) // Can't move in opposite direction
            reqDir = Dir.Up;
        else if ((Input.GetKeyDown("down") || Input.GetKeyDown("s"))
                 && direction != Vector2.up)
            reqDir = Dir.Down;
        else if ((Input.GetKeyDown("left") || Input.GetKeyDown("a"))
                 && direction != Vector2.right)
            reqDir = Dir.Left;
        else if ((Input.GetKeyDown("right") || Input.GetKeyDown("d"))
                 && direction != Vector2.left)
            reqDir = Dir.Right;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Boundary") return;
        boundaryId = collision.gameObject.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("Exiting " + boundaryId);
        boundaryId = ""; // Reset
    }

    void CheckWrap()
    {
        // Sides
        if (boundaryId == "NorthBounds" && direction == Vector2.up)
            WrapVertical(GameExtents.YMin);
        else if (boundaryId == "SouthBounds" && direction == Vector2.down)
            WrapVertical(GameExtents.YMax);
        else if (boundaryId == "WestBounds" && direction == Vector2.left)
            WrapHorizontal(GameExtents.XMax);
        else if (boundaryId == "EastBounds" && direction == Vector2.right)
            WrapHorizontal(GameExtents.XMin);
        // Corners
        else if (boundaryId == "NWBounds")
        {
            if (direction == Vector2.up) WrapVertical(GameExtents.YMin);
            else if (direction == Vector2.left) WrapHorizontal(GameExtents.YMax);
        }
        else if (boundaryId == "NEBounds")
        {
            if (direction == Vector2.up) WrapVertical(GameExtents.YMin);
            else if (direction == Vector2.right) WrapHorizontal(GameExtents.XMin);
        }
        else if (boundaryId == "SEBounds")
        {
            if (direction == Vector2.down) WrapVertical(GameExtents.YMax);
            else if (direction == Vector2.right) WrapHorizontal(GameExtents.XMin);
        }
        else if (boundaryId == "SWBounds")
        {
            if (direction == Vector2.down) WrapVertical(GameExtents.YMax);
            else if (direction == Vector2.left) WrapHorizontal(GameExtents.XMax);
        }
    }

    void WrapVertical(float newY)
    {
        newPosOnCol = new Vector2(transform.position.x, newY);
        wrap = true;
    }

    void WrapHorizontal(float newX)
    {
        newPosOnCol = new Vector2(newX, transform.position.y);
        wrap = true;
    }
}
