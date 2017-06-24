using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FigureSaveLoadManager : ManagerBase<FigureSaveLoadManager> {

    private TextAsset[] _figures;

    private new void Awake()
    {
        base.Awake();
        try
        {
            _figures = Resources.LoadAll<TextAsset>("Figures/");
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    /*  public FigureData loadFigure(int _fileNum) {

         string[] _fileNames = Directory.GetFiles(Application.dataPath + "/Figures");
          if (_fileNames.Length > _fileNum)
          {
              return loadFigure(_fileNames[_fileNum]);
          }
          else
          {
              Debug.Log("Error Loading Figure: _fileNum > _fileNames.Length");
              return null;
          }
      }*/

    public FigureData loadRandomFigure()
    {
      return  loadFigure(UnityEngine.Random.Range(0, _figures.Length));
    }


    public FigureData loadFigure (int _fileNum)
    {
        FigureData _figureData;      
        try
        {
             TextAsset _data = _figures[_fileNum];
             _figureData = JsonUtility.FromJson<FigureData>(_data.text);

             return _figureData;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return null;
        }

    }

#if UNITY_EDITOR
    public void saveFigureToFile(List<Vector2> _figureDotsList, string _fileName)
    {
        FigureData _figureData = new FigureData();
        _figureData.figureDots = _figureDotsList;
        saveFigureToFile(_figureData, _fileName);
    }

    public void saveFigureToFile(FigureData _figureData, string _fileName)
    {
        try
        {
            string jsonData = JsonUtility.ToJson(_figureData);
           // Debug.Log(jsonData);

            FileStream file = File.Create(Application.dataPath + "/Resources/Figures/" + _fileName + ".json");
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(jsonData);
            }
            file.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
#endif
}
