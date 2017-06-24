#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RedactorManager : ManagerBase<RedactorManager> {

    [SerializeField]private GameObject _saveDialog;
    [SerializeField]private Text _nameFigureText;

    [SerializeField]private FigurePreviev _figurePreviev_base;
    [SerializeField]private FigurePreviev _figurePreviev_clusterized;

    private List<Vector2> _tempFigureDots;

    private void Start ()
    {
        _saveDialog.SetActive(false);

        InputManager.Current.onDrawingEnd += (List<Vector2> _figureDots) =>
        {
            _saveDialog.SetActive(true);
            InputManager.Current.drawInputAllowed = false;

           
            _figurePreviev_base.updateFigure(FigureAnalyzer.Vector2ToVector3(_figureDots));
            _tempFigureDots = FigureAnalyzer.ComplexClusterizeProcess(_figureDots);
            _figurePreviev_clusterized.updateFigure(FigureAnalyzer.Vector2ToVector3(_tempFigureDots));

        };

    }

    public void saveFigure() {
        FigureSaveLoadManager.Current.saveFigureToFile(_tempFigureDots, _nameFigureText.text);
    }

    public void clearPreviev()
    {
        InputManager.Current.drawInputAllowed = true;
        _saveDialog.SetActive(false);
        _figurePreviev_base.clear();
        _figurePreviev_clusterized.clear();
    }
 }
#endif