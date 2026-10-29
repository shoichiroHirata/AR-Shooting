using UnityEngine;

public class RedTarget : Target
{
    protected override void Start()
    {
        maxHp = 3;
        scoreValue = 3;

        base.Start();
    }
}