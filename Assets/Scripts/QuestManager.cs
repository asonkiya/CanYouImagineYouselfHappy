using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;

    [Header("Teleport Targets")]
    [SerializeField] private GameObject nunTarget;
    [SerializeField] private GameObject sheriffTarget;
    [SerializeField] private GameObject builderTarget;
    [SerializeField] private GameObject sandcastleTarget;

    [Header("Teleport Destinations")]
    [SerializeField] private Transform nunDestination;
    [SerializeField] private Transform sheriffDestination;
    [SerializeField] private Transform builderDestination;
    [SerializeField] private Transform sandcastleDestination;

    public static QuestManager GetInstance()
    {
        return instance;
    }

    public bool NunInProgress { get; private set; }
    public bool SheriffInProgress { get; private set; }
    public bool BuilderInProgress { get; private set; }
    public bool SandcasteInProgress { get; private set; }

    public int NunIteration { get; private set; }
    public int SheriffIteration { get; private set; }
    public int BuilderIteration { get; private set; }
    public int SandcasteIteration { get; private set; }

    public bool NunHasBeenReset { get; private set; }
    public bool SheriffHasBeenReset { get; private set; }
    public bool BuilderHasBeenReset { get; private set; }
    public bool SandcastleHasBeenReset { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one QuestManager");
        }
        instance = this;
    }

    void Start()
    {
        NunInProgress = false;
        SheriffInProgress = false;
        BuilderInProgress = false;
        SandcasteInProgress = false;

        NunIteration = 0;
        SheriffIteration = 0;
        BuilderIteration = 0;
        SandcasteIteration = 0;

        NunHasBeenReset = true;
        SheriffHasBeenReset = true;
        BuilderHasBeenReset = true;
        SandcastleHasBeenReset = true;

        Physics2D.IgnoreLayerCollision(3, 8, true);  // Player ↔ Nun
        Physics2D.IgnoreLayerCollision(3, 9, true);  // Player ↔ Sheriff
        Physics2D.IgnoreLayerCollision(3, 7, true);  // Player ↔ Builder
        Physics2D.IgnoreLayerCollision(3, 10, true); // Player ↔ Sand
    }

    void Update() { }

    public void StartQuest(string questName)
    {
        switch (questName.ToLower())
        {
            case "nun":
                NunInProgress = true;
                Physics2D.IgnoreLayerCollision(3, 8, false);
                NunIteration++;
                NunHasBeenReset = false;
                break;
            case "sheriff":
                SheriffInProgress = true;
                Physics2D.IgnoreLayerCollision(3, 9, false);
                SheriffIteration++;
                SheriffHasBeenReset = false;
                break;
            case "builder":
                BuilderInProgress = true;
                Physics2D.IgnoreLayerCollision(3, 7, false);
                BuilderIteration++;
                BuilderHasBeenReset = false;
                break;
            case "sandcastle":
                SandcasteInProgress = true;
                Physics2D.IgnoreLayerCollision(3, 10, false);
                SandcasteIteration++;
                SandcastleHasBeenReset = false;
                break;
            default:
                Debug.LogWarning("Unknown quest: " + questName);
                break;
        }
    }

    public void EndQuest(string questName)
    {
        var dialogueManager = DialogueManager.GetInstance();

        switch (questName.ToLower())
        {
            case "nun":
                NunInProgress = false;
                Physics2D.IgnoreLayerCollision(3, 8, true);
                dialogueManager?.dialogueVariables?.SetVariable("nun", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
                break;
            case "sheriff":
                SheriffInProgress = false;
                Physics2D.IgnoreLayerCollision(3, 9, true);
                dialogueManager?.dialogueVariables?.SetVariable("sheriff", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
                break;
            case "builder":
                BuilderInProgress = false;
                Physics2D.IgnoreLayerCollision(3, 7, true);
                dialogueManager?.dialogueVariables?.SetVariable("builder", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
                break;
            case "sandcastle":
                SandcasteInProgress = false;
                Physics2D.IgnoreLayerCollision(3, 10, true);
                dialogueManager?.dialogueVariables?.SetVariable("sandcastle", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
                break;
            default:
                Debug.LogWarning("Unknown quest: " + questName);
                break;
        }
    }

    public void AbortAllQuests()
    {
        EndQuest("nun");
        EndQuest("sheriff");
        EndQuest("builder");
        EndQuest("sandcastle");
    }

    public void ResetAllQuests()
    {
        AbortAllQuests();

        if (nunTarget && nunDestination)
            nunTarget.transform.position = nunDestination.position;
        if (sheriffTarget && sheriffDestination)
            sheriffTarget.transform.position = sheriffDestination.position;
        if (builderTarget && builderDestination)
            builderTarget.transform.position = builderDestination.position;
        if (sandcastleTarget && sandcastleDestination)
            sandcastleTarget.transform.position = sandcastleDestination.position;

        var dialogueManager = DialogueManager.GetInstance();
        dialogueManager?.dialogueVariables?.SetVariable("nun", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
        dialogueManager?.dialogueVariables?.SetVariable("sheriff", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
        dialogueManager?.dialogueVariables?.SetVariable("builder", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);
        dialogueManager?.dialogueVariables?.SetVariable("sandcastle", new Ink.Runtime.BoolValue(false), dialogueManager.CurrentStory);

        NunHasBeenReset = true;
        SheriffHasBeenReset = true;
        BuilderHasBeenReset = true;
        SandcastleHasBeenReset = true;
    }
}