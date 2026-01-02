using UnityEngine;

[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }
    public void Fill(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
    public static implicit operator SerializableVector3(Vector3 vector)
    {
        return new SerializableVector3(vector.x, vector.y, vector.z);
    }
    public static implicit operator Vector3(SerializableVector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }
    public override string ToString()
    {
        return $"{x}, {y}, {z}";
    }
}
