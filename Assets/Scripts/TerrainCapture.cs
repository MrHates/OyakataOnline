using UnityEngine;

public class TerrainCapture : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // SpriteRenderer, na którym chcemy przej¹æ teren
    public Color captureColor1 = new Color(0f, 1f, 0f, 0.5f);  // Kolor przejmowania przez pierwsz¹ frakcjê (pó³przezroczysty zielony)
    public Color captureColor2 = new Color(1f, 0f, 0f, 0.5f);  // Kolor przejmowania przez drug¹ frakcjê (pó³przezroczysty czerwony)
    public float captureSpeed1 = 0.1f;  // Szybkoœæ rozszerzania terenu przez pierwsz¹ frakcjê
    public float captureSpeed2 = 0.1f;  // Szybkoœæ rozszerzania terenu przez drug¹ frakcjê
    public float maxRadius = 10f;  // Maksymalny promieñ przejmowania przez obie frakcje
    public int pixelSize = 4;  // Rozmiar wiêkszych "pikseli", np. 2x2, 3x3

    private Texture2D texture;
    private Texture2D captureLayer1;  // Warstwa przejmowania dla frakcji 1
    private Texture2D captureLayer2;  // Warstwa przejmowania dla frakcji 2
    private Vector2 center1;  // Œrodek przejmowania przez pierwsz¹ frakcjê
    private Vector2 center2;  // Œrodek przejmowania przez drug¹ frakcjê
    private float currentRadius1 = 0f;  // Aktualny promieñ przejmowania przez pierwsz¹ frakcjê
    private float currentRadius2 = 0f;  // Aktualny promieñ przejmowania przez drug¹ frakcjê

    void Start()
    {
        texture = spriteRenderer.sprite.texture;

        // Tworzymy now¹ teksturê na podstawie istniej¹cej tekstury, aby nie nadpisaæ oryginalnej
        captureLayer1 = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        captureLayer2 = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);

        // Ustawiamy filterMode Point (no filter) i wy³¹czamy mipmapy
        captureLayer1.filterMode = FilterMode.Point;
        captureLayer2.filterMode = FilterMode.Point;

        captureLayer1.anisoLevel = 0;  // Wy³¹czamy anizotropowe filtrowanie
        captureLayer2.anisoLevel = 0;

        captureLayer1.mipMapBias = 0f;  // Wy³¹czamy mipmapy
        captureLayer2.mipMapBias = 0f;

        // Wype³niamy warstwy przezroczystym kolorem na pocz¹tku
        FillLayerWithTransparency(captureLayer1);
        FillLayerWithTransparency(captureLayer2);

        center1 = new Vector2(texture.width / 4, texture.height / 4);  // Pocz¹tkowy punkt przejmowania dla frakcji 1
        center2 = new Vector2(3 * texture.width / 4, 3 * texture.height / 4);  // Pocz¹tkowy punkt przejmowania dla frakcji 2

        // Tworzymy sprite z oryginaln¹ tekstur¹, zachowuj¹c pixels per unit
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

        // Nak³adamy obie warstwy na oryginaln¹ teksturê
        ApplyCaptureLayersToSprite();
    }

    void ExpandCaptureZone(Vector2 center, ref float currentRadius, Color captureColor, float captureSpeed, Texture2D captureLayer, Texture2D enemyLayer)
    {
        // Obliczamy promieñ przejmowania, który siê rozszerza
        currentRadius += captureSpeed * Time.deltaTime;

        // Modyfikujemy piksele w obszarze przejmowania
        for (int x = 0; x < texture.width; x += pixelSize)
        {
            for (int y = 0; y < texture.height; y += pixelSize)
            {
                Vector2 pixelPosition = new Vector2(x, y);
                float distance = Vector2.Distance(pixelPosition, center);

                // Sprawdzamy, czy piksel mieœci siê w okrêgu przejmowania
                if (distance <= currentRadius)
                {
                    // Sprawdzamy, czy piksel nie jest ju¿ kontrolowany przez wroga
                    if (IsControlledByEnemy(x, y, enemyLayer))
                    {
                        continue;  // Jeœli jest kontrolowany przez wroga, pomijamy ten piksel
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

        // Jeœli piksel jest nieprzezroczysty (a wiêc kontrolowany przez wroga), zwrócimy true
        return enemyColor.a > 0.1f;  // Mo¿emy dostosowaæ próg, jeœli jest potrzeba wiêkszej dok³adnoœci
    }

    void ApplyCaptureLayersToSprite()
    {
        // Tworzymy now¹ teksturê, która ³¹czy oryginaln¹ teksturê z dwoma warstwami przejmowania
        Texture2D finalTexture = new Texture2D(texture.width, texture.height);

        // £¹czymy warstwê z oryginalnym obrazem
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color baseColor = texture.GetPixel(x, y);
                Color captureColor1 = captureLayer1.GetPixel(x, y);
                Color captureColor2 = captureLayer2.GetPixel(x, y);

                // £¹czymy kolory z obu warstw (dodajemy je, aby uzyskaæ efekt przejmowania)
                Color finalColor = Color.Lerp(baseColor, captureColor1, captureColor1.a);
                finalColor = Color.Lerp(finalColor, captureColor2, captureColor2.a);

                finalTexture.SetPixel(x, y, finalColor);
            }
        }

        finalTexture.Apply();

        // Ustawiamy now¹ teksturê na SpriteRenderer, zachowuj¹c Pixels Per Unit oraz Point Filter
        spriteRenderer.sprite = Sprite.Create(finalTexture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);

        // Upewniamy siê, ¿e tekstura wci¹¿ ma ustawienia filterMode jako Point
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
