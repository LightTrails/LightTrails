﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ReorderableListElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Tooltip("Can this element be dragged?")]
    public bool IsGrabbable = true;
    public bool IsFrozen = false;

    private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();
    private ReorderableList _currentReorderableListRaycasted;
    private RectTransform _draggingObject;
    private LayoutElement _draggingObjectLE;
    private Vector2 _draggingObjectOriginalSize;
    private RectTransform _fakeElement;
    private LayoutElement _fakeElementLE;
    private int _fromIndex;
    private bool _isDragging;
    private RectTransform _rect;
    private ReorderableList _reorderableList;
    internal bool isValid;

    #region IBeginDragHandler Members

    public void OnBeginDrag(PointerEventData eventData)
    {
        isValid = true;

        if (_reorderableList == null)
            return;

        //Can't drag, return...
        if (!_reorderableList.IsDraggable || !this.IsGrabbable)
        {
            _draggingObject = null;
            return;
        }

        _draggingObject = _rect;
        _fromIndex = _rect.GetSiblingIndex();

        if (isValid == false)
        {
            _draggingObject = null;
            return;
        }

        //Put _dragging object into the dragging area
        _draggingObjectOriginalSize = gameObject.GetComponent<RectTransform>().rect.size;
        _draggingObjectLE = _draggingObject.GetComponent<LayoutElement>();
        _draggingObject.SetParent(_reorderableList.DraggableArea, true);
        _draggingObject.SetAsLastSibling();

        //Create a fake element for previewing placement
        _fakeElement = new GameObject("Fake").AddComponent<RectTransform>();
        _fakeElementLE = _fakeElement.gameObject.AddComponent<LayoutElement>();

        RefreshSizes();

        //Send OnElementGrabbed Event
        if (_reorderableList.OnElementGrabbed != null)
        {
            _reorderableList.OnElementGrabbed.Invoke(new ReorderableList.ReorderableListEventStruct
            {
                DroppedObject = _draggingObject.gameObject,
                FromIndex = _fromIndex,
            });

            if (!isValid)
            {
                CancelDrag();
                return;
            }
        }

        _isDragging = true;
    }

    #endregion

    #region IDragHandler Members

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging)
            return;
        if (!isValid)
        {
            CancelDrag();
            return;
        }

        var canvas = _draggingObject.GetComponentInParent<Canvas>();
        Vector3 mousePos = (Input.mousePosition - canvas.GetComponent<RectTransform>().localPosition);
        Vector3 localPosition = new Vector3(mousePos.x + 15, mousePos.y - 15, mousePos.z);
        _draggingObject.GetComponent<RectTransform>().localPosition = localPosition;

        Vector3 worldPosition = _draggingObject.position;


        //Check everything under the cursor to find a ReorderableList
        EventSystem.current.RaycastAll(eventData, _raycastResults);
        for (int i = 0; i < _raycastResults.Count; i++)
        {
            _currentReorderableListRaycasted = _raycastResults[i].gameObject.GetComponent<ReorderableList>();
            if (_currentReorderableListRaycasted != null)
            {
                break;
            }
        }

        //If nothing found or the list is not dropable, put the fake element outsite
        if (_currentReorderableListRaycasted == null || _currentReorderableListRaycasted.IsDropable == false)
        {
            RefreshSizes();
            _fakeElement.transform.SetParent(_reorderableList.DraggableArea, false);

        }
        //Else find the best position on the list and put fake element on the right index  
        else
        {
            if (_fakeElement.parent != _currentReorderableListRaycasted)
                _fakeElement.SetParent(_currentReorderableListRaycasted.Content, false);

            float minDistance = float.PositiveInfinity;
            int targetIndex = 0;
            float dist = 0;

            for (int j = 0; j < _currentReorderableListRaycasted.Content.childCount; j++)
            {
                var c = _currentReorderableListRaycasted.Content.GetChild(j).GetComponent<RectTransform>();
                var le = c.GetComponent<ReorderableListElement>();
                if (le != null && le.IsFrozen)
                {
                    continue;
                }

                if (_currentReorderableListRaycasted.ContentLayout is VerticalLayoutGroup)
                    dist = Mathf.Abs(c.position.y - worldPosition.y);
                else if (_currentReorderableListRaycasted.ContentLayout is HorizontalLayoutGroup)
                    dist = Mathf.Abs(c.position.x - worldPosition.x);
                else if (_currentReorderableListRaycasted.ContentLayout is GridLayoutGroup)
                    dist = (Mathf.Abs(c.position.x - worldPosition.x) + Mathf.Abs(c.position.y - worldPosition.y));

                if (dist < minDistance)
                {
                    minDistance = dist;
                    targetIndex = j;
                }
            }

            RefreshSizes();
            _fakeElement.SetSiblingIndex(targetIndex);
            _fakeElement.gameObject.SetActive(true);

        }
    }

    #endregion

    #region IEndDragHandler Members

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;

        if (_draggingObject != null)
        {
            //If we have a, ReorderableList that is dropable
            //Put the dragged object into the content and at the right index
            if (_currentReorderableListRaycasted != null && _currentReorderableListRaycasted.IsDropable
                && (_currentReorderableListRaycasted == _reorderableList))
            {
                var args = new ReorderableList.ReorderableListEventStruct
                {
                    DroppedObject = _draggingObject.gameObject,
                    FromIndex = _fromIndex,
                    ToIndex = _fakeElement.GetSiblingIndex()
                };

                if (_reorderableList && _reorderableList.OnElementDropped != null)
                {
                    _reorderableList.OnElementDropped.Invoke(args);
                }

                if (!isValid)
                {
                    CancelDrag();
                    return;
                }

                RefreshSizes();

                _draggingObject.SetParent(_currentReorderableListRaycasted.Content, false);
                _draggingObject.rotation = _currentReorderableListRaycasted.transform.rotation;
                _draggingObject.SetSiblingIndex(_fakeElement.GetSiblingIndex());

                if (!isValid) throw new Exception("It's too late to cancel the Transfer! Do so in OnElementDropped!");

            }
            //We don't have an ReorderableList
            else
            {
                CancelDrag();
            }
        }

        //Delete fake element
        if (_fakeElement != null)
            Destroy(_fakeElement.gameObject);
    }

    #endregion

    void CancelDrag()
    {
        _isDragging = false;

        RefreshSizes();
        _draggingObject.SetParent(_reorderableList.Content, false);
        _draggingObject.rotation = _reorderableList.Content.transform.rotation;
        _draggingObject.SetSiblingIndex(_fromIndex);

        if (!isValid) throw new Exception("Transfer is already Cancelled.");

        //Delete fake element
        if (_fakeElement != null)
            Destroy(_fakeElement.gameObject);
    }

    private void RefreshSizes()
    {
        Vector2 size = _draggingObjectOriginalSize;

        if (_currentReorderableListRaycasted != null && _currentReorderableListRaycasted.IsDropable && _currentReorderableListRaycasted.Content.childCount > 0)
        {
            var firstChild = _currentReorderableListRaycasted.Content.GetChild(0);
            if (firstChild != null)
            {
                size = firstChild.GetComponent<RectTransform>().rect.size;
            }
        }

        _draggingObject.sizeDelta = size;
        _fakeElementLE.preferredHeight = _draggingObjectLE.preferredHeight = size.y;
        _fakeElementLE.preferredWidth = _draggingObjectLE.preferredWidth = size.x;
    }

    public void Init(ReorderableList reorderableList)
    {
        _reorderableList = reorderableList;
        _rect = GetComponent<RectTransform>();
    }
}