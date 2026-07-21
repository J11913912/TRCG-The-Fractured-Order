using System;
using UnityEngine;
public class CrateGridLLayout : CrateGrid
{
    public override void Awake()
    {
        grid = new int[4, 3]
        {
            {1, 0, 0},
            {1, 0, 0},
            {1, 0, 0},
            {1, 1, 1}
                // start pos
        };

        posX = 1;
        posY = 3;
        
        maxX = grid.GetLength(1);
        maxY = grid.GetLength(0);
        
        _startVector = _startPosition.position;
    }

    public override void RemoveBarrier()
    {
        grid[0, 4] = 1;
        grid[0, 5] = 1;
    }

    public override void LockCrate()
    {
        grid[0, 5] = 0;
    }
    
    public override void Reset()
    {
        posX = 1;
        posY = 2;
        
        gameObject.transform.position = _startVector;
    }
}