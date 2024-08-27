using UnityEditor;
using HitRewardSpace;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(HitReward))]
public class HitRewardEditor : UnityEditor.Editor
{
    private HitReward _target;
    private SerializedObject _serializedObject;

    private SerializedProperty _miniGameObj;
    private SerializedProperty _paidStartPrice;
    private SerializedProperty _coinIcon;

    private SerializedProperty _randomOrder;
    private SerializedProperty _randomSelection;

    private SerializedProperty _rewardSet;
    private SerializedProperty _useOneCurrency;
    private SerializedProperty _amounts;

    private SerializedProperty _gameController;
    private SerializedProperty _animator;

    private bool _showCommunications;


    private void OnEnable() {
        AssignVariables();
    }

    private void AssignVariables() {
        _target = (HitReward)target;
        _serializedObject = new SerializedObject(target);

        _miniGameObj = _serializedObject.FindProperty("_miniGameObj");
        _paidStartPrice = _serializedObject.FindProperty("_paidStartPrice");
        _coinIcon = _serializedObject.FindProperty("_coinIcon");

        _randomOrder = _serializedObject.FindProperty("_randomOrder");
        _randomSelection = _serializedObject.FindProperty("_randomSelection");

        _rewardSet = _serializedObject.FindProperty("_rewardSet");
        _useOneCurrency = _serializedObject.FindProperty("_useOneCurrency");

        _amounts = _serializedObject.FindProperty("_amounts");

        _gameController = _serializedObject.FindProperty("_gameController");
        _animator = _serializedObject.FindProperty("_animator");
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_paidStartPrice, true);
        EditorGUILayout.PropertyField(_coinIcon, true);

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_useOneCurrency, true);
        if (_useOneCurrency.boolValue)
            EditorGUILayout.PropertyField(_amounts, true);
        else
            EditorGUILayout.PropertyField(_rewardSet, true);

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_randomSelection, true);
        if (_randomSelection.boolValue == false)
            EditorGUILayout.PropertyField(_randomOrder, true);

        _showCommunications = EditorGUILayout.BeginFoldoutHeaderGroup(_showCommunications, "Communications");
        if (_showCommunications) {
            EditorGUILayout.PropertyField(_miniGameObj, true);
            EditorGUILayout.PropertyField(_gameController, true);
            EditorGUILayout.PropertyField(_animator, true);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        #region Save
        if (GUI.changed) {
            _serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(_target.gameObject);

            if (Application.isPlaying == false)
                EditorSceneManager.MarkSceneDirty(_target.gameObject.scene);

            AssignVariables();
        }
        _serializedObject.Update();
        #endregion
    }
}
