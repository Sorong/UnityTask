using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveWalker : MonoBehaviour
{    
    public List<Vector3> ControlPoints { get; set;}

    public float CurrentInterpolation { get; private set; }

    public bool StartCurveWalk { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        CurrentInterpolation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCurveWalk)
        {
            this.transform.Rotate(new Vector3(-5, -5, 0));
            if (CurrentInterpolation >= 1)
            {
                CurrentInterpolation = 1f;
                this.StartCurveWalk = false;
            }
            this.transform.localPosition = Evaluate(CurrentInterpolation);
            
            CurrentInterpolation += 0.01f;

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
}
