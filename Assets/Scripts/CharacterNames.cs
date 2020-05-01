using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterNames
{
    NotSelected, 
    Anne, 
    Mads, 
    MadMads,
    Olive, 
    Dottie, 
    Hamster,
    Siggie,
    Interactable
}

public static class CharacterNameFunctions
{
    public static string ToString(CharacterNames name)
    {
        switch (name)
        {
            case CharacterNames.Anne:
                return "Anne";

            case CharacterNames.Mads:
                return "Mads";

            case CharacterNames.Olive:
                return "Olive";

            case CharacterNames.Dottie:
                return "Dottie";

            case CharacterNames.Hamster:
                return "Hamster";

            case CharacterNames.MadMads:
                return "MadMads";

            case CharacterNames.Siggie:
                return "Siggie";

            case CharacterNames.Interactable:
                return "Interactable";

            case CharacterNames.NotSelected:
            return "NotSelected";
        }

        return null;
    }

    public static CharacterNames FromString(string name)
    {
        Debug.Log(name);
        switch (name)
        {
            case "Anne":
                return CharacterNames.Anne;

            case "Mads":
                return CharacterNames.Mads;

            case "Olive":
                return CharacterNames.Olive;

            case "Dottie":
                return CharacterNames.Dottie;

            case "Hamster":
                return CharacterNames.Hamster;

            case "MadMads":
                return CharacterNames.MadMads;

            case "Siggie":
                return CharacterNames.Siggie;

            case "Interactable":
                return CharacterNames.Interactable;

            case "NotSelected":
            return CharacterNames.NotSelected;
        }

        return CharacterNames.NotSelected;
    }
}
