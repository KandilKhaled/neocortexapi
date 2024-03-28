# SERIALIZING-DESERIALIZING METHODS (from line 1000 to line 1400) 

## **Method 1 (1039-1169) : DeserializeObject**
    This is a method in C# that deserializes an object of a generic type 'T' from a StreamReader. 
	
    -Expected input: The method takes in a StreamReader, a property name, an optional list of fields to exclude during deserialization, and an optional action to perform on fields that are excluded during deserialization.
	
	-Expected output: The expected output of this C# method is an object of type T that has been deserialized from a string representation. 
    If the deserialization process is successful, this method should return the deserialized object with its properties and fields set to the values specified in the input string.
	
```` 
    public static T DeserializeObject<T>(StreamReader sr, string propertyName, List<string> excludeEntries = null,Action<T,string> action = null)
        {
            var type = typeof(T);
            T obj = default;

            if (type.IsInterface || type.IsAbstract)
            {
                return obj;
            }

            obj = (T)Activator.CreateInstance(type);

            var properties = GetProperties(type);
            var fields = GetFields(type);
            while (sr.Peek() > 0)
            {

                var content = sr.ReadLine().Trim();
                //Debug.WriteLine(content);
                if (content == ReadGenericBegin(propertyName))
                {

                    continue;
                }
                if (content == ReadGenericEnd(propertyName) || content == ReadGenericEnd(propertyName, type))
                {
                    break;
                }

                var components = content.Split(' ');

                if (content.StartsWith("Begin"))
                {
                    if (content == ReadGenericBegin(cReplaceId))
                    {
                        var replaceHashCode = Deserialize<int>(sr, cReplaceId);
                        if (MapObjectHashCode.TryGetValue(replaceHashCode, out object replaceedObj))
                        {
                            obj = (T)replaceedObj;
                        }
                        else
                        {
                            throw new KeyNotFoundException($"Object with hash code {replaceHashCode} cannot be found!");
                        }
                    }
                    if (content == ReadGenericBegin(cIdString))
                    {
                        var hashcode = Deserialize<int>(sr, cIdString);
                        MapObjectHashCode.TryAdd(hashcode, obj);
                        continue;
                    }
                    if (components.Length == 2)
                    {
                        if (excludeEntries == null || excludeEntries.Contains(components[1]) == false)
                        {
                            var property = properties.FirstOrDefault(p => p.Name == components[1]);
                            if (property != null && property.CanWrite)
                            {
                                var deserializeMethod = typeof(HtmSerializer).GetMethod(nameof(HtmSerializer.Deserialize)).MakeGenericMethod(property.PropertyType);

                                var propertyValue = deserializeMethod.Invoke(null, new object[] { sr, property.Name });
                                property.SetValue(obj, propertyValue);
                            }
                            else
                            {
                                var field = fields.FirstOrDefault(f => f.Name == components[1]);
                                if (field != null && field.IsInitOnly == false)
                                {
                                    var deserializeMethod = typeof(HtmSerializer).GetMethod(nameof(HtmSerializer.Deserialize)).MakeGenericMethod(field.FieldType);

                                    var fieldValue = deserializeMethod.Invoke(null, new object[] { sr, field.Name });
                                    field.SetValue(obj, fieldValue);
                                }
                            }
                        }
                        else
                        {
                            // Deserialize the excluded fields or properties
                            action?.Invoke(obj, components[1]);
                        }
                    }
                    else if (components.Length == 3)
                    {
                        if (components[1] == "m_PredictiveCells")
                        {

                        }
                        if (excludeEntries == null || excludeEntries.Contains(components[1]) == false)
                        {
                            var property = properties.FirstOrDefault(p => p.Name == components[1]);
                            if (property != null && property.CanWrite)
                            {
                                var implementedType = GetType(components[2]);

                                if (implementedType != null)
                                {
                                    var deserializeMethod = typeof(HtmSerializer).GetMethod(nameof(HtmSerializer.Deserialize)).MakeGenericMethod(implementedType);

                                    var propertyValue = deserializeMethod.Invoke(null, new object[] { sr, property.Name });
                                    property.SetValue(obj, propertyValue);
                                    //Debug.WriteLine($"set {propertyName ?? type.Name}.{property.Name} to {propertyValue.ToString()}");
                                }
                            }
                            else
                            {
                                var field = fields.FirstOrDefault(f => f.Name == components[1]);
                                if (field != null && field.IsInitOnly == false)
                                {
                                    var implementedType = GetType(components[2]);

                                    if (implementedType != null)
                                    {
                                        var deserializeMethod = typeof(HtmSerializer).GetMethod(nameof(HtmSerializer.Deserialize)).MakeGenericMethod(implementedType);

                                        var fieldValue = deserializeMethod.Invoke(null, new object[] { sr, field.Name });
                                        field.SetValue(obj, fieldValue);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Deserialize the excluded fields or properties
                            action?.Invoke(obj, components[1]);
                        }
                    }
                }
            }
               return obj;
        }
````
   -Code Explanation:
   The method first checks if the type is an interface or abstract and if it is, it returns the default value of the type. It then creates an instance of the type using Activator.CreateInstance, gets the properties and fields of the type, and reads the StreamReader line by line. 
   It then checks for specific keywords in the line and performs different actions based on the keyword. If the line starts with "Begin", it checks for specific keywords and deserializes different properties or fields accordingly. 
   It also keeps track of the objects deserialized using a hashcode and stores them in a map.

## **METHOD 2 (1184-1198): DeserializeHtmConfig**
   This is a C# method that deserializes an object of type "HtmConfig" from a stream reader.
   
   -Expected inputs:
   The method takes in two parameters: "sr" which is the stream reader and "propertyName" which is the name of the property, type is string.
   A StreamReader object "sr" which is used to read a stream of data, usually a file or network stream. The method reads the data from the stream and deserializes it into an object of type "HtmConfig".
   A string "propertyName", which is used as a reference to the property being deserialized. This string is passed along to other methods called within this method such as "DeserializeObject" and "Deserialize".
   
   -Expected output: This method doesn't have any expected output since it returns an object of type object and its behavior depends on the implementation details of the methods called within it. 
   
````
    private static object DeserializeHtmConfig(StreamReader sr, string propertyName)
  
        {
            var excludeEntries = new List<string> { nameof(HtmConfig.Random) };
            var htmConfig = DeserializeObject<HtmConfig>(sr, propertyName, excludeEntries, (config, propertyName) =>
            {
                if (propertyName == nameof(HtmConfig.Random))
                {
                    var seed = Deserialize<int>(sr, propertyName);
                    config.Random = new ThreadSafeRandom(seed);
                }
            });
    
            return htmConfig;
       }
````
    -Code Explanation:
    The method creates a new list of strings called "excludeEntries" which contains the name of the property "Random" and uses it to exclude it from the deserialization process.
    The method then creates a new instance of "ThreadSafeRandom" using the seed and assigns it to the "Random" property of the "HtmConfig" object.
    Finally, the method returns the deserialized "HtmConfig" object.
    Additionally, the method relies on other methods such as "DeserializeObject" and "Deserialize", which are necessary to understand the full functionality of this method.
	
## **METHOD 3 (1200-1226): DeserializeValue** 
    This method appears to be deserializing a single value of a specified generic type "T" from a StreamReader (the StreamReader "sr" and the contents of the stream it is reading from).
    
	-Expected input: The input of this code is a StreamReader "sr" and a string "propertyName". 
	The StreamReader reads the content from a stream and the string "propertyName" is the name of the property being deserialized.
    
	-Expected output: The output will depend on the specific type of the generic parameter "T" that is passed to the method.
	
````
        private static T DeserializeValue<T>(StreamReader sr, string propertyName)
        {
            T obj = default;
            while (sr.Peek() > 0)
            {
                var content = sr.ReadLine().Trim();

                if (content == ReadGenericEnd(propertyName))
                {
                    break;
                }
                if (content == ReadGenericBegin(propertyName))
                {
                    continue;
                }

                var type = typeof(T);

                if (type == typeof(string))
                {
                    content = content.Replace("\\n", "\n");
                }
                obj = (T)Convert.ChangeType(content, type);
    
            }
            return obj;
        }
        #endregion
````
    -Code Explanation:
    The method reads lines from the StreamReader until it reaches a line that matches the end marker for the property, and uses the Convert.ChangeType method to convert each line read to the specified type "T" before returning the final converted value.
    A special case is made for strings, where newline characters "\n" are replaced with actual newlines "\n" before being casted to the type T.
    As well as the implementation of the "ReadGenericEnd" and "ReadGenericBegin" methods used within the method. 
    
## **METHOD 4 (1265-1284): SerializeValue**
    This code is a method that serializes an object to a text stream using a StreamWriter.
	
    -Expected inputs: The method takes input in three parameters: the object to be serialized, the type of the object, and the StreamWriter to write the serialized object to.
    The method first checks if the object is a value type (such as an int or a struct) using the IsValueType property.
	
	-Expected output: The output of this code is the serialization of the input object in the form of text written to the input StreamWriter.
    It does not return anything, it just writes the serialized form of the object to the StreamWriter.
	
 ````
    public void SerializeValue(object val, Type type, StreamWriter sw)
        {
            if (type.IsValueType)
            {
                sw.Write(ValueDelimiter);
                sw.Write(val.ToString());
                sw.Write(ValueDelimiter);
                sw.Write(ParameterDelimiter);
   
            }
            else
            {
                var method = type.GetMethod("Serialize", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (method != null)
                {
                    method.Invoke(val, new object[] { sw });
  
                }
                else
                    throw new NotSupportedException($"No serialization implemented on the type {type}!");
 
            }
        }
````
    -Code Explanation:
    If it is a value type, it writes the value of the object to the StreamWriter surrounded by a delimiter.
    If it is not a value type, it looks for a method called "Serialize" on the object's type and invokes it, passing in the StreamWriter as a parameter.
    If no such method is found, it throws a NotSupportedException.
    
## **METHOD 5 (1286-1306): DeserializeValue**
   This method is a generic method that deserializes a value of type T from a StreamReader.
   
   -Expected input: The input of the DeserializeValue method is a StreamReader object named sr. 
   The method is designed to read from this stream and deserialize the data read into an object of type T. 
   The type T is a generic type parameter and is specified when calling the method.
   
   -Expected output: We need StreamReader and the value of the ParameterDelimiter for output.
````
       public T DeserializeValue<T>(StreamReader sr)
        {
            Type type = typeof(T);
       if (type.IsValueType)
            {       
                var reader = sr.ReadLine().Trim().Replace(ParameterDelimiter.ToString(), "");
                return (T)Convert.ChangeType(reader, type);
            }
            else
            {
                var method = type.GetMethod("Deserialize");
                if (method != null)
                {
                    return (T)method.Invoke(null, new object[] { sr });
                }
                else
                    throw new NotSupportedException($"No de-serialization implemented on the type {type}!");
 
            }
       }
````
   -Code Explanation:
   If T is a value type, the method reads a line from the StreamReader, trims it, and replaces a specified delimiter with an empty string.
   Then it converts the string to the type T and returns it.
   If T is not a value type, the method uses reflection to look for a method called "Deserialize" on the type T, and if it exists, it invokes that method and returns the result.
   If the "Deserialize" method does not exist, it throws a NotSupportedException.
   
## **METHOD 6 (1332-1356): DeserializeSynapse**
  This code is deserializing a Synapse object from a text file using a StreamReader.
  
  -Expected input: The input of this code is a StreamReader object that is connected to a text file. 
  The text file should contain serialized information about a Synapse object in a specific format that is understood by the "Deserialize" method of the Synapse class and the "ReadBegin" method. 
  
  -Expected output: The output of this code would be an instance of the Synapse class that has been deserialized from a text file using a StreamReader. 
  If it could not find the string returned by "ReadBegin" method with the parameter "nameof(Synapse)", it would return null.
  
 ````
    public Synapse DeserializeSynapse(StreamReader sr)
        {
            while (sr.Peek() >= 0)
            {
                string data = sr.ReadLine();

                if (data == ReadBegin(nameof(Synapse)))
                {
                    Synapse synapseT1 = Synapse.Deserialize(sr);

                    Cell cell1 = synapseT1.SourceCell;
                    DistalDendrite distSegment1 = synapseT1.SourceCell.DistalDendrites[0];

                    DistalDendrite distSegment2 = synapseT1.SourceCell.DistalDendrites[1];

                    distSegment1.ParentCell = cell1;
                    distSegment2.ParentCell = cell1;
                    synapseT1.SourceCell = cell1;

                    return synapseT1;
                }
            }
            return null;
        }
````
    -Code Explanation:
    It reads lines of data from the file until it reaches a line that matches the string returned by the "ReadBegin" method with the parameter "nameof(Synapse)".
    It then calls the "Deserialize" method on the Synapse class, passing in the StreamReader.
	The resulting Synapse object is stored in the "synapseT1" variable. The code then gets the SourceCell property of "synapseT1" and assigns it to "cell1".
	It also gets the DistalDendrites property of "cell1" at index 0 and 1 and assigns them to "distSegment1" and "distSegment2" respectively.
	Finally, it sets the ParentCell property of "distSegment1" and "distSegment2" to "cell1" and the SourceCell property of "synapseT1" to "cell1" and returns "synapseT1".
	If the end of the file is reached, the function returns null.
    
## **METHOD 7 (1363-1385): DeserializeDistalDendrite**
   This is a method in C# that deserializes a DistalDendrite object from a StreamReader object.
   
   -Expected input: The input of this code is a StreamReader object, which is passed as an argument to the method. 
   The StreamReader object is used to read the data that is used to deserialize the DistalDendrite object. 
   
   -Expected output: The output of this code is a DistalDendrite object if the marker is found in the stream otherwise it will return null.
   
 ````
     public DistalDendrite DeserializeDistalDendrite(StreamReader sr)
        {
            while (sr.Peek() >= 0)
            {
                string data = sr.ReadLine();

                if (data == ReadBegin(nameof(DistalDendrite)))
                {

                    DistalDendrite distSegment1 = DistalDendrite.Deserialize(sr);

                    Cell cell1 = distSegment1.ParentCell;
                    distSegment1 = distSegment1.ParentCell.DistalDendrites[0];
                    distSegment1.ParentCell = cell1;
                    DistalDendrite distSegment2 = distSegment1.ParentCell.DistalDendrites[1];
                    distSegment2.ParentCell = cell1;

                    return distSegment1;
                }
            }
            return null;
 
        }
````
   -Code Explanation:
   The method reads lines of data from the StreamReader and checks if the line of data is the start of a DistalDendrite object. 
   If it is, it creates a DistalDendrite object using the  DistalDendrite.
   Deserialize method and sets its ParentCell property.
   It then sets the ParentCell property of the first and second DistalDendrite in the ParentCell.DistalDendrites list. Finally, it returns the first DistalDendrite object.
   If no DistalDendrite is found, it returns null.
   This code is a method that appears to deserialize a DistalDendrite object from a StreamReader object, and sets the ParentCell property of the object and its first two DistalDendrites to the same Cell object, and returns the first DistalDendrite object. 
   If the marker string "ReadBegin(nameof(DistalDendrite))" is not found in the stream, the method will return null. 
  
## **METHOD 8 (1392-1398): SerializeValue**
   This is a method that serializes a double value to a StreamWriter.
   
   -Expected input: The expected input for this method is a double value (named val) that needs to be serialized (converted into a format that can be stored or transmitted). 
   Additionally, the method requires a StreamWriter object (named sw) that represents the destination where the serialized value will be written to.
   
   -Expected output: The output of this method is the serialized string representation of the val parameter, which is written to the specified StreamWriter object (sw).
   
 ````
    public void SerializeValue(double val, StreamWriter sw) 
        {
            sw.Write(ValueDelimiter);
            sw.Write(string.Format(CultureInfo.InvariantCulture, "{0:0.000}", val));
            sw.Write(ValueDelimiter);
            sw.Write(ParameterDelimiter);
        }
 ````
   -Code Explanation:
   The method starts by writing a ValueDelimiter to the StreamWriter, then it formats the double value using the CultureInfo.InvariantCulture and a format of "0.000" and writes it to the StreamWriter. 
   After that, it writes another ValueDelimiter and a ParameterDelimiter to the StreamWriter. This method is likely used to write double values to a file or other stream in a specific format.
   The output of this code would be the double value passed as an argument to the method, formatted as a string with three decimal places, preceded by a ValueDelimiter and followed by a ValueDelimiter and a ParameterDelimiter. 
   For example, if the method is called with the value of 1.234, the output written to the StreamWriter would be something like this: "|1.234|,".
   It is important to note that the method does not return any output, it writes the output to the StreamWriter passed as argument.