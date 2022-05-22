using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Button button;
    private Image image;
    private UnityAction exitAction;
    [SerializeField] private bool hasMouseOn;

   public void Init(Vector3 position,UnityAction buttonAction,UnityAction exitAction)
    {
        transform.position = position;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(buttonAction);
        this.exitAction = exitAction;
        gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hasMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hasMouseOn = false;
    }

    private void Exit()
    {
        gameObject.SetActive(false);
        exitAction?.Invoke();
    }

    private void Update()
    {
        /*if (Input.GetMouseButton(1))
            Exit();*/

        if(Input.GetMouseButtonDown(0))
            Exit();
    }
}
