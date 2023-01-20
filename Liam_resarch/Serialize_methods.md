#2 Methods

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