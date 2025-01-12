# DGG Localization

A modular localization system for the Unity engine.

📚 **Full Documentation**: [DGG Localization Docs](https://dark-dgg.gitbook.io/dgg-localization)

---

## Installation via UPM

1. **Install UniTask**  
   [UniTask GitHub Repository](https://github.com/Cysharp/UniTask?ysclid=lulo5ve1dr734166736736#upm-package)

2. **Install DGG Localization**  
   Add the package via this Git URL:  
   https://github.com/DaniilDGG/DGG-Localization.git

---

## How to Use

1. **Load Localization Files**  
   By default, the loader processes all localization files listed in the `LocalizationProfile`. This profile must be created in the `Resources` folder and specifies file paths relative to `StreamingAssets`.  
   Need more control? Implement your own loader by creating a custom `ILocalizationLoader`.

2. **Set Up Languages**  
   Configure the languages used in your project under `Localization/Settings/Languages`. Once configured, the system is ready to use.

3. **Define Keys and Add Components**  
   Define your localization keys and place localization components in your scene.

4. **Switch Languages**  
   Change the active language using:  
   - `LocalizationController.SwitchLanguage(int languageIndex)`  
   - `LocalizationController.SwitchLanguage(string languageName)`  

   💡 **Example**: Check the **Demo Scene** for implementation details.

5. **XLSX Import/Export**  
   Edit localization data outside Unity by importing/exporting XLSX files.

---

Start localizing your Unity project effortlessly with **DGG Localization**!
