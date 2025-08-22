using UnityEngine;
using UnityEngine.UI;

public class StackElementVisual : MonoBehaviour
{
    private Image imageComponent;
    private RectTransform rectTransform;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(200, 200); // Tamaño del elemento
        }
    }

    public void SetSprite(Sprite sprite)
    {
        if (imageComponent != null)
        {
            imageComponent.sprite = sprite;
        }
    }

    public void SetPosition(Vector2 position)
    {
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = position;
        }
    }
}
