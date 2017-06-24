using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : ManagerBase<InputManager> {

    public bool drawInputAllowed = true;

    [Header ("Actions")]
    public UnityAction<List<Vector2>> onDrawingEnd = null;
    public UnityAction<Vector2> onDrawingStart = null;
    public UnityAction<Vector2> onDrawingUpdate = null;

    private List<Vector2> _currentFigureDotes = new List<Vector2>();
    private bool _drawing = false;
    private Coroutine _drawingCoroutine;


	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && drawInputAllowed && !EventSystem.current.IsPointerOverGameObject())
        {
            _currentFigureDotes.Clear();
            _drawing = true;
            _drawingCoroutine = StartCoroutine(drawingCoroutine());
            if(onDrawingStart != null)  onDrawingStart(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0)&& _drawing) {
            _drawing = false;
            StopCoroutine(_drawingCoroutine);
            if (onDrawingEnd != null) onDrawingEnd(_currentFigureDotes);
        }
	}

    IEnumerator drawingCoroutine()
    {
        while (_drawing && drawInputAllowed)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _currentFigureDotes.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward);
                if (onDrawingUpdate != null) onDrawingUpdate(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward);               
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
