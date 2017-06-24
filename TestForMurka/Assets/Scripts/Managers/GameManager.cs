using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField]private FigurePreviev _figurePreviev;
    [SerializeField]private Text _pointsText;
    [SerializeField]private Text _timerText;
    [SerializeField]private UIGameOver _uiGameOver;
    private FigureData _currentFigure;
    private Coroutine _turnCoroutinePointer;
    public float currentTimer = 8f;
    public float timerStepCoef = 0.97f;
    private int _points;
    public int points
    {
        get
        {
            return _points;
        }
        private set
        {
            _points = value;
            _pointsText.text = value.ToString();
        }
    }

    public void startGame ()
    {
        _turnCoroutinePointer = StartCoroutine(turnCoroutine());
        InputManager.Current.onDrawingEnd += onDrawingEnd;
    }

    private void onDrawingEnd(List<Vector2> _figureDots)
    {
        
        if (FigureAnalyzer.CompareFigures(_currentFigure.figureDots, _figureDots))
        {
            StopCoroutine(_turnCoroutinePointer);
            points += 1;
            _turnCoroutinePointer = StartCoroutine(turnCoroutine());
        }
        else
        {

        }
    }


    private IEnumerator turnCoroutine()
    {
        _currentFigure = FigureSaveLoadManager.Current.loadRandomFigure();
        _figurePreviev.updateFigure(FigureAnalyzer.Vector2ToVector3(_currentFigure.figureDots));

        float _timer = currentTimer;
        currentTimer *= timerStepCoef;
        while (_timer > 0f)
        {
            yield return new WaitForEndOfFrame();
            _timer -= Time.deltaTime;
            _timerText.text = _timer.ToString("0.0");
        }
       
        gameOver();
    }

    private void gameOver()
    {
        _uiGameOver.showGameOver(points);
        _figurePreviev.clear();
        InputManager.Current.onDrawingEnd -= onDrawingEnd;
        InputManager.Current.drawInputAllowed = false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
