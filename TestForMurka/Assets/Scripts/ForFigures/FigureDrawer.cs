using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureDrawer : MonoBehaviour {
    public bool clearWhenMouseUp = true;

    [SerializeField]private TrailRenderer _trailRenderer;

    void Start () {
        InputManager.Current.onDrawingUpdate += drawingStep;
        if (clearWhenMouseUp) {
            InputManager.Current.onDrawingEnd += clearTrail;

        }
        InputManager.Current.onDrawingStart += drawStart;
    }

    private void drawingStep(Vector2 _newDot) {
        transform.position = _newDot;
      //  Debug.Log("_newDot: " + _newDot + "_trailRenderer: " + _trailRenderer.GetPosition(_trailRenderer.positionCount-1));
    }
    private void clearTrail(List<Vector2> _dotes) {
        _trailRenderer.Clear();
    }
    private void drawStart(Vector2 _newDot)
    {
        transform.position = _newDot;
        _trailRenderer.Clear();
    }
}
