using System.Collections.Generic;
using app.Models;

namespace app
{
    public class DataConverter
    {
        public DataConverter()
        {

        }

        public List<BottleStack> ArraysToStacks(string[,] rawData)
        {
            if(rawData == null) return null;

            List<BottleStack> output = new List<BottleStack>();
            for(int i = 0; i < rawData.GetLength(0); i++)
            {
                List<string> tmpData = new List<string>();
                for(int j = 0; j < rawData.GetLength(1); j++)
                {
                    if(rawData[i, j] == "") continue;
                    tmpData.Add(rawData[i, j]);
                }
                output.Add(new BottleStack(tmpData, rawData.GetLength(1)));
            }
            
            return output;
        }
    }
}
