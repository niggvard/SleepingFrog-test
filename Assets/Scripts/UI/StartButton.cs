using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onGameStarted;
    [SerializeField] private GameObject buttonObject;

    public void OnButtonClick()
    {
        buttonObject.SetActive(false);
        onGameStarted.Invoke();
    }
}
