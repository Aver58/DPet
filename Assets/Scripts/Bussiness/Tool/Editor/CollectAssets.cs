using System.Collections.Generic;
using System.Linq;
using Scripts.Framework.UI;
using UnityEditor;
using UnityEngine;

namespace Bussiness.Tool.Editor {
    public class CollectAssets {
        // 收集 Assets\ToBundle\Atlas 目录下的所有 .spriteatlas 文件到 Assets\ToBundle\Config\Localization\Chinese\SpriteAtlas.csv
        [MenuItem("Tools/CollectAssets/CollectSpriteAtlas")]
        public static void CollectSpriteAtlas() {
            var spriteAtlasDirectory = ImageExtension.SpriteAtlasDirectory;
            string csvPath = "Assets/ToBundle/Config/Localization/Chinese/SpriteAtlas.csv";
            if (!System.IO.Directory.Exists(spriteAtlasDirectory)) {
                Debug.LogError($"Directory not found: {spriteAtlasDirectory}");
                return;
            }

            var atlasFiles = System.IO.Directory.GetFiles(spriteAtlasDirectory, "*.spriteatlas", System.IO.SearchOption.AllDirectories);
            if (atlasFiles.Length == 0) {
                Debug.LogWarning("No sprite atlas files found.");
                return;
            }

            var constLineLength = 3;
            var csvLines = System.IO.File.ReadAllLines(csvPath).ToList();
            if (csvLines.Count < constLineLength) {
                Debug.LogError($"CSV file {csvPath} does not have enough lines. Expected at least {constLineLength} lines.");
                return;
            }

            // 清除之前的内容
            csvLines.RemoveRange(constLineLength, csvLines.Count - constLineLength);

            for (int i = 0; i < atlasFiles.Length; i++) {
                var filePath = atlasFiles[i];
                var fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(filePath);
                var newLine = $"{csvLines.Count - constLineLength},{fileNameWithoutExtension}";
                csvLines.Add(newLine);
            }
            System.IO.File.WriteAllLines(csvPath, csvLines);
            UnityEngine.Debug.Log($"SpriteAtlas collect completed. Output: {csvPath}");
        }


        // 收集所有图集文件的Sprite文件到 Assets\ToBundle\Config\Localization\Chinese\Sprite.csv
        [MenuItem("Tools/CollectAssets/CollectSprite")]
        public static void CollectSprite() {
            string atlasDirectory = "Assets/ToBundle/Atlas";
            string csvPath = "Assets/ToBundle/Config/Localization/Chinese/Sprite.csv";
            if (!System.IO.Directory.Exists(atlasDirectory)) {
                UnityEngine.Debug.LogError($"Directory not found: {atlasDirectory}");
                return;
            }

            var atlasFiles = System.IO.Directory.GetFiles(atlasDirectory, "*.spriteatlas", System.IO.SearchOption.AllDirectories);
            if (atlasFiles.Length == 0) {
                UnityEngine.Debug.LogWarning("No sprite atlas files found.");
                return;
            }

            var constLineLength = 3;
            var csvLines = System.IO.File.ReadAllLines(csvPath).ToList();
            if (csvLines.Count < constLineLength) {
                UnityEngine.Debug.LogError($"CSV file {csvPath} does not have enough lines. Expected at least {constLineLength} lines.");
                return;
            }

            // 清除之前的内容
            csvLines.RemoveRange(constLineLength, csvLines.Count - constLineLength);

            HashSet<string> nameMap = new HashSet<string>();
            for (int i = 0; i < atlasFiles.Length; i++) {
                var filePath = atlasFiles[i];
                // filePath 转 AssetDatabase 路径
                filePath = filePath.Replace(Application.dataPath, "Assets");
                var spriteAtlas = AssetDatabase.LoadAssetAtPath<UnityEngine.U2D.SpriteAtlas>(filePath);
                if (spriteAtlas == null) {
                    UnityEngine.Debug.LogWarning($"SpriteAtlas not found at path: {filePath}");
                    continue;
                }

                var sprites = new Sprite[spriteAtlas.spriteCount];
                spriteAtlas.GetSprites(sprites);
                foreach (var sprite in sprites) {
                    var name = sprite.name;
                    name = name.Replace("(Clone)", "");
                    if (nameMap.Contains(name)) {
                        UnityEngine.Debug.LogError($"Duplicate sprite name found: {name}");
                        continue;
                    }
                    var newLine = $"{name},{i}";
                    csvLines.Add(newLine);
                    nameMap.Add(name);
                }
            }
            System.IO.File.WriteAllLines(csvPath, csvLines);
            UnityEngine.Debug.Log($"Sprite collect completed. Output: {csvPath}");
        }
    }
}