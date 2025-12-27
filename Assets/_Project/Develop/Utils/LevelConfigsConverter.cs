using System.IO;
using UnityEditor;
using UnityEngine;

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
                if (columns.Length < 2) continue;

                var name = (i + 1).ToString() + "_" + columns[0]
                    .Replace(" ", "_").Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", "");
                var targetEn = columns[0]
                    .ToLower()
                    .Replace(" ", "").Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", "")
                    .Replace("he", "he/she").Replace("she", "he/she").Replace("he/he/she", "he/she");

                var sentanceConfigs = ScriptableObject.CreateInstance<SentanceConfigs>();
                sentanceConfigs.SentenceEn = columns[0];
                sentanceConfigs.SentenceRu = columns[1];
                sentanceConfigs.TargetSentence = targetEn;

                if (!Directory.Exists(_savePath))
                    Directory.CreateDirectory(_savePath);

                AssetDatabase.CreateAsset(sentanceConfigs, $"{_savePath}/{name}.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
