using UnityEngine;

[System.Serializable]
public struct RGB
{
    float r;
    float g;
    float b;
    float a;

    public RGB(float red, float green, float blue, float alpha)
    {
        r = red;
        g = green;
        b = blue;
        a = alpha;
    }
    public RGB(float red, float green, float blue): this(red, green, blue, 255)
    {
    }
    public static implicit operator RGB(Color color)
    {
        return new RGB(color.r, color.g, color.b, color.a);
    }
    public static implicit operator Color(RGB color)
    {
        return new Color(color.r, color.g, color.b, color.a);
    }
    public override string ToString()
    {
        return $"({r}, {g}, {b}, {a})";
    }
}
