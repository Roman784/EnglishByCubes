#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Utils
{
    public class LevelConfigsConverter : MonoBehaviour
    {
        [SerializeField] private string _sentancesTablePath;

        private string _savePath = "Assets/_Project/Configs/Sentances";

        [ContextMenu("Convert")]
        private void Convert()
        {
            var csvData = Resources.Load<TextAsset>(_sentancesTablePath);

            var lines = csvData.text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var columns = lines[i].Split(';');
                if (columns.Length < 3)
                {
                    Debug.Log($"{i + 1} {columns.Length}");
                    continue; 
                }

                var name = (i + 1).ToString() + "_" + columns[0]
                    .Replace(" ", "_").Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", "");

                if (columns[0] == "")
                {
                    ConvertSentence(i+1, name, "", "", "", null);
                    continue;
                }

                var targetEn = columns[0]
                    .ToLower()
                    .Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", "");

                targetEn = targetEn.Replace(" ", "|");
                targetEn = targetEn.Insert(0, "|");
                targetEn += "|";

                if (targetEn.Contains("|she|")) targetEn = targetEn.Replace("|she|", "|he/she|");
                else if (targetEn.Contains("|he|")) targetEn = targetEn.Replace("|he|", "|he/she|");
                targetEn = targetEn.Replace("|", "");

                var cubesPool = columns[2].Split(" ").Select(int.Parse).ToArray();

                ConvertSentence(i+1, name, columns[0], columns[1], targetEn, cubesPool);
            }
        }

        private void ConvertSentence(int number, string confName, string senEn, string senRu, string targetEn, int[] cubesPool)
        {
            // Формируем ожидаемое имя файла
            string expectedFileName = $"{confName}.asset";
            string expectedPath = Path.Combine(_savePath, expectedFileName).Replace("\\", "/");

            SentenceConfigs sentanceConfigs;

            // Проверяем, существует ли файл с таким числом
            SentenceConfigs existingConfig = FindAssetByNumber(number);

            if (existingConfig != null)
            {
                // Получаем путь к существующему файлу
                string existingPath = AssetDatabase.GetAssetPath(existingConfig);
                string existingFileName = Path.GetFileName(existingPath);

                // Проверяем, совпадает ли имя файла с ожидаемым
                if (existingPath != expectedPath)
                {
                    // Если имя не совпадает, переименовываем файл
                    string directory = Path.GetDirectoryName(existingPath);
                    string newPath = Path.Combine(directory, expectedFileName).Replace("\\", "/");

                    string error = AssetDatabase.RenameAsset(existingPath, expectedFileName);

                    if (string.IsNullOrEmpty(error))
                    {
                        Debug.Log($"Файл переименован: {existingFileName} -> {expectedFileName}");
                        sentanceConfigs = existingConfig;
                    }
                    else
                    {
                        Debug.LogError($"Ошибка переименования: {error}");
                        return;
                    }
                }
                else
                {
                    sentanceConfigs = existingConfig;
                    Debug.Log($"Загружен существующий ассет: {expectedFileName}");
                }
            }
            else
            {
                // Создаем новый ассет
                sentanceConfigs = ScriptableObject.CreateInstance<SentenceConfigs>();

                if (!Directory.Exists(_savePath))
                    Directory.CreateDirectory(_savePath);

                AssetDatabase.CreateAsset(sentanceConfigs, expectedPath);
                Debug.Log($"Создан новый ассет: {expectedFileName}");
            }

            // Обновляем данные
            sentanceConfigs.SentenceEn = senEn;
            sentanceConfigs.SentenceRu = senRu;
            sentanceConfigs.TargetSentence = targetEn;
            sentanceConfigs.CubesPool = cubesPool;

            EditorUtility.SetDirty(sentanceConfigs);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public SentenceConfigs FindAssetByNumber(int number)
        {
            if (!Directory.Exists(_savePath))
                return null;

            string[] allFiles = Directory.GetFiles(_savePath, "*.asset", SearchOption.TopDirectoryOnly);

            foreach (string file in allFiles)
            {
                string fileName = Path.GetFileName(file);
                if (fileName.EndsWith(".meta")) continue;

                string[] parts = fileName.Split('_');
                if (parts.Length > 0 && int.TryParse(parts[0], out int fileNumber))
                {
                    if (fileNumber == number)
                    {
                        string fullPath = file.Replace("\\", "/");
                        return AssetDatabase.LoadAssetAtPath<SentenceConfigs>(fullPath);
                    }
                }
            }

            return null;
        }

        [ContextMenu("Delete empty")]
        public void DeleteNumberUnderscoreFiles()
        {
            // Регулярное выражение для формата "[число]_" - только числа и подчеркивание
            // ^\d+_$ - означает: начало строки, одна или более цифр, подчеркивание, конец строки
            Regex regex = new Regex(@"^\d+_\.asset$");

            string[] assetFiles = Directory.GetFiles(_savePath, "*.asset", SearchOption.AllDirectories);
            List<string> filesToDelete = new List<string>();

            foreach (string file in assetFiles)
            {
                string fileName = Path.GetFileName(file);
                if (regex.IsMatch(fileName))
                {
                    filesToDelete.Add(file);
                    Debug.Log($"Найден для удаления: {fileName}");
                }
            }

            if (filesToDelete.Count > 0)
            {
                foreach (string file in filesToDelete)
                {
                    string unityPath = file.Replace("\\", "/");
                    AssetDatabase.DeleteAsset(unityPath);
                    Debug.Log($"Удален: {Path.GetFileName(file)}");
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"Удалено {filesToDelete.Count} файлов формата [число]_.asset");
            }
            else
            {
                Debug.Log("Файлы формата [число]_.asset не найдены");
            }
        }
    }
}
#endif