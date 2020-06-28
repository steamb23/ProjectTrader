using UnityEngine;
using System.Collections;
using TMPro;
using JetBrains.Annotations;
using System;

public class EmployeeInfo : MonoBehaviour
{
    [Serializable]
    public class EmploemyeeData
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public string age;
        [SerializeField]
        public string charisma;
        [SerializeField]
        public string inteligent;
        [SerializeField]
        public string dexterity;
        [SerializeField]
        public string state;
        [SerializeField]
        public int code;
    }

    [SerializeField]
    TextMeshProUGUI nameField;
    [SerializeField]
    TextMeshProUGUI ageField;
    [SerializeField]
    TextMeshProUGUI charismaField;
    [SerializeField]
    TextMeshProUGUI inteligentField;
    [SerializeField]
    TextMeshProUGUI dexterityField;
    [SerializeField]
    TextMeshProUGUI stateField;

    [Space]
    [SerializeField]
    EmploemyeeData employeeData;

    public string Name
    {
        get => employeeData.name;
        set => nameField.text = employeeData.name = value;
    }

    public string Age
    {
        get => employeeData.age;
        set => ageField.text = employeeData.age = value;
    }

    public string Charisma
    {
        get => employeeData.charisma;
        set => charismaField.text = employeeData.charisma = value;
    }

    public string Inteligent
    {
        get => employeeData.inteligent;
        set => inteligentField.text = employeeData.inteligent = value;
    }

    public string Dexturity
    {
        get => employeeData.dexterity;
        set => dexterityField.text = employeeData.dexterity = value;
    }

    public string State
    {
        get => employeeData.state;
        set => stateField.text = employeeData.state = value;
    }

    public int Code
    {
        get => employeeData.code;
        set => employeeData.code = value;
    }

    public void ClearProperties()
    {
        Name = null;
        Age = null;
        Charisma = null;
        Inteligent = null;
        Dexturity = null;
        State = null;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        if (nameField == null ||
            ageField == null ||
            charismaField == null ||
            inteligentField == null ||
            dexterityField == null ||
            stateField == null)
        {
            Debug.LogError("EmployeeInfo가 제대로 초기화되지 않았습니다.");
        }
        else
        {
            Name = Name;
            Age = Age;
            Charisma = Charisma;
            Inteligent = Inteligent;
            Dexturity = Dexturity;
            State = State;
        }
    }
}
