using System;
using UnityEngine;
public class CrateGridHorizontalShort : CrateGrid
{
    public override void Awake()
    {
        grid = new int[1, 3]
        {
            {1, 1, 1}
            // start pos
        };

        posX = 0;
        posY = 0;
        
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
        posX = 0;
        posY = 0;
        
        gameObject.transform.position = _startVector;
    }
}