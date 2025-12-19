using UnityEngine;
using UnityEditor; // Required for Editor tools
using System.IO;   // Required for file paths

public class CardAssetGenerator : EditorWindow
{
    // This adds a menu item at the top of Unity editor
    [MenuItem("Tools/Generate Card Data from Selection")]
    public static void GenerateCards()
    {
        // Get all selected objects in the Project window
        Object[] selectedObjects = Selection.objects;

        // Check if anything is selected
        if (selectedObjects.Length == 0)
        {
            Debug.LogError("Please select the card Sprites first!");
            return;
        }

        // Loop through every selected object
        foreach (Object obj in selectedObjects)
        {
            // Check if it is a Sprite/Texture
            if (obj is Texture2D || obj is Sprite)
            {
                CreateCardData(obj);
            }
        }
        
        // Save everything
        AssetDatabase.SaveAssets();
        Debug.Log("Success! Generated " + selectedObjects.Length + " card assets.");
    }

    static void CreateCardData(Object spriteObject)
    {
        // 1. Create a new instance of the ScriptableObject
        CardData newData = ScriptableObject.CreateInstance<CardData>();

        // 2. Assign the sprite automatically
        // (We need to load it as a Sprite type explicitly)
        string spritePath = AssetDatabase.GetAssetPath(spriteObject);
        Sprite actualSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        
        newData.cardImage = actualSprite;
        newData.cardName = spriteObject.name; // Set internal name to filename

        // 3. Try to guess Rank/Suit (Optional - Logic depends on your filenames)
        // If your files are named like "spades_1", "hearts_12", this helps:
        /*
        string[] parts = spriteObject.name.Split('_');
        if (parts.Length >= 2)
        {
             // This is just a placeholder example of how you could auto-parse
             // newData.rank = int.Parse(parts[1]); 
        }
        */

        // 4. Create the actual file on disk
        // We save it in the same folder as the image, but with .asset extension
        string folderPath = Path.GetDirectoryName(spritePath);
        string assetPath = Path.Combine(folderPath, spriteObject.name + "_Data.asset");

        // Check if it already exists so we don't overwrite blindly
        if (AssetDatabase.LoadAssetAtPath<CardData>(assetPath) == null)
        {
            AssetDatabase.CreateAsset(newData, assetPath);
        }
        else
        {
            Debug.LogWarning("Card Data already exists for: " + spriteObject.name);
        }
    }
}