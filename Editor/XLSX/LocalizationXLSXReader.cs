//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.IO;
using System.Linq;
using DGGLocalization.Data;
using DGGLocalization.Editor.Windows;
using DGGLocalization.Loaders;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.XLSX
{
    public static class LocalizationXlsxReader
    {
        private static string _filePath = Path.Combine(Application.dataPath, "localization.xlsx");
        private static IWorkbook _workbook;

        [MenuItem(MenuConstants.Import + "/XLSX", false, MenuConstants.ImportPriority)]
        private static void ReadLocalizationInFile()
        {
            _filePath = EditorUtility.OpenFilePanel("Localization file", "Assets", "xlsx");
            
            if (string.IsNullOrEmpty(_filePath))
            {
                Debug.Log("File is null!");
                return;
            }
            
            using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                _workbook = new XSSFWorkbook(fileStream);
            }

            var container = XlsxImportWindow.Open();

            if (!container.HasValue) return;
            
            StartImport(container.Value.parameters, container.Value.target);
        }

        private static void StartImport(ParametersImport parametersImport, Localization target)
        {
            var sheet = _workbook.GetSheetAt(0);

            var localizations = new List<LocalizationData>();
            var languages = new List<Language>();

            for (var index = 1; index < sheet.GetRow(0).Cells.Count; index++)
            {
                var row = sheet.GetRow(0);
                var cell = row.Cells[index];

                var find = target.Languages.FirstOrDefault(x => cell.StringCellValue == x.LanguageCode);
                var language = new Language(cell.StringCellValue, (find == null ? $"{index}" : find.LanguageName));
                
                languages.Add(language);
            }
            
            Debug.Log($"Languages: {languages.Count}");
            
            for (var index = 1; index < sheet.LastRowNum + 1; index++)
            {
                var row = sheet.GetRow(index);
                
                if (row.Cells.Count == 0 || string.IsNullOrEmpty(row.Cells[0].StringCellValue)) continue;
                
                var languageDates = new List<LanguageData>();

                for (var index2 = 1; index2 < row.Cells.Count; index2++)
                {
                    if (row.Cells[index2].CellType != CellType.String)
                    {
                        Debug.LogError($"Not string cell! IndexLanguage - {index2}, IndexRow - {index}");
                        
                        continue;
                    }
                    
                    if (index2 > languages.Count)
                    {
                        Debug.LogError($"Failed add language data! Value: {row.Cells[index2].StringCellValue} Index - {index2}");

                        break;
                    }
                    
                    var shortLanguage = languages[index2 - 1];
                    var value = row.Cells[index2].StringCellValue;
                    
                    languageDates.Add(new LanguageData(shortLanguage, value));
                }
                
                localizations.Add(new LocalizationData(row.Cells[0].StringCellValue, languageDates));
            }

            for (var index = 0; index < languages.Count; index++)
            {
                Debug.Log($"{languages[index].LanguageCode}");
            }
            
            for (var index = 0; index < localizations.Count; index++)
            {
                Debug.Log($"{localizations[index].LocalizationCode} - {JsonUtility.ToJson(localizations[index])}");
            }
            
            if (!parametersImport.ReplaceLocalizationInFile)
            {
                target.SetLocalization(languages.ToArray());
                target.SetLocalization(localizations);
                
                Loader.SetLocalization(target);
                
                return;
            }

            for (var index = 0; index < localizations.Count; index++)
            {
                var data = localizations[index].Data;
                
                Debug.Log($"Final: {localizations[index].LocalizationCode}");

                var current = target.GetLocalization(localizations[index].LocalizationCode);
                
                if (current == null) continue;

                var currentData = current.Data;
                
                for (var index2 = 0; index2 < currentData.Count; index2++)
                {
                    if (!parametersImport.Languages.Contains(currentData[index2].Language.LanguageCode)) continue;

                    var newData = data.Find(languageData => languageData.Language.LanguageCode == currentData[index2].Language.LanguageCode);

                    if (newData.Language == null) continue;
                    
                    currentData[index2] = newData;
                }
                
                target.SetLocalization(localizations[index].LocalizationCode, currentData);
            }
            
            Loader.SetLocalization(target);
        }
    }
}
