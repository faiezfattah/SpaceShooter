using System;
/// <summary>
/// nice things to have when dealing with randomizations.
/// can be set on the inspector
/// </summary>
[Serializable]
class MinMax {
    public int Min;
    public int Max;
    public MinMax(int min, int max) {
        Min = min;
        Max = max;
    }
    public int GetRandom() {
        return UnityEngine.Random.Range(Min, Max);
    }
}

/// <summary>
/// <inheritdoc cref="MinMax"/>
/// </summary>
[Serializable]
class MinMaxFloat {
    public float Min;
    public float Max;
    public MinMaxFloat(float min, float max) {
        Min = min;
        Max = max;
    }
    public float GetRandom() {
        return UnityEngine.Random.Range(Min, Max);
    }
}