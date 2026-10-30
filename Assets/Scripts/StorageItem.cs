using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StorageItem : MonoBehaviour {

    //==============================================================================================================//
    //==============================================================================================================//

    [SerializeField] private Image _icon;

    public RarityData Data { get; private set; }

    private Action<StorageItem> _onClicked;

    //==============================================================================================================//
    //==============================================================================================================//

    public void Init(RarityData data, Action<StorageItem> onClicked) {
        Data = data;
        _onClicked = onClicked;
        _icon.sprite = data.Sprite;

        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void HandleClick() {
        _onClicked?.Invoke(this);
    }

    //==============================================================================================================//
    //==============================================================================================================//
}