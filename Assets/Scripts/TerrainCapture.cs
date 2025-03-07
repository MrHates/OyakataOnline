using UnityEngine;

public class TerrainCapture : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // SpriteRenderer, na kt�rym chcemy przej�� teren
    public Color captureColor1 = new Color(0f, 1f, 0f, 0.5f);  // Kolor przejmowania przez pierwsz� frakcj� (p�przezroczysty zielony)
    public Color captureColor2 = new Color(1f, 0f, 0f, 0.5f);  // Kolor przejmowania przez drug� frakcj� (p�przezroczysty czerwony)
    public float captureSpeed1 = 0.1f;  // Szybko�� rozszerzania terenu przez pierwsz� frakcj�
    public float captureSpeed2 = 0.1f;  // Szybko�� rozszerzania terenu przez drug� frakcj�
    public float maxRadius = 10f;  // Maksymalny promie� przejmowania przez obie frakcje
    public int pixelSize = 4;  // Rozmiar wi�kszych "pikseli", np. 2x2, 3x3

    private Texture2D texture;
    private Texture2D captureLayer1;  // Warstwa przejmowania dla frakcji 1
    private Texture2D captureLayer2;  // Warstwa przejmowania dla frakcji 2
    private Vector2 center1;  // �rodek przejmowania przez pierwsz� frakcj�
    private Vector2 center2;  // �rodek przejmowania przez drug� frakcj�
    private float currentRadius1 = 0f;  // Aktualny promie� przejmowania przez pierwsz� frakcj�
    private float currentRadius2 = 0f;  // Aktualny promie� przejmowania przez drug� frakcj�

    void Start()
    {
        texture = spriteRenderer.sprite.texture;

        // Tworzymy now� tekstur� na podstawie istniej�cej tekstury, aby nie nadpisa� oryginalnej
        captureLayer1 = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        captureLayer2 = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);

        // Ustawiamy filterMode Point (no filter) i wy��czamy mipmapy
        captureLayer1.filterMode = FilterMode.Point;
        captureLayer2.filterMode = FilterMode.Point;

        captureLayer1.anisoLevel = 0;  // Wy��czamy anizotropowe filtrowanie
        captureLayer2.anisoLevel = 0;

        captureLayer1.mipMapBias = 0f;  // Wy��czamy mipmapy
        captureLayer2.mipMapBias = 0f;

        // Wype�niamy warstwy przezroczystym kolorem na pocz�tku
        FillLayerWithTransparency(captureLayer1);
        FillLayerWithTransparency(captureLayer2);

        center1 = new Vector2(texture.width / 4, texture.height / 4);  // Pocz�tkowy punkt przejmowania dla frakcji 1
        center2 = new Vector2(3 * texture.width / 4, 3 * texture.height / 4);  // Pocz�tkowy punkt przejmowania dla frakcji 2

        // Tworzymy sprite z oryginaln� tekstur�, zachowuj�c pixels per unit
        spriteRenderer.sprite = Sprite.Create(texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);

        // Ustawiamy odpowiednie filtry dla sprite'a - bez rozmycia
        spriteRenderer.sprite.texture.filterMode = FilterMode.Point;
        spriteRenderer.sprite.texture.anisoLevel = 0;
    }

    void Update()
    {
        if (currentRadius1 < maxRadius)
        {
            ExpandCaptureZone(center1, ref currentRadius1, captureColor1, captureSpeed1, captureLayer1, captureLayer2);
        }

        if (currentRadius2 < maxRadius)
        {
            ExpandCaptureZone(center2, ref currentRadius2, captureColor2, captureSpeed2, captureLayer2, captureLayer1);
        }

        // Nak�adamy obie warstwy na oryginaln� tekstur�
        ApplyCaptureLayersToSprite();
    }

    void ExpandCaptureZone(Vector2 center, ref float currentRadius, Color captureColor, float captureSpeed, Texture2D captureLayer, Texture2D enemyLayer)
    {
        // Obliczamy promie� przejmowania, kt�ry si� rozszerza
        currentRadius += captureSpeed * Time.deltaTime;

        // Modyfikujemy piksele w obszarze przejmowania
        for (int x = 0; x < texture.width; x += pixelSize)
        {
            for (int y = 0; y < texture.height; y += pixelSize)
            {
                Vector2 pixelPosition = new Vector2(x, y);
                float distance = Vector2.Distance(pixelPosition, center);

                // Sprawdzamy, czy piksel mie�ci si� w okr�gu przejmowania
                if (distance <= currentRadius)
                {
                    // Sprawdzamy, czy piksel nie jest ju� kontrolowany przez wroga
                    if (IsControlledByEnemy(x, y, enemyLayer))
                    {
                        continue;  // Je�li jest kontrolowany przez wroga, pomijamy ten piksel
                    }

                    // Zmieniamy kolor piksela na odpowiedni dla frakcji
                    for (int i = 0; i < pixelSize; i++)
                    {
                        for (int j = 0; j < pixelSize; j++)
                        {
                            int xPos = Mathf.Min(x + i, texture.width - 1);
                            int yPos = Mathf.Min(y + j, texture.height - 1);
                            captureLayer.SetPixel(xPos, yPos, captureColor);
                        }
                    }
                }
            }
        }

        captureLayer.Apply();
    }

    bool IsControlledByEnemy(int x, int y, Texture2D enemyLayer)
    {
        // Sprawdzamy kolor piksela na warstwie wroga
        Color enemyColor = enemyLayer.GetPixel(x, y);

        // Je�li piksel jest nieprzezroczysty (a wi�c kontrolowany przez wroga), zwr�cimy true
        return enemyColor.a > 0.1f;  // Mo�emy dostosowa� pr�g, je�li jest potrzeba wi�kszej dok�adno�ci
    }

    void ApplyCaptureLayersToSprite()
    {
        // Tworzymy now� tekstur�, kt�ra ��czy oryginaln� tekstur� z dwoma warstwami przejmowania
        Texture2D finalTexture = new Texture2D(texture.width, texture.height);

        // ��czymy warstw� z oryginalnym obrazem
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color baseColor = texture.GetPixel(x, y);
                Color captureColor1 = captureLayer1.GetPixel(x, y);
                Color captureColor2 = captureLayer2.GetPixel(x, y);

                // ��czymy kolory z obu warstw (dodajemy je, aby uzyska� efekt przejmowania)
                Color finalColor = Color.Lerp(baseColor, captureColor1, captureColor1.a);
                finalColor = Color.Lerp(finalColor, captureColor2, captureColor2.a);

                finalTexture.SetPixel(x, y, finalColor);
            }
        }

        finalTexture.Apply();

        // Ustawiamy now� tekstur� na SpriteRenderer, zachowuj�c Pixels Per Unit oraz Point Filter
        spriteRenderer.sprite = Sprite.Create(finalTexture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);

        // Upewniamy si�, �e tekstura wci�� ma ustawienia filterMode jako Point
        spriteRenderer.sprite.texture.filterMode = FilterMode.Point;
        spriteRenderer.sprite.texture.anisoLevel = 0;
    }

    void FillLayerWithTransparency(Texture2D layer)
    {
        Color[] transparentPixels = new Color[layer.width * layer.height];
        for (int i = 0; i < transparentPixels.Length; i++)
        {
            transparentPixels[i] = new Color(0f, 0f, 0f, 0f);  // Przezroczysty kolor
        }
        layer.SetPixels(transparentPixels);
        layer.Apply();
    }
}
