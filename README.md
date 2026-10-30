# Task for Nagih Games

### Scripts
- **GachaService**: handles the roll, reads the input fields, picks the rarity, manages the storage list and checks Game Over.
- **CurrencyService**: keeps the player's coins and updates the coin text.
- **RarityData**: ScriptableObject with the sprite, rarity type and sell value of each rarity.
- **StorageItem**: the item in the storage panel. It shows the icon and tells GachaService when it is clicked.

### Bugs and Issues I encountered
### 1. Changing a ScriptableObject at runtime
- **Problem:** `RarityData` had a `Percentage` value that was changed while the game was running. In the Editor, the changed value can stay on the asset after the game stops.
- **Cause:** A ScriptableObject is one shared asset. Any change made during play affects everything that uses it, and in the Editor the change stays after Play mode stops.
- **Fix:** `RarityData` only keeps fixed data (sprite, rarity type, sell value). The percentages stay in the input fields and are read by `GachaService`.

### 2. Storage item could not be sold
- **Problem:** Clicking an item in the storage panel did nothing.
- **Cause:** The Button's `Interactable` property was set to false on the prefab.
- **Fix:** Set `Interactable` to true.
