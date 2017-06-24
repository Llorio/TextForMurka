using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurePreviev : MonoBehaviour {
    [SerializeField] private LineRenderer _lineRenderer;

    public void updateFigure(List<Vector3> _figureDots)
    {
        _lineRenderer.positionCount = _figureDots.Count;
        _lineRenderer.SetPositions(_figureDots.ToArray());
    }
    public void clear()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.SetPositions(new Vector3[] { });
    }
}
