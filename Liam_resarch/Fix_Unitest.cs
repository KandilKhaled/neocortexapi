// attempt to validate the unitest HTMSerializationTests
// 1rst failure SerializeArrayDouble in HTMSerializationTests
// i replaced with a new code beneath in HtmSerializer line 1452 the former one 

public double[] ReadArrayDouble(string reader)
{
    if (string.IsNullOrWhiteSpace(reader))
        return null;

    string[] str = reader.Split(ElementsDelimiter);
    double[] vs = new double[str.Length];
    for (int i = 0; i < str.Length; i++)
    {
        if (!double.TryParse(str[i].Trim(), out vs[i]))
            throw new FormatException("Unable to parse " + str[i].Trim() + " as a double");  // the error is here idk the origin see the photo for more info
    }
    return vs;
}
// the former code was 
public Double[] ReadArrayDouble(string reader)
{
    string value = reader.Trim();
    if (value == LineDelimiter)
		return null;
    else
        {
		string[] str = reader.Split(ElementsDelimiter);
		Double[] vs = new double[str.Length - 1];
		for (int i = 0; i < str.Length - 1; i++)
		{

			vs[i] = Convert.ToDouble(str[i].Trim()); // the error is also here 

		}
		return vs;
	}

}