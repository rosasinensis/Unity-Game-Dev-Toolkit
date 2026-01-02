using UnityEngine;

[System.Serializable]
public struct SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;
    public SerializableQuaternion(float rX, float rY, float rZ, float rW)
    {
        this.x = rX;
        this.y = rY;
        this.z = rZ;
        this.w = rW;
    }
    public void Fill(Quaternion quaternion)
    {
        this.x = quaternion.x;
        this.y = quaternion.y;
        this.z = quaternion.z;
        this.w = quaternion.w;
    }
    public static implicit operator SerializableQuaternion(Quaternion quaternion)
    {
        return new SerializableQuaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }
    public static implicit operator Quaternion(SerializableQuaternion quaternion)
    {
        return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
    }
    public override string ToString()
    {
        return $"{x}, {y}, {z}, {w}";
    }
}
