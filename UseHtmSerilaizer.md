#How to use HtmSerializer

##Introduction
The purpose of this document is to explain the methods in the Serializer, in order to have a better understanding of how HtmSerializer works.

##Methods
**Method SerializeBegin:** public void SerializeBegin(String typeName, StreamWriter sw)

This method indicates the start of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

-Expected parameters: 
1. A string called typeName that is the object we want to serialize
2. An object Streamwriter that will write the text to the stream.
-Return value: none 
-Example:
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


**Method SerializeEnd:** public void SerializeEnd(String typeName, StreamWriter sw)

This method indicates the end of the serialize object by writing a variable called "TypeDelimiter" before and after the serialize object

-Expected intput: 
1. A string call typeName that is the object we want to serialize.
2. An object Streamwriter that will write the text to the stream.

-Return value: none  
-Example:
```
public void SerializeEnd(String typeName, StreamWriter sw)
        {
            sw.WriteLine();
       
            sw.Write($"{TypeDelimiter} END '{typeName}' {TypeDelimiter}");
            sw.WriteLine();
        }

```

**Method SerializeValue:**  public void SerializeValue(Dictionary<int, int> keyValues, StreamWriter sw)

This method is formatting the properties of a generic class Dictionary before writing them to a stream. Dictionary is a container class that associates keys with values.

-Expected parameters: 

1. A Dictionary object that contains the key-value pairs to be serialized.
2. StreamWriter object named sw that will be used to write the serialized string to an output stream.
	It is possible to have as an argument instead of the generic class Dictionary a simple variable like a double, int, string, etc. In this case, we will be formatting those arguments passed to the method.

-Return value: none
		

-Example:
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


**Mehtod  Serialize:** public static void Serialize(object obj, string name, StreamWriter sw, Type propertyType = null, List<string> ignoreMembers = null)

This is a method for serializing an object to a file using the StreamWriter class.


-Expected parameters:
1. The object to be serialized.
2. A string representing the name for the object.
3. A StreamWriter object to write the serialized data to.
4. Optional parameters for the property type and a list of members to ignore during serialization.

-Return value: none

-Example:
```
private static void SerializeDistalDendrite(object obj, string name, StreamWriter sw)
        {
            var ignoreMembers = new List<string> { nameof(DistalDendrite.ParentCell) };
            SerializeObject(obj, name, sw, ignoreMembers);

            var cell = (obj as DistalDendrite).ParentCell;

            if (isCellsSerialized.Contains(cell.Index) == false)
            {
                isCellsSerialized.Add(cell.Index);
                Serialize((obj as DistalDendrite).ParentCell, nameof(DistalDendrite.ParentCell), sw, ignoreMembers: ignoreMembers);
            }
        }
```

**Mehtod  SerializeObject:** public static void SerializeObject(object obj, string name, StreamWriter sw, List ignoreMembers = null)


-Expected parameters:
1. The object to be serialized.
2. A string representing the name for the object.
3. A StreamWriter object to write the serialized data to.
4. A list of members

-Return value: none


**Method SerializeHtmConfig:** private static void SerializeHtmConfig(object obj, string name, StreamWriter sw)

This is a helper method called by the main Serialize method to serialize a HtmConfig object.

-Expected parameters:
1. The object to be serialized.
2. A string representing the name for the object.
3. A StreamWriter object to write the serialized data to.

-Return value: none

-Example:
```
private static void SerializeHtmConfig(object obj, string name, StreamWriter sw)
        {
            var excludeEntries = new List<string> { nameof(HtmConfig.Random) };
            SerializeObject(obj, name, sw, excludeEntries);

            var htmConfig = obj as HtmConfig;
            Serialize(htmConfig.RandomGenSeed, nameof(HtmConfig.Random), sw);

        }
```


**Mehtod  DeserializeObject:** public Cell DeserializeCell(StreamReader sr)

This method deserialize an object that has been previously serialized.

-Example:
```
public Cell DeserializeCell(StreamReader sr)
        {
            while (sr.Peek() >= 0)
            {
                string data = sr.ReadLine();

                if (data == ReadBegin(nameof(Cell)))
                {
                    Cell cell1 = Cell.Deserialize(sr);

                    DistalDendrite distSegment1 = cell1.DistalDendrites[0];

                    DistalDendrite distSegment2 = cell1.DistalDendrites[1];

                    distSegment1.ParentCell = cell1;
                    distSegment2.ParentCell = cell1;

                    return cell1;
                }
            }
            return null;
        }
```
-Expected parameters: a StreamReader object
-Return value: in the example above the method returns a single Cell object, although it is possible to have another output as an array of Cell object. (Cell[])


**Method Deserialize** :public static T Deserialize<T>(StreamReader sr, string propName = null)















A serialized string representation of the method´s first argument written to the output stream specified by the sw StreamWriter object. The format of the string will be in accordance with the code inside the method.