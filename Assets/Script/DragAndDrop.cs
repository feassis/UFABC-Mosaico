using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    Vector3 startPos;

    public static event Action OnPieceCorrectPlacement;

    public bool IsCorrect {  get; private set; }

    private void Awake()
    {
        
    }

    public void PosiçãoInicialPartes()
    {
        startPos = transform.position;
    }

    public void Drag()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void Drop()
    {
        CheckMatch();
    }

    public void MoveBack()
    {
        transform.position = startPos;
    }

    public void Snap(GameObject img, GameObject lm)
    {
        img.transform.position = lm.transform.position;
    }

    public void CheckMatch()
    {
        
        GameObject dragImage = gameObject;
        GameObject lm = GameObject.Find("LM"+gameObject.tag);

        float distance = Vector3.Distance(lm.transform.position,
        dragImage.transform.position);

        if(distance <= 50)
        {
            Snap(dragImage, lm);
            IsCorrect = true;
            OnPieceCorrectPlacement?.Invoke();
        }
        else
        {
            MoveBack();
            IsCorrect = false;
        }
    }


    public void Test() => Debug.Log("Test");
}
