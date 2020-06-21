using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveWalker : MonoBehaviour
{
    private const float StepSize = 0.005f;
    private const int Rotations = 1;
    public List<Vector3> ControlPoints { get; set;}

    public float CurrentInterpolation { get; private set; }

    public ParticleSystem ParticleSystem { get; private set; }

    public AudioSource AudioSource { get; private set; }
    public AudioClip AudioClip { get; private set; }

    public bool IsStarted { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentInterpolation = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsStarted)
        {
            var rotAngle = Rotations *  360 * StepSize;
            
            ParticleSystem.transform.Rotate(new Vector3(rotAngle, rotAngle, 0));
            if (CurrentInterpolation >= 1)
            {
                CurrentInterpolation = 1f;
                IsStarted = false;
                ParticleSystem.Stop();
            }
            else
            {
                transform.Rotate(-rotAngle, 0f, 0f, Space.Self);
                transform.Rotate(0f, -rotAngle, 0f, Space.World);
                
            }
            transform.localPosition = Evaluate(CurrentInterpolation);

            CurrentInterpolation += StepSize;

        }
    }

    private Vector3 Evaluate(float t)
    {
        int n = ControlPoints.Count - 1;
        List<float> bernsteinPolynoms = new List<float>();
        Vector3 point = Vector3.zero;
        for (int k = 0; k <= n; k++)
        {
            float polynom = BinomialCoefficient(n, k) * Mathf.Pow(t, k) * Mathf.Pow(1 - t, n - k);
            bernsteinPolynoms.Add(polynom);
        }

        for (int j = 0; j <= n; j++)
        {
            point += ControlPoints[j] * bernsteinPolynoms[j];
        }

        return point;
    }

    private float BinomialCoefficient(int n, int k)
    {
        if (n < k) { return 0; }

        var denominator = (Factorial(n - k) * Factorial(k));
        if (denominator <= 0)
        {
            return 0;
        }

        return Factorial(n) / denominator;
    }

    private float Factorial(int n)
    {
        if (n <= 1) { return 1; }

        return n * Factorial(n - 1);
    }

    public void StartWalk(ParticleSystem particleSystem, AudioSource audioSource, AudioClip clip)
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(transform);
            particleSystem.transform.localPosition = Vector3.zero;
            particleSystem.transform.localScale = Vector3.one * 0.5f;
            ParticleSystem = particleSystem;
            //particleSystem.Play();
        }
        IsStarted = true;
        if (clip != null && audioSource != null)
        {
            //audioSource.PlayOneShot(clip);
            audioSource.PlayOneShot(clip, 0.2f);
        }

        AudioSource = audioSource;
    }
}
