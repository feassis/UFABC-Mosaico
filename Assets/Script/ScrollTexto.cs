using UnityEngine;

public class ScrollTexto : MonoBehaviour
{
    public float scrollSpeed = 50.0f;


    void Update()
    {
        Vector3 pos = transform.position;

        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

        pos += localVectorUp * scrollSpeed * Time.deltaTime;

        transform.position = pos;
    }
}
