using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    [SerializeField]
    private Texture2D _tex;

    [SerializeField]
    [Range(2, 512)]
    private int _resolution = 256;

    [SerializeField]
    private float _frequency = 1f;

    [SerializeField]
    [Range(1, 8)]
    private int _octaves = 1;

    [SerializeField]
    [Range(1f, 4f)]
    private float _lacunarity = 2f;

    [SerializeField]
    [Range(0f, 1f)]
    private float _persistence = 0.5f;

    [SerializeField]
    private Gradient _gradient;



    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate()
    {
        //if (_tex == null)
        //{
        _tex = new Texture2D(_resolution, _resolution, TextureFormat.RGB24, true)
        {
            name = "Texture" + this.gameObject.name,
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Trilinear,
            anisoLevel = 9
        };
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial.mainTexture = _tex;
        }
        //}

        CreateTexture();
    }

    private void CreateTexture()
    {
        if (_tex.width != _resolution)
        {
            _tex.Resize(_resolution, _resolution);
        }

        float stepSize = 1f / _resolution;
        for (int y = 0; y < _resolution; y++)
        {
            for (int x = 0; x < _resolution; x++)
            {
                float amplitude = 1f;
                float range = 1f;
                float frequency = _frequency;
                float sample = 0;
                for (int o = 0; o < _octaves; o++)
                {
                    frequency *= _lacunarity;
                    amplitude *= _persistence;
                    range += amplitude;

                    sample += Mathf.PerlinNoise(x * stepSize * frequency, y * stepSize * frequency) * amplitude;
                }
                sample /= range;
                Color color = _gradient != null ? _gradient.Evaluate(sample) : Color.Lerp(Color.white, Color.black, sample);
                _tex.SetPixel(x, y, color);
            }
        }

        _tex.Apply();
    }
}
