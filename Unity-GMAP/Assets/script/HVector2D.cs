using UnityEngine;

public class HVector2D
{
    public float x, y;
    public float h;

    public HVector2D()
    {
        x = 0.0f;
        y = 0.0f;
        h = 1.0f;
    }

    public HVector2D(float _x, float _y)
    {
        x = _x;
        y = _y;
        h = 1.0f;
    }

    public static HVector2D operator + (HVector2D left, HVector2D right)
    {
        HVector2D r = new HVector2D();
        r.x = left.x + right.x;
        r.y = left.y + right.y;
        return r;
    }

    public static HVector2D operator - (HVector2D left, HVector2D right)
    {
        HVector2D r = new HVector2D();
        r.x = left.x - right.x;
        r.y = left.y - right.y;
        return r;
    }

    public static HVector2D operator * (HVector2D left, float scalar)
    {
        HVector2D r = new HVector2D();
        r.x = left.x * scalar;
        r.y = left.y * scalar;
        return r;
    }

    public static HVector2D operator / (HVector2D left, float scalar)
    {
        HVector2D r = new HVector2D();
        r.x = left.x / scalar;
        r.y = left.y / scalar;
        return r;
    }

    public float magnitude()
    {
        float magnitude = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        return magnitude;
    }

    public void normalize()
    {
        HVector2D result = new HVector2D();
        result = this / magnitude();
        x = result.x;
        y = result.y;
    }

    public float dotProduct(HVector2D vec)
    {
        float dotProduct = (x * vec.x + y * vec.y);
        return dotProduct;
    }

    public HVector2D projection(HVector2D vec)
    {
        float fraction = dotProduct(vec) / vec.dotProduct(vec);
        return (vec * fraction);
    }

    public float findAngle(HVector2D vec)
    {
        float angle = Mathf.Rad2Deg * Mathf.Acos(dotProduct(vec) / (vec.magnitude() * magnitude()));
        return angle;
    }

    public void print()
    {
        Debug.Log("HVector2D(" + x + "," + y + ")");
    }
}