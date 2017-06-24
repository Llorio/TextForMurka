using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    #region INSTANCE PROPERTY

    protected static T _currentInstance = null;
    protected static bool _instanceExists = false;
	public static T Current
	{
		get
		{
			if (!_instanceExists)
			{
                _currentInstance = (T)GameObject.FindObjectOfType(typeof(T));
                _instanceExists = true;
			}

			return _currentInstance;
		}
	}

    #endregion


    protected void Awake()
	{
        if (!_instanceExists)
        {
			_currentInstance = (T)GameObject.FindObjectOfType(typeof(T));
            _instanceExists = true;
        }
	}

	protected void OnDestroy()
	{
		_currentInstance = null;
        _instanceExists = false;
    }

    public void SetActive(bool _state)
    {
        gameObject.SetActive(_state);
    }

}