using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureAnalyzer : MonoBehaviour {
    public const int CLUSTERIZED_DOTS_COUNT = 25; //кол-во точек для кластеризованной фигуры
    public const float MAX_DELTA_DISTANCE = 0.12f; //максимальный радиус отклонения точки
    public const float MIN_DOTES_PERCENT = 0.85f;//процент точек, которые должны совпасть
    public const int DISTANSE_Z = 0;

    /// <summary>
    /// Нормализует и центрирует исходный набор точек фигуры
    /// </summary>
    public static List<Vector2> NormalizeDotsCoordinates(List<Vector2> _figureDots)
    {
        Vector2 _massCenter = new Vector2(0, 0);
        foreach (var _dot in _figureDots)
        {
            _massCenter += _dot;
        }
        _massCenter /= _figureDots.Count;
        for (int i = 0; i < _figureDots.Count; i++)
        {
            _figureDots[i] -= _massCenter;
        }

        float _maxCoordValue = 0;
        foreach (var _dot in _figureDots)
        {
            if (Mathf.Abs(_dot.x) > _maxCoordValue)
            {
                _maxCoordValue = Mathf.Abs(_dot.x);
            }
            else if (Mathf.Abs(_dot.y) > _maxCoordValue)
            {
                _maxCoordValue = Mathf.Abs(_dot.y);
            }
        }

        for (int i = 0; i < _figureDots.Count; i++)
        {
            _figureDots[i] /= _maxCoordValue;
        }

        return _figureDots;
    }


    /// <summary>
    /// Кластеризует нормализованные точки фигуры, возвращая CLUSTERIZED_DOTS_COUNT центров кластеров
    /// </summary>
    public static List<Vector2> ClusterizeSortedDots(List<Vector2> _figureDots) {
        List<Vector2> _clusterizedDots = new List<Vector2>();

        _clusterizedDots.Add(_figureDots[0]);
        int clusterSize = _figureDots.Count / CLUSTERIZED_DOTS_COUNT;
        for (int i = 0; i < CLUSTERIZED_DOTS_COUNT ; i++)
        {
            Vector2 _currResult = new Vector2(0f, 0f);
            foreach (var _dot in _figureDots.GetRange(i * clusterSize, clusterSize))
            {
                _currResult += _dot;
            }
            _currResult /= clusterSize;
            _clusterizedDots.Add(_currResult);
        }
        _clusterizedDots.Add(_figureDots[_figureDots.Count-1]);
        return _clusterizedDots;
    }

    public static List<Vector2> ComplexClusterizeProcess(List<Vector2> _figureDots)
    {
        return ClusterizeSortedDots(NormalizeDotsCoordinates(_figureDots));
    }

    public static bool CompareFigures(List<Vector2> _figureDotsControl, List<Vector2> _figureDotDrawed)
    {
        List<Vector2> _clusterizedSecondFigure = ComplexClusterizeProcess(_figureDotDrawed);
        int _matchCount=0;
        
        for (int i = 0; i < _figureDotsControl.Count; i++)
        {
            for (int j = 0; j < _clusterizedSecondFigure.Count; j++)
            {
              //    Debug.Log((_figureDotsControl[i] - _clusterizedSecondFigure[j]).sqrMagnitude);
                if ((_figureDotsControl[i] - _clusterizedSecondFigure[j]).sqrMagnitude < MAX_DELTA_DISTANCE)
                {
                    _matchCount++;
                    break;
                }
            }
        }
        Debug.Log(_matchCount);
        return _matchCount>=CLUSTERIZED_DOTS_COUNT*MIN_DOTES_PERCENT;
    }

    public static List<Vector3> Vector2ToVector3(List<Vector2> _figureDots)
    {
        List<Vector3> _outList = new List<Vector3>();
        foreach (var _dot in _figureDots)
        {
            _outList.Add(new Vector3(_dot.x, _dot.y, DISTANSE_Z));
        }
        return _outList;
    }
    public static List<Vector2> Vector3ToVector2(List<Vector3> _figureDots)
    {
        List<Vector2> _outList = new List<Vector2>();
        foreach (var _dot in _figureDots)
        {
            _outList.Add(new Vector2(_dot.x, _dot.y));
        }
        return _outList;
    }
}
