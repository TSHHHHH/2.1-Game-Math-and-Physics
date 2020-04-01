using UnityEngine;

public class HMatrix2D
{
    private float[,] entries = new float[3, 3];

    public HMatrix2D()
    {
    }

    public HMatrix2D(float[,] iArray)
    {
        for (int y = 0; y < 3; y++) // Do for each row
            for (int x = 0; x < 3; x++) // Do for each col
            {
                entries[x, y] = iArray[x, y];
            }
    }

    public HMatrix2D(float m00, float m01, float m02,
                     float m10, float m11, float m12,
                     float m20, float m21, float m22)
    {
        entries[0, 0] = m00;
        entries[0, 1] = m01;
        entries[0, 2] = m02;

        // Second row
        entries[1, 0] = m10;
        entries[1, 1] = m11;
        entries[1, 2] = m12;

        // Third row
        entries[2, 0] = m20;
        entries[2, 1] = m21;
        entries[2, 2] = m22;
    }

    public static HMatrix2D operator +(HMatrix2D a, HMatrix2D b)
    {
        HMatrix2D result = new HMatrix2D();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result.entries[row, col] = a.entries[row, col] + b.entries[row, col];
            }
        }
        return result;
    }

    public static HMatrix2D operator -(HMatrix2D a, HMatrix2D b)
    {
        HMatrix2D result = new HMatrix2D();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result.entries[row, col] = a.entries[row, col] - b.entries[row, col];
            }
        }
        return result;
    }

    public static HMatrix2D operator *(HMatrix2D a, float b)
    {
        HMatrix2D result = new HMatrix2D();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result.entries[row, col] = a.entries[row, col] * b;
            }
        }
        return result;
    }

    public static HVector2D operator *(HMatrix2D a, HVector2D right)
    {
        HVector2D product = new HVector2D();
        product.x = a.entries[0, 0] * right.x + a.entries[0, 1] * right.y + a.entries[0,2] * right.h;
        product.y = a.entries[1, 0] * right.x + a.entries[1, 1] * right.y + a.entries[1,2] * right.h;
        product.h = a.entries[2, 0] * right.x + a.entries[2, 1] * right.y + a.entries[2,2] * right.h;
        return product;
    }

    public static HMatrix2D operator *(HMatrix2D a, HMatrix2D right)
    {
        HMatrix2D result = new HMatrix2D();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result.entries[row, col] = 0;
                for (int i = 0; i < 3; i++)
                {
                    result.entries[row, col] = result.entries[row, col] + a.entries[row, i] * right.entries[i, col];
                }
            }
        }
        return result;
    }

    public static bool operator ==(HMatrix2D a, HMatrix2D right)
    {
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
                if (a.entries[row, col] != right.entries[row, col]) return false;

        return true;
    }

    public static bool operator !=(HMatrix2D a, HMatrix2D right)
    {
        for (int row = 0; row < 2; row++)
            for (int col = 0; col < 3; col++)
                if (a.entries[row, col] == right.entries[row, col]) return false;

        return true;
    }

    public override int GetHashCode()
    {
        // Which is preferred?

        return base.GetHashCode();

        //return this.FooId.GetHashCode();
    }

    public HMatrix2D transpose()
    {
        HMatrix2D result = new HMatrix2D();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result.entries[row, col] = result.entries[col, row];
            }
        }
        return result;
    }

    public float getDeterminant()
    {
        float a = this.entries[0, 0] * this.entries[1, 1] * this.entries[2, 2] + this.entries[0, 1] * this.entries[1, 2] * this.entries[2, 0] + this.entries[0, 2] * this.entries[1, 0] * this.entries[2, 1];
        float b = this.entries[0, 0] * this.entries[1, 2] * this.entries[2, 1] + this.entries[0, 1] * this.entries[1, 0] * this.entries[2, 2] + this.entries[0, 2] * this.entries[1, 1] * this.entries[2, 0];
        return a - b;
    }

    public void setIdentity()
    {
        entries = new float[3, 3];
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                //entries[row,col] = if (row == col) return 1 else return 0);
                entries[row, col] = (row == col ? 1 : 0);
            }
        }
    }

    public void setTranslationMat(float transX, float transY)
    {
        setIdentity();
        entries[0, 2] = transX;
        entries[1, 2] = transY;
    }

    public void setRotationMat(float rotDeg)
    {
        setIdentity();
        entries[0, 0] = Mathf.Cos(rotDeg);
        entries[0, 1] = Mathf.Sin(rotDeg) * -1;
        entries[1, 0] = Mathf.Sin(rotDeg);
        entries[1, 1] = Mathf.Cos(rotDeg);
    }

    public void setScalingMat(float scaleX, float scaleY)
    {
        setIdentity();
        entries[0, 0] = scaleX;
        entries[1, 1] = scaleY;
    }
}