using System;

[System.Serializable]
public class Choice
{
    public string id;
    public string speaker;        
    public string text;           
    public ChoiceOption[] options; 
}

[System.Serializable]
public class ChoiceOption
{
    public string optionText;  
    public string nextChoiceID;     // 成功/默认
    public string failChoiceID;     // 失败
    public string skillCheck;       // 判定
    public int minRollRequirement;
}
