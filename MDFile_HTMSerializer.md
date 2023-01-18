**How to use HTMSerializer**
 
 // https://github.com/ddobric/neocortexapi/blob/faf692e0a1f45a37619cb95dbb44cc77d4ca47e2/source/NeoCortexEntities/Entities/DistalDendrite.cs

  
https://github.com/ddobric/neocortexapi/blob/3ae893e8209a05018c1f97aa5a25dfc92e420749/source/NeoCortexEntities/HtmSerializer.cs

https://github.com/ddobric/neocortexapi/blob/3ae893e8209a05018c1f97aa5a25dfc92e420749/source/NeoCortexApi/HtmSerializer2.cs#L26


///https://github.com/ddobric/neocortexapi/blob/faf692e0a1f45a37619cb95dbb44cc77d4ca47e2/source/NeoCortexEntities/Entities/Cell.cs

   #region Serialization

        /// <summary>
        /// Serializes the cell to the stream.
        /// </summary>
        /// <param name="writer"></param>
        public void SerializeT(StreamWriter writer)
        {
            //Create a HtmSerializer object (ser)
            HtmSerializer ser = new HtmSerializer();

            //Serialization begins
            ser.SerializeBegin(nameof(Cell), writer);

            //SerializeValue mehtod serialize the data Index and ParentColumnIndez, properties of the class Cell
            ser.SerializeValue(this.Index, writer);
            //ser.SerializeValue(this.CellId, writer);
            ser.SerializeValue(this.ParentColumnIndex, writer);

            if (this.DistalDendrites != null && this.DistalDendrites.Count > 0)
                ser.SerializeValue(this.DistalDendrites, writer);

            if (this.ReceptorSynapses != null && this.ReceptorSynapses.Count > 0)
                ser.SerializeValue(this.ReceptorSynapses, writer);

            //Finish the serialization if neither of the two conditions are 1
            ser.SerializeEnd(nameof(Cell), writer);
        }

        public static Cell Deserialize(StreamReader sr)
        {
            Cell cell = new Cell();

            HtmSerializer ser = new HtmSerializer();

            while (sr.Peek() >= 0)
            {
                //reads a line of characters from the current stream and returns the data as a string.
                string data = sr.ReadLine();
                
                //the HtmSerializer object invoques de method ReadBegin
                if (data == String.Empty || data == ser.ReadBegin(nameof(Cell)) || data.ToCharArray()[0] == HtmSerializer.ElementsDelimiter || (data.ToCharArray()[0] == HtmSerializer.ElementsDelimiter && data.ToCharArray()[1] == HtmSerializer.ParameterDelimiter))
                {
                    continue;
                }
                else if (data == ser.ReadBegin(nameof(Segment)))
                {
                    //cell.DistalDendrites.Add(DistalDendrite.Deserialize(sr));
                }
                else if (data == ser.ReadBegin(nameof(Synapse)))
                {
                    return cell;
                }
                else if (data == ser.ReadEnd(nameof(Cell)))
                {
                    break;
                }
                else
                {
                    string[] str = data.Split(HtmSerializer.ParameterDelimiter);
                    for (int i = 0; i < str.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    cell.Index = ser.ReadIntValue(str[i]);
                                    break;
                                }
                            case 1:
                                {
                                    // cell.CellId = ser.ReadIntValue(str[i]);
                                    break;
                                }
                            case 2:
                                {
                                    cell.ParentColumnIndex = ser.ReadIntValue(str[i]);
                                    break;
                                }
                            default:
                                { break; }

                        }
                    }
                }
            }
            return cell;
        }

    