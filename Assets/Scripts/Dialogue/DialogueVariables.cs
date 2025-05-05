using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset loadGlobalsJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void StartListening(Story story)
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
        }
        else
        {
            variables.Add(name, value);
        }

        Debug.Log($"Variable changed: {name} = {value}");

        // Trigger quest start if a recognized quest variable is set to true
        if (value is BoolValue boolVal && boolVal.value == true)
        {
            switch (name)
            {
                case "nun":
                case "sheriff":
                case "builder":
                case "sandcastle":
                    QuestManager.GetInstance()?.StartQuest(name);
                    break;
            }
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
            Debug.Log(variable.Key);
            Debug.Log(variable.Value);
        }
        
    }

    public void SetVariable(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
        }
        else
        {
            variables.Add(name, value);
        }

        Debug.Log("Set global Ink variable: " + name + " = " + value);
    }

    public void SetVariable(string name, Ink.Runtime.Object value, Story story)
    {
        SetVariable(name, value); // update global cache

        if (story != null && story.variablesState != null)
        {
            story.variablesState.SetGlobal(name, value);
            Debug.Log("Updated story variable: " + name + " = " + value);
        }
    }
    public void ResetAllGlobalsToFalse(Story story = null)
    {
        string[] keysToReset = { "nun", "sheriff", "builder", "sandcastle" };

        foreach (string key in keysToReset)
        {
            var value = new Ink.Runtime.BoolValue(false);
            SetVariable(key, value, story);
        }

        Debug.Log("All global variables reset to false.");
    }

}