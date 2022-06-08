using UnityEngine;

public static class TransformExtensions
{
    public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
    {
        transform.position = matrix.ExtractPosition();
        transform.rotation = matrix.ExtractRotation();
        transform.localScale = matrix.ExtractScale();
    }
}
