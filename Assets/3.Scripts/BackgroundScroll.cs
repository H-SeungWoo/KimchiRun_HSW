using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed of the scrolling")]
    public float scrollSpeed = 0.1f;

    [Header("References")]
    public MeshRenderer meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed *GameManager.instance.CalculateGameSpeed() * Time.deltaTime, 0);
    }
}
