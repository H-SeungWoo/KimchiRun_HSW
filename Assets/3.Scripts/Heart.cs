using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;

    public SpriteRenderer spriteRenderer;

    public int LiveNum;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.Lives >= LiveNum)
        {
            spriteRenderer.sprite = OnHeart;
        }
        else
        {
            spriteRenderer.sprite = OffHeart;
        }
    }
}
