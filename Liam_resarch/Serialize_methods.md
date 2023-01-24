# Methods explain:

> A serializer in C# is a process or mechanism that converts an object's state to a 
format that can be persisted or transmitted across a network. 
This process is known as serialization, 
and the resulting format is known as a serialized representation of the object.
The reverse process,in which a serialized representation of an object is converted back into an object, 
is known as deserialization. 
C# provides built-in support for serialization and deserialization through various classes and interfaces, 
such as the BinaryFormatter and XmlSerializer classes. These classes can be used to serialize objects to binary or XML formats, respectively.

## 1rst Method
###### SerializeT in Class Cell (a neuron)

It serialize Index, ParentColumnIndex => 2 int 
and if the list DistalDendrite and ReceptorSynapses are non null and have at least 1 element, they are serialize.

## 2nd Method
###### SerializationTest in SpatialPoolerPersistenceTests

The test method SerializationTest() 
uses this Parameters object to create a new SpatialPooler object and a new Connections object.
The test method uses the JsonConvert class to serialize the Connections object to a JSON string and then deserialize it back to a Connections object. 
It also serializes the SpatialPooler object to a JSON string and then deserialize it back to a SpatialPooler object.

***Methods in HtmSerializer***

## Method SerializeBegin(String typeName, StreamWriter sw) line 50
This method takes in two parameters: a typeName string and a StreamWriter object (sw). 
The method serializes the begin marker of the type and writes it to the StreamWriter object.
The method starts by writing a new line to the StreamWriter object using the WriteLine() method. 
Then, it writes a string to the StreamWriter object using the Write() method. 
This string contains the TypeDelimiter, the string " BEGIN '", the typeName variable, the string "' ", and the TypeDelimiter.
For example, if you call this method with the typeName variable set to "MyType" and the sw variable 
set to a StreamWriter object that is writing to a file, it will write the following text to the file:


-- BEGIN -- 'MyType' --

It will also write a new line after the text using sw.WriteLine();

## Method String ReadBegin(string typeName) line 60
This method takes in a single parameter, a typeName string. 
The method creates a new string variable named val by using a string interpolation, which is a way to embed expressions inside string literals. 
It is done by wrapping the expressions inside curly braces {} and prefixing the string with the dollar sign $.

The val variable is assigned with a string that contains the TypeDelimiter, the string " BEGIN '", the typeName variable, the string "' ", and the TypeDelimiter.

Then, the method returns the value of the val variable, which is the string that was just created.

For example, if you call this method with the typeName variable set to "MyType", it will return the following string:

-- BEGIN -- 'MyType' --

## Method Reset() line 66

The method is used to reset the state of the class where this method is defined.

The method has no parameters. It calls the Clear() method on the SerializedHashCodes and MapObjectHashCode dictionaries, which removes all elements from the dictionaries. This effectively resets the state of the dictionaries, removing all the stored hashcodes and the mapped objects.

It also reset Id variable to 0.

This method can be used to clear the state of the class so that it can start fresh, for example, 
if you are serializing a new object or starting a new serialization process.

line 22 and 23 which are use in method Reset()
SerializedHashCodes is a dictionary that maps an object to an integer (hash code). 
The key of the dictionary is of the type object, which means that it can hold any reference type, 
and the value is of the type int, which represents the hash code of the object.

MapObjectHashCode is a dictionary that maps an integer (hash code) to an object. 
The key of the dictionary is of the type int, which represents the hash code of the object, 
and the value is of the type object, which can hold any reference type.

In general, These dictionaries are used to store the hashcodes of the objects that are serialized to keep track of the objects that are already serialized, 
so that the same object is not serialized multiple times.

# Methods explain from line 479 of HtmSerializer

## Method Serialize line 487 to 550 
Here is the beginning of the code to understand which parameters takes the methods
 /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="sw"></param>
        /// <param name="propertyType"></param>
        /// <param name="ignoreMembers"></param>
        public static void Serialize(object obj, string name, StreamWriter sw, Type propertyType = null, List<string> ignoreMembers = null)

This is a method for serializing an object to a file using the StreamWriter class. 
The method takes in several parameters including the object to be serialized, a name for the object, 
a StreamWriter object to write the serialized data to, and optional parameters for the property type and a list of members to ignore during serialization. 
The method first checks if the object is null and returns if it is. 
It then determines the type of the object and checks if it implements the ISerializable interface or is a primitive or string type. 
If so, it calls the appropriate method for serializing the object. If the object is a dictionary or list, it calls the appropriate method for serializing those types. 
If the object is a KeyValuePair, it calls the SerializeKeyValuePair method. If the object is a class, 
it checks if the object has been serialized before and uses the serialized version if it has. 
If not, it assigns an ID to the object and adds it to the SerializedHashCodes dictionary, then calls the SerializeObject method to serialize the object. 
Finally, it calls the SerializeEnd method to close the serialized data.

## Method SerializeObject line 554 to 607
public static void SerializeObject(object obj, string name, StreamWriter sw, List<string> ignoreMembers = null)

This is a **helper** method called by the main Serialize method to serialize the properties and fields of a class object. 
It takes in the same parameters as the main Serialize method, plus an optional list of members to ignore during serialization. 
It starts by checking if the object passed in is null, and if it is, it returns immediately. 
It then gets the type of the object and creates lists of the object's properties and fields using the GetProperties and GetFields methods.
It then loops through the properties and fields, checking if the current property or field is in the ignore list or if the property can't be written to. 
If either of these conditions are true, it continues to the next property or field. Otherwise, 
it gets the value of the property or field and calls the main Serialize method to serialize it. 
If it throws an exception while trying to read the property or field, it will print a warning message.

## Method  GetFields from 609 to 617

private static List<FieldInfo> GetFields(Type type)
        {
            var fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(f => f.GetCustomAttribute<CompilerGeneratedAttribute>() == null).ToList();
            if (type.BaseType != null)
            {
                fields.AddRange(type.BaseType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(f => f.GetCustomAttribute<CompilerGeneratedAttribute>() == null));
            }

            return fields;
        }
This is a **helper** method called by the SerializeObject method, which is used to get the fields of a specific type. 
The method takes in a single parameter, "type", which is the Type of the object for which the fields are to be retrieved.
The method starts by using the Type.GetFields method to get all fields of the type, with specific binding flags passed in to only include declared instance fields, 
both public and non-public. The method filters out fields that have the CompilerGeneratedAttribute using LINQ Where method and then converts the result into a List.
Then it check if the type has a base type and if it does, it adds all the fields of the base type to the fields list using GetFields method.
It returns the final list of fields, which can be used to iterate through the fields of an object and retrieve their values.

## Method GetProperties from 620 to 629

private static List<PropertyInfo> GetProperties(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
            //if (type.BaseType != null)
            //{
            //    properties.AddRange(type.BaseType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
            //}

            return properties;
        }
This is a **helper** method called by the SerializeObject method, which is used to get the properties of a specific type. The method takes in a single parameter, 
"type", which is the Type of the object for which the properties are to be retrieved.
The method starts by using the Type.GetProperties method to get all properties of the type, 
with specific binding flags passed in to include only declared instance properties, both public and non-public.
It then returns the list of properties, which can be used to iterate through the properties of an object and retrieve their values.
It is worth noting that the commented code was supposed to get the properties of the base type as well but it is commented out which means that it is not currently used, 
so this method will only retrieve the properties of the current type passed to it and not the base type.

## Method  SerializeDistalDendrite from 631 to 643

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
This is a **helper** method called by the main Serialize method to serialize a DistalDendrite object. 
It takes in the same parameters as the main Serialize method (the object to be serialized, a name for the object, 
and a StreamWriter object to write the serialized data to).
It starts by creating a new list of members to ignore during serialization, which includes the "ParentCell" property of the DistalDendrite object. 
Then it calls the SerializeObject method, passing in the object, the name, the StreamWriter, and the ignore list.
It then gets the ParentCell property of the DistalDendrite object and checks if it has been serialized before by checking if it exists in isCellsSerialized list, 
if not it adds it to the list and call the Serialize method to serialize the ParentCell object, passing in the ParentCell object, the name "ParentCell", the StreamWriter, 
and the ignore list as parameters.
This method is specifically for the DistalDendrite class and it is used to serialize the parent cell object and make sure it is not serialized multiple times.

## Method  SerializeHtmConfig from 645 to 653

 private static void SerializeHtmConfig(object obj, string name, StreamWriter sw)
        {
            var excludeEntries = new List<string> { nameof(HtmConfig.Random) };
            SerializeObject(obj, name, sw, excludeEntries);

            var htmConfig = obj as HtmConfig;
            Serialize(htmConfig.RandomGenSeed, nameof(HtmConfig.Random), sw);

        }
This is a **helper** method called by the main Serialize method to serialize a HtmConfig object. 
It takes in the same parameters as the main Serialize method (the object to be serialized, a name for the object, 
and a StreamWriter object to write the serialized data to).
It starts by creating a new list of members to ignore during serialization, which includes the "Random" property of the HtmConfig object. 
Then it calls the SerializeObject method, passing in the object, the name, the StreamWriter, and the ignore list.
It then gets the HtmConfig object and calls the Serialize method to serialize the "Random" property, passing in the RandomGenSeed, 
the name "Random" and the StreamWriter as parameters.
This method is specifically for the HtmConfig class and it is used to exclude the "Random" property from the serialization while still serializing the RandomGenSeed property.

## Method  SerializeHomeostaticPlasticityController from 655 to 670

 private static void SerializeHomeostaticPlasticityController(object obj, string name, StreamWriter sw)
        {
            var excludeEntries = new List<string> { "m_OnStabilityStatusChanged" };
            SerializeObject(obj, name, sw, excludeEntries);
        }
        #endregion
        #region Deserialization

        //public static T Deserialize<T>(StreamReader sr, string propName = null)
        //{
        //    var type = typeof(T);

        //    T obj = ReadContent<T>(sr, propName);

        //    return obj;
        //}
This is a helper method called by the main Serialize method to serialize a HomeostaticPlasticityController object. 
It takes in the same parameters as the main Serialize method (the object to be serialized, a name for the object, 
and a StreamWriter object to write the serialized data to).
It starts by creating a new list of members to ignore during serialization, 
which includes the "m_OnStabilityStatusChanged" field of the HomeostaticPlasticityController object. 
Then it calls the SerializeObject method, passing in the object, the name, the StreamWriter, and the ignore list.
The method then ends with the region Deserialization, which contains the definition of a Deserialize method but is commented out, 
this method is not currently in use. It's purpose would be to deserialize the object by reading the contents of the StreamReader and returning the deserialized object of the type T.

## Method  T Deserialize from 672 to 720