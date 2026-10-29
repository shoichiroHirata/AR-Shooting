using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public enum MovementType
    {
        Horizontal,
        Vertical,
        Circle,
        Lissajous
    }

    [SerializeField]
    private List<MovementType> movementTypes;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float amplitude = 0.5f;

    [SerializeField]
    private float lissajousA = 2f;

    [SerializeField]
    private float lissajousB = 3f;

    private Vector3 startPosition;

    private MovementType currentMovement;

    private float elapsedTime;

    private void Start()
    {
        startPosition = transform.position;

        currentMovement =
            movementTypes[
                Random.Range(0, movementTypes.Count)
            ];
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime * speed;

        Vector3 offset = Vector3.zero;

        switch (currentMovement)
        {
            case MovementType.Horizontal:
                offset =
                    Vector3.right *
                    Mathf.Sin(t) *
                    amplitude;
                break;

            case MovementType.Vertical:
                offset =
                    Vector3.up *
                    Mathf.Sin(t) *
                    amplitude;
                break;

            case MovementType.Circle:
                offset =
                    new Vector3(
                        Mathf.Cos(t),
                        Mathf.Sin(t),
                        0f
                    ) * amplitude;
                break;

            case MovementType.Lissajous:
                offset =
                    new Vector3(
                        Mathf.Sin(lissajousA * t),
                        Mathf.Sin(lissajousB * t),
                        0f
                    ) * amplitude;
                break;
        }

        transform.position =
            startPosition + offset;
    }
}