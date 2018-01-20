using System;

[Serializable]
public class AttributesValues
{
    public AttributeValue[] Values;
}

[Serializable]
public class AttributeValue
{
    public string Key;
    public object Value;
}

