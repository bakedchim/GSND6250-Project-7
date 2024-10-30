using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog: ScriptableObject
{
    public bool isLeft;
    public string text;
    public bool isSelfDialogue;
}