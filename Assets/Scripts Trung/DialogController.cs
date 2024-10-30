using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private Dialog[] ponder;
    [SerializeField] 
    private Dialog[] greetingsAndObjective1;
    [SerializeField]
    private Dialog[] objective1Completed;
    [SerializeField]
    private Dialog[] objective2;
    [SerializeField]
    private Dialog[] objective2Completed;
    [SerializeField]
    private Dialog[] objective3;
    [SerializeField]
    private Dialog[] objective3Completed;
    private Dialog[] end;

    [SerializeField]
    private GameObject dialogPanel;
    [SerializeField]
    private TMP_Text dialogText;

    private Dialog[] currentDialogs;

    [SerializeField]
    private ObjectiveController objectiveController;

    public void setCurrentDialogs(Dialog[] dialogs)
    {
        currentDialogs = dialogs;
        SetDialog(currentDialogs[0]);
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
    }
    

    public void SetDialog(Dialog dialog)
    {
        dialogText.text = dialog.text;

        if (dialog.isSelfDialogue) {
            // italicize the text
            dialogText.fontStyle = FontStyles.Italic;
        } else {
            // remove italicize
            dialogText.fontStyle = FontStyles.Normal;
        }

        dialogText.alignment = dialog.isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
        
        dialogPanel.SetActive(true);
    }
}
