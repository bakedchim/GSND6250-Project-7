using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private Dialog[] ponder;
    [SerializeField] 
    private Dialog[] greetingsAndQuestion1;
    [SerializeField]
    private Dialog[] question2;
    [SerializeField]
    private Dialog[] question3;
    private Dialog[] end;

    
    [SerializeField]
    private Dialog[] objective1Completed;
    [SerializeField]
    private Dialog[] objective2Completed;
    [SerializeField]
    private Dialog[] objective3Completed;

    [SerializeField]
    private GameObject dialogPanel;
    [SerializeField]
    private TMP_Text dialogText;

    private Dialog[] currentDialogs;
    private int currentDialogIndex = 0;

    [SerializeField]
    private ObjectiveController objectiveController;

    public void StartInteraction(string tag)
    {
        if (tag == "NPC") {
            if (!objectiveController.hasGreeted) {
                SetCurrentDialogs(greetingsAndQuestion1);
                objectiveController.hasGreeted = true;
            } else if (!objectiveController.objective1Complete) {
                SetCurrentDialogs(ponder);
            } else if (!objectiveController.hasReturnObjective1) {
                SetCurrentDialogs(question2);
                objectiveController.hasReturnObjective1 = true;
            } else if (!objectiveController.objective2Complete) {
                SetCurrentDialogs(ponder);
            } else if (!objectiveController.hasReturnObjective2) {
                SetCurrentDialogs(question3);
                objectiveController.hasReturnObjective2 = true;
            } else if (!objectiveController.objective3Complete) {
                SetCurrentDialogs(ponder);
            } else if (!objectiveController.hasReturnObjective3) {
                SetCurrentDialogs(end);
                objectiveController.hasReturnObjective3 = true;
            } else {
                SetCurrentDialogs(end);
            }
        }

        if (tag == "Objective1") {
            SetCurrentDialogs(objective1Completed);
            objectiveController.objective1Complete = true;
        }

        if (tag == "Objective2") {
            SetCurrentDialogs(objective2Completed);
            objectiveController.objective2Complete = true;
        }

        if (tag == "Objective3") {
            SetCurrentDialogs(objective3Completed);
            objectiveController.objective3Complete = true;
        }
    }

    public void AdvanceDialog()
    {
        if (currentDialogIndex == currentDialogs.Length - 1) {
            if (CompareDialogs(currentDialogs, greetingsAndQuestion1)) {
                if (objectiveController.objective1Complete) {
                    SetCurrentDialogs(objective1Completed);
                } else {
                    SetCurrentDialogs(ponder);
                }
            } else if (CompareDialogs(currentDialogs, question2)) {
                if (objectiveController.objective2Complete) {
                    SetCurrentDialogs(objective2Completed);
                } else {
                    SetCurrentDialogs(ponder);
                }
            } else if (CompareDialogs(currentDialogs, question3)) {
                if (objectiveController.objective3Complete) {
                    SetCurrentDialogs(objective3Completed);
                } else {
                    SetCurrentDialogs(ponder);
                }
            } else {
                EndDialog();
            }
            return;
        }

        currentDialogIndex++;
        SetDialog(currentDialogs[currentDialogIndex]);
    }

    private bool CompareDialogs(Dialog[] dialog1, Dialog[] dialog2)
    {
        if (dialog1.Length != dialog2.Length) {
            return false;
        }

        for (int i = 0; i < dialog1.Length; i++) {
            if (dialog1[i].text != dialog2[i].text) {
                return false;
            }
        }

        return true;
    }

    public void SetCurrentDialogs(Dialog[] dialogs)
    {
        currentDialogs = dialogs;
        currentDialogIndex = 0;
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
