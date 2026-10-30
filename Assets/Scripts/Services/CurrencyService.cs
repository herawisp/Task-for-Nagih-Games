using UnityEngine;
using UnityEngine.UI;

public class CurrencyService : MonoBehaviour {

    //==============================================================================================================//
    //==============================================================================================================//

    [SerializeField] private int _startingCoins = 200;
    [SerializeField] private Text _coinText;

    public int Coins { private set; get; }

    //==============================================================================================================//
    //==============================================================================================================//

    public static CurrencyService Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Coins = _startingCoins;
        UpdateUI();
    }

    //==============================================================================================================//
    //==============================================================================================================//

    public bool HasEnoughCoins(int amount) {
        return Coins >= amount;
    }

    public bool TrySpend(int amount) {
        if (!HasEnoughCoins(amount)) return false;

        Coins -= amount;
        UpdateUI();
        return true;
    }

    public void AdjustCoins(int amount) {
        Coins += amount;
        UpdateUI();
    }

    //==============================================================================================================//
    //==============================================================================================================//

    private void UpdateUI() {
        if (_coinText == null) return;
        _coinText.text = $"My Coin : {Coins}";
    }

    //==============================================================================================================//
    //==============================================================================================================//
}