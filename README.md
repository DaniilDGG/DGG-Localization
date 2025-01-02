# DGG Localization
Modular localization system for the Unity engine.

# Installation via UPM

First, install UniTask: https://github.com/Cysharp/UniTask?ysclid=lulo5ve1dr734166736736#upm-package  
Then, install this package via git URL: https://github.com/DaniilDGG/DGG-Localization.git

# How to Use

The default loader loads all localization files whose paths (relative to StreamingAssets) are specified in the LocalizationProfile, created in the Resources folder.  
If this is not sufficient, you can create your own implementation of ILocalizationLoader.

First, you need to set the languages used in localization via Localization/Language settings.
After that, the system is ready to work; all that remains is to define localization keys and place localization components in the scene.

To change the language, use LocalizationController.SwitchLanguage(int) or LocalizationController.SwitchLanguage(string). Example: Demo Scene.  
Additionally, XLSX import and export are supported for editing localizations outside the Unity editor.
