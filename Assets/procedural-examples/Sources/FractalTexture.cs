// This script is placed in public domain. The author takes no responsibility for any possible harm.
using UnityEngine;

public class FractalTexture : MonoBehaviour
{
    bool gray = true;
    int width = 128;
    int height = 128;

    float lacunarity = 6.18f;
    float h = 0.69f;
    float octaves = 8.379f;
    float offset = 0.75f;
    float scale = 0.09f;

    float offsetPos = 0.0f;

    private Texture2D texture;
    private  Perlin perlin;
    private FractalNoise fractal;

    void Start ()
    {
        texture = new Texture2D (width, height, TextureFormat.RGB24, false);
        GetComponent<Renderer> ().material.mainTexture = texture;

        if (perlin == null)
            perlin = new Perlin ();
        fractal = new FractalNoise (h, lacunarity, octaves, perlin);
    }

    void Update ()
    {
        Calculate ();
    }

    void Calculate ()
    {
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (gray) {
                    var value = fractal.HybridMultifractal (x * scale + Time.time, y * scale + Time.time, offset);
                    texture.SetPixel (x, y, new Color (value, value, value, value));
                } else {
                    offsetPos = Time.time;
                    var valuex = fractal.HybridMultifractal (x * scale + offsetPos * 0.6f, y * scale + offsetPos * 0.6f, offset);
                    var valuey = fractal.HybridMultifractal (x * scale + 161.7f + offsetPos * 0.2f, y * scale + 161.7f + offsetPos * 0.3f, offset);
                    var valuez = fractal.HybridMultifractal (x * scale + 591.1f + offsetPos, y * scale + 591.1f + offsetPos * 0.1f, offset);

                    texture.SetPixel (x, y, new Color (valuex, valuey, valuez, 1));
                }
            }
        }
        texture.Apply ();
    }
}