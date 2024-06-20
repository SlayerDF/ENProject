using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using TMPro;
using UnityEditor;
using UnityEngine;

// https://gist.github.com/lukz/0065cb4a18242aa94633ac02789234a3
[CustomEditor(typeof(TextMeshProUGUI), true), CanEditMultipleObjects]
public class FixedTMPColorsEditor : TMP_EditorPanelUI
{
    private static bool _customToolsFoldout = false;

    private static readonly GUIContent FixedFaceLabel = new("Fixed face color");
    private static readonly GUIContent FixedOutlineLabel = new("Fixed outline color");
    private static readonly GUIContent FixedUnderlayLabel = new("Fixed underline color");

    private Color _outlineColor;
    private Color _underlayColor;
    private Color _faceColor;

    protected override void OnEnable()
    {
        base.OnEnable();

        _faceColor = ReverseTMPOutlineColor(m_TextComponent.faceColor);
        _outlineColor = ReverseTMPOutlineColor(m_TextComponent.outlineColor);
        _underlayColor = ReverseTMPOutlineColor(m_TextComponent.fontSharedMaterial.GetColor(ShaderUtilities.ID_UnderlayColor));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawCustomTools();
    }

    protected void DrawCustomTools()
    {
        var rect = EditorGUILayout.GetControlRect(false, 24);

        if (GUI.Button(rect, new GUIContent("<b>Custom tools</b>"), TMP_UIStyleManager.sectionHeader))
            _customToolsFoldout = !_customToolsFoldout;

        GUI.Label(rect, _customToolsFoldout ? k_UiStateLabel[0] : k_UiStateLabel[1], TMP_UIStyleManager.rightLabel);

        if (_customToolsFoldout)
        {
            DrawColorField(FixedFaceLabel, ref _faceColor, ShaderUtilities.ID_FaceColor);

            DrawColorField(FixedOutlineLabel, ref _outlineColor, ShaderUtilities.ID_OutlineColor);

            DrawColorField(FixedUnderlayLabel, ref _underlayColor, ShaderUtilities.ID_UnderlayColor);
        }
    }

    public void DrawColorField(GUIContent label, ref Color color, int shaderPropertyID)
    {
        var newColor = EditorGUILayout.ColorField(label, color, true, true, true);
        if (color != newColor)
        {
            color = newColor;

            m_TextComponent.fontSharedMaterial.SetColor(shaderPropertyID, FixTMPOutlineColor(newColor));
        }
    }

    private Color FixTMPOutlineColor(Color input)
        => new(Mathf.Pow(input.r, 0.4545f), Mathf.Pow(input.g, 0.4545f), Mathf.Pow(input.b, 0.4545f));

    private Color ReverseTMPOutlineColor(Color input)
        => new(Mathf.Pow(input.r, (1 / 0.4545f)), Mathf.Pow(input.g, (1 / 0.4545f)), Mathf.Pow(input.b, (1 / 0.4545f)));
}
