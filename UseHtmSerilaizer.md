***How to use HtmSerializer***

#Introduction
The purpose of this document is to explain the methods in the Serializer, in order to have a better understanding of how HtmSerializer works.

#Methods

##Method SerializeBegin: this method indicates the start of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

###Expected intput: a string call typeName that is the object we want to serialize, and an object Streamwriter that will write the text to the stream.
###Expected output: a stream or file  
###Example
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


##Method SerializeEnd: this method indicates the end of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

###Expected intput: a string call typeName that is the object we want to serialize, and an object Streamwriter that will write the text to the stream.
###Expected output: a stream or file  
###Example
```
public void SerializeEnd(String typeName, StreamWriter sw)
        {
            sw.WriteLine();
       
            sw.Write($"{TypeDelimiter} END '{typeName}' {TypeDelimiter}");
            sw.WriteLine();
        }

```
##Method SerializeValue: this method is formatting the properties of a generic class Dictionary before writing them to a stream. This class 
It is possible to have as an argument instead of the generic class Dictionary a simple variable like a double, int, string, etc. The format of the variable will depend on the code inside the method. 


###Expected intput: 
###Expected output: it will depend on the inputs and the type of formatting, meaning...
###Example
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