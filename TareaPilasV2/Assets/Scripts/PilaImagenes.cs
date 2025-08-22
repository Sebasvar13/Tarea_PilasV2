using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class StackManager : MonoBehaviour
{
    
    public Button pushButton;
    public Button popButton;
    public Button peekButton;
    public Button clearButton;
    public TextMeshProUGUI infoText;

    
    public Transform stackContainer; 
    public GameObject imagePrefab; 
    public float stackSpacing = 10f; 

   
    public string[] imageNames = { "foto1" }; 

    
    private Stack<StackElement> stack = new Stack<StackElement>();
    private List<GameObject> visualElements = new List<GameObject>();
    private List<Sprite> loadedSprites = new List<Sprite>();
    private int pushCounter = 0;

    void Start()
    {
        
        StartCoroutine(LoadImagesFromStreamingAssets());

        
        pushButton.onClick.AddListener(Push);
        popButton.onClick.AddListener(Pop);
        peekButton.onClick.AddListener(Peek);
        clearButton.onClick.AddListener(Clear);

        
        UpdateInfoText("Cargando imágenes...");
    }

    public void Push()
    {
        if (loadedSprites.Count == 0)
        {
            UpdateInfoText("No hay imágenes cargadas disponibles");
            return;
        }

        
        int imageIndex = Random.Range(0, loadedSprites.Count);
        StackElement element = new StackElement
        {
            id = pushCounter,
            imageIndex = imageIndex,
            sprite = loadedSprites[imageIndex]
        };

        
        stack.Push(element);

        
        CreateVisualElement(element);

        
        UpdateInfoText($"Push: {imageNames[imageIndex % imageNames.Length]}_{pushCounter} (tipo={imageIndex})");
        pushCounter++;
    }

    public void Pop()
    {
        if (stack.Count == 0)
        {
            UpdateInfoText("La pila está vacía");
            return;
        }

        
        StackElement poppedElement = stack.Pop();

        
        if (visualElements.Count > 0)
        {
            GameObject lastElement = visualElements[visualElements.Count - 1];
            visualElements.RemoveAt(visualElements.Count - 1);
            Destroy(lastElement);
        }

        
        if (stack.Count > 0)
        {
            StackElement topElement = stack.Peek();
            UpdateInfoText($"Pop: imagen_{poppedElement.id} removida. Top: imagen_{topElement.id}");
        }
        else
        {
            UpdateInfoText($"Pop: imagen_{poppedElement.id} removida. Pila vacía");
        }
    }

    public void Peek()
    {
        if (stack.Count == 0)
        {
            UpdateInfoText("La pila está vacía");
            return;
        }

        StackElement topElement = stack.Peek();
        UpdateInfoText($"Peek: imagen_{topElement.id} (tipo={topElement.imageIndex}) - Total: {stack.Count}");
    }

    public void Clear()
    {
        
        stack.Clear();

        
        foreach (GameObject element in visualElements)
        {
            Destroy(element);
        }
        visualElements.Clear();

        
        pushCounter = 0;

        UpdateInfoText("Pila limpiada");
    }

    private IEnumerator LoadImagesFromStreamingAssets()
    {
        foreach (string imageName in imageNames)
        {
            string[] extensions = { ".png", ".jpg", ".jpeg" };

            foreach (string ext in extensions)
            {
                string fileName = imageName + ext;
                string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

                if (File.Exists(filePath))
                {
                    
                    byte[] fileData = File.ReadAllBytes(filePath);

                    
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);

                    
                    Sprite sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f));
                    sprite.name = imageName;

                    loadedSprites.Add(sprite);
                    Debug.Log($"Imagen cargada: {fileName}");
                    break; 
                }
            }
        }

        if (loadedSprites.Count > 0)
        {
            UpdateInfoText("Pila vacía - Imágenes cargadas");
        }
        else
        {
            UpdateInfoText("Error: No se encontraron imágenes");
        }

        yield return null;
    }

    private void CreateVisualElement(StackElement element)
    {
        
        GameObject newElement = Instantiate(imagePrefab, stackContainer);

        
        Image imageComponent = newElement.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = element.sprite;
        }

       
        RectTransform rectTransform = newElement.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            float yPosition = visualElements.Count * stackSpacing;
            rectTransform.anchoredPosition = new Vector2(0, yPosition);
        }

        
        visualElements.Add(newElement);
    }

    private void UpdateInfoText(string text)
    {
        if (infoText != null)
        {
            infoText.text = text;
        }
    }
}

[System.Serializable]
public class StackElement
{
    public int id;
    public int imageIndex;
    public Sprite sprite;
}