using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON Variants (one per quest iteration)")]
    [SerializeField] private TextAsset[] inkJSONVariants;

    [Header("Thank You Ink (used if quest has already been completed once)")]
    [SerializeField] private TextAsset thankYouInk;

    [Header("Quest Name (must match exactly: nun, sheriff, builder, sandcastle)")]
    [SerializeField] private string questName;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.I))
            {
                QuestManager qm = QuestManager.GetInstance();

                bool showProgressDialogue = questName.ToLower() switch
                {
                    "nun" => qm.NunHasBeenReset,
                    "sheriff" => qm.SheriffHasBeenReset,
                    "builder" => qm.BuilderHasBeenReset,
                    "sandcastle" => qm.SandcastleHasBeenReset,
                    _ => false
                };

                if (showProgressDialogue)
                {
                    int iter = GetQuestIteration();
                    int clampedIndex = Mathf.Clamp(iter, 0, inkJSONVariants.Length - 1);

                    if (inkJSONVariants.Length > 0 && inkJSONVariants[clampedIndex] != null)
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(inkJSONVariants[clampedIndex]);
                    }
                    else
                    {
                        Debug.LogWarning($"No Ink JSON found for quest '{questName}' at index {clampedIndex}.");
                    }
                }
                else
                {
                    if (thankYouInk != null)
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(thankYouInk);
                    }
                    else
                    {
                        Debug.LogWarning($"No thank-you Ink file assigned for quest '{questName}'.");
                    }
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private int GetQuestIteration()
    {
        QuestManager qm = QuestManager.GetInstance();
        return questName.ToLower() switch
        {
            "nun" => qm.NunIteration,
            "sheriff" => qm.SheriffIteration,
            "builder" => qm.BuilderIteration,
            "sandcastle" => qm.SandcasteIteration,
            _ => 0
        };
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}