using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BlobEnemy))]
public class BlobEnemyInspector : Editor 
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();    
    }

    public void OnSceneGUI()
    {

    }

}
