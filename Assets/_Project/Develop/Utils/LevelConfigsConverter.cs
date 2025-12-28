using System.IO;
using System.Linq;
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
                if (columns.Length < 3) continue;

                var name = (i + 1).ToString() + "_" + columns[0]
                    .Replace(" ", "_").Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", "");

                var targetEn = columns[0]
                    .ToLower()
                    .Replace(".", " ").Replace(",", " ").Replace("?", " ").Replace("!", " ");
                if (targetEn.Contains("she ")) targetEn = targetEn.Replace("she ", "he/she");
                else if (targetEn.Contains("he ")) targetEn = targetEn.Replace("he ", "he/she");
                targetEn = targetEn.Replace(" ", "");

                var cubesPool = columns[2].Split(" ").Select(int.Parse).ToArray();

                var sentanceConfigs = AssetDatabase.LoadAssetAtPath<SentenceConfigs>($"{_savePath}/{name}.asset");
                if (sentanceConfigs == null)
                {
                    sentanceConfigs = ScriptableObject.CreateInstance<SentenceConfigs>();
                    AssetDatabase.CreateAsset(sentanceConfigs, $"{_savePath}/{name}.asset");
                }

                sentanceConfigs.SentenceEn = columns[0];
                sentanceConfigs.SentenceRu = columns[1];
                sentanceConfigs.TargetSentence = targetEn;
                sentanceConfigs.CubesPool = cubesPool;

                if (!Directory.Exists(_savePath))
                    Directory.CreateDirectory(_savePath);

                EditorUtility.SetDirty(sentanceConfigs);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
