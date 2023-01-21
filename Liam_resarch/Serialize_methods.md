#Methods explain:

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

##Method SerializeBegin(String typeName, StreamWriter sw) line 50
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

##Method Reset() line 66

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