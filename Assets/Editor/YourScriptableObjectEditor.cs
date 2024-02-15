using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(AudioSOData))]
public class AudioSODataEditor : Editor
{
    private const string targetFolder = "Assets/Resources/Audio"; // ”кажите путь к целевой папке

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Event currentEvent = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));

        GUI.Box(dropArea, "Drop external file here");

        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(currentEvent.mousePosition))
                    break;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (var draggedObject in DragAndDrop.paths)
                    {
                        string fileName = Path.GetFileName(draggedObject);
                        string destinationPath = Path.Combine(targetFolder, fileName);

                        File.Copy(draggedObject, destinationPath, true);
                        AssetDatabase.Refresh();

                        Debug.Log("File copied to: " + destinationPath);
                    }
                }

                break;
        }
    }
}
