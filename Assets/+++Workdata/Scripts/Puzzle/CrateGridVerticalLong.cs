using System;
using UnityEngine;
public class CrateGridVerticalLong : CrateGrid
{ 
    public override void Awake()
    {
        grid = new int[5, 1]
        {
            {1},
            {1}, // start pos
            {1},
            {1},
            {1}
        };

        posX = overrideX;
        posY = overrideY;
        
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
        posX = overrideX;
        posY = overrideY;
        
        gameObject.transform.position = _startVector;
    }
}
