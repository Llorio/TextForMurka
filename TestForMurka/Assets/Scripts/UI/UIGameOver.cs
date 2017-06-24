using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIGameOver : MonoBehaviour {
    [SerializeField]private Text _pointsText;


	private void Start ()
    {
        gameObject.SetActive(false);
    }

    public void showGameOver(int _points)
    {
        _pointsText.text = _points.ToString();
        gameObject.SetActive(true);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
