using UnityEngine;

public class GreenTarget : Target
{
    protected override void Start()
    {
        maxHp = 1;
        scoreValue = 0;

        base.Start();
    }
}