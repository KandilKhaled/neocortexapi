***How to use HtmSerializer***

**Introduction**
The purpose of this document is to explain the methods in the Serializer, in order to have a better understanding of how HtmSerializer works.

**Methods**
	Method SerializeBegin: this method indicates the start of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

-Expected intput: a string call typeName that is the object we want to serialize, and an object Streamwriter that will write the text to the stream.
-Expected output: a stream or file  
-Example
```
 public void SerializeBegin(String typeName, StreamWriter sw)
        {
            //
            // -- BEGIN ---
            // typeName
       
            sw.WriteLine();

            sw.Write($"{TypeDelimiter} BEGIN '{typeName}' {TypeDelimiter}");
            sw.WriteLine();

        }
```


	Method SerializeEnd: this method indicates the end of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

-Expected intput: a string call typeName that is the object we want to serialize, and an object Streamwriter that will write the text to the stream.
-Expected output: a stream or file  
-Example
```
public void SerializeEnd(String typeName, StreamWriter sw)
        {
            sw.WriteLine();
       
            sw.Write($"{TypeDelimiter} END '{typeName}' {TypeDelimiter}");
            sw.WriteLine();
        }

```


	Method SerializeValue: this method is formatting the properties of a generic class Dictionary before writing them to a stream. 
 In c++, a dictionary can be implemented as a map, which is a container class that associates keys with values.

-Expected intput: 
		1. A Dictionary object that contains the key-value pairs to be serialized
		2. StreamWriter object named sw that will be used to write the serialized string to an output stream.
		It is possible to have as an argument instead of the generic class Dictionary a simple variable like a double, int, string, etc. In this case, we will be formatting those arguments passed to the method.
-Espected output: a serialized string representation of the method´s first argument written to the output stream specified by the sw StreamWriter object. The format of the string will be in accordance with the code inside the method.

-Example
```
 public void SerializeValue(Dictionary<int, int> keyValues, StreamWriter sw)
        {
            sw.Write(ValueDelimiter);
            foreach (KeyValuePair<int, int> i in keyValues)
            {
                sw.Write(i.Key.ToString() + KeyValueDelimiter + i.Value.ToString());
                sw.Write(ElementsDelimiter);
            }
            sw.Write(ParameterDelimiter);
        }
```

