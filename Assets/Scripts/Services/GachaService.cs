using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaService : MonoBehaviour {

    //==============================================================================================================//
    //==============================================================================================================//

    [Serializable]
    public class RarityInput {
        public RarityData Rarity;
        public InputField Field;
    }

    [Header("Gacha Settings")]
    [SerializeField] private int _gachaCost = 30;
    [SerializeField] private List<RarityInput> _rarityInputs;

    [Header("Storage UI")]
    [SerializeField] private Transform _rarityItemList;
    [SerializeField] private StorageItem _rarityItemPrefab;
    [SerializeField] private Text _storageValueText;

    private List<StorageItem> _storageItems = new List<StorageItem>();

    //==============================================================================================================//
    //==============================================================================================================//

    void Start() {
        UpdateStorageUI();
    }

    //==============================================================================================================//
    //==============================================================================================================//

    public void Roll() {
        int[] weights = GetWeights();
        int totalWeight = GetTotalWeight(weights);

        if (totalWeight <= 0) {
            Debug.Log("Invalid percentages: total cannot be zero");
            return;
        }

        bool hasSpent = CurrencyService.Instance.TrySpend(_gachaCost);
        if (!hasSpent) {
            Debug.Log("Not enough coin");
            CheckGameOver();
            return;
        }

        RarityData result = PickRarity(weights, totalWeight);
        AddToStorage(result);
        CheckGameOver();
    }

    private int[] GetWeights() {
        int[] weights = new int[_rarityInputs.Count];

        for (int i = 0; i < _rarityInputs.Count; i++) {
            int value;
            if (!int.TryParse(_rarityInputs[i].Field.text, out value)) {
                value = 0;
            }
            weights[i] = Mathf.Max(0, value);
        }

        return weights;
    }

    private int GetTotalWeight(int[] weights) {
        int total = 0;
        for (int i = 0; i < weights.Length; i++) {
            total += weights[i];
        }
        return total;
    }

    private RarityData PickRarity(int[] weights, int totalWeight) {
        int randomNumber = UnityEngine.Random.Range(0, totalWeight);
        int sum = 0;

        for (int i = 0; i < weights.Length; i++) {
            sum += weights[i];
            if (randomNumber < sum) return _rarityInputs[i].Rarity;
        }
        return _rarityInputs[_rarityInputs.Count - 1].Rarity;
    }

    //==============================================================================================================//
    //==============================================================================================================//

    private void AddToStorage(RarityData rarity) {
        StorageItem item = Instantiate(_rarityItemPrefab, _rarityItemList);
        item.Init(rarity, SellItem);

        int index = _storageItems.Count;
        for (int i = 0; i < _storageItems.Count; i++) {
            if (_storageItems[i].Data.RarityType < rarity.RarityType) {
                index = i;
                break;
            }
        }

        _storageItems.Insert(index, item);
        item.transform.SetSiblingIndex(index);
        UpdateStorageUI();
    }

    private void SellItem(StorageItem item) {
        CurrencyService.Instance.AdjustCoins(item.Data.Value);

        _storageItems.Remove(item);
        Destroy(item.gameObject);

        UpdateStorageUI();
        CheckGameOver();
    }

    private int GetStorageValue() {
        int total = 0;
        foreach (StorageItem item in _storageItems) {
            total += item.Data.Value;
        }
        return total;
    }

    private void UpdateStorageUI() {
        if (_storageValueText == null) return;
        _storageValueText.text = $"Storage coin value : {GetStorageValue()}";
    }

    private void CheckGameOver() {
        int total = CurrencyService.Instance.Coins + GetStorageValue();
        if (total < _gachaCost) Debug.Log("Game Over");
    }

    //==============================================================================================================//
    //==============================================================================================================//
}