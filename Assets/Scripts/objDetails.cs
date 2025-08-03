using UnityEngine;

public class objDetails : MonoBehaviour
{
    public enum ObjType
    {
        bardak,
        ayi,
        foto,
        Computer,
        oyuncak,
        kupa

    }

    // the current type of this object (set via triggers)
    public ObjType objectType;
}
