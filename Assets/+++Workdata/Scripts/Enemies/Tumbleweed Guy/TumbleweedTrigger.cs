using System;
using UnityEngine;

public class TumbleweedTrigger : MonoBehaviour
{
    public enum Direction {up, down, left, right, upRight, upLeft, downRight, downLeft}

    public Direction thisDirection;

    public TumbleweedRolling tumbleweedRolling;

    private Vector2 directionVector;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoUp"))
        {
            switch (thisDirection)
            {
                case Direction.down:
                directionVector = Vector2.up;
                break;
                
                case Direction.left:
                    directionVector = Vector2.right;
                    break;
                
                case Direction.right:
                    directionVector = Vector2.left;
                    break;
                
                case Direction.up:
                    directionVector = Vector2.down;
                    break;
                
                case Direction.downLeft:
                    directionVector = new Vector2(1, 1);
                    break;
                
                case Direction.downRight:
                    directionVector = new Vector2(-1, 1);
                    break;
                
                case Direction.upLeft:
                    directionVector = new Vector2(1, -1);
                    break;
                
                case Direction.upRight:
                    directionVector = new Vector2(-1, -1);
                    break;
            }
            tumbleweedRolling.HitWall();
            tumbleweedRolling.SetMovingDirection(directionVector);
        }
    }
}
