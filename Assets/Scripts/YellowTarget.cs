using UnityEngine;

public class YellowTarget : Target
{
    protected override void Start()
    {
        maxHp = 1;
        scoreValue = 1;

        base.Start();
    }
}