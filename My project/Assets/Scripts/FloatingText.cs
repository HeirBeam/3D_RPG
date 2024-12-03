using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float fadeDuration = 1f;

    private TextMesh textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        Destroy(gameObject, fadeDuration);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        Color color = textMesh.color;
        color.a -= Time.deltaTime / fadeDuration;
        textMesh.color = color;
    }
}
