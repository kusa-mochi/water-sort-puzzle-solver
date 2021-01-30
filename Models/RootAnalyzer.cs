using System;
using System.Collections.Generic;
using app.Models;

namespace app
{
    public class RootAnalyzer
    {
        public RootAnalyzer()
        {

        }

        public List<RootSelection> Analyze(string[,] data)
        {
            DataConverter converter = new DataConverter();
            List<BottleStack> startData = converter.ArraysToStacks(data);
            this.AnalyzeRecursively(startData);

            return _resultRoot;
        }

        private void AnalyzeRecursively(List<BottleStack> currentData)
        {
            _analyzingCount++;
            if(_analyzingCount > MAX_ANALYZING_COUNT) return;

            // if fin
            if(_resultRoot.Count > 0) return;

            // i th bottle to j th bottle
            for(int iBottle = 0; iBottle < currentData.Count; iBottle++)
            {
                for(int jBottle = 0; jBottle < currentData.Count; jBottle++)
                {
                    // if it is unavailable to pour i to j
                    if(
                        iBottle == jBottle ||
                        !currentData[iBottle].CanPushTo(currentData[jBottle])
                        ) continue;

                    // copy bottles data as next generation
                    List<BottleStack> nextData = this.CopyData(currentData);

                    // pour i to j in next generation data
                    nextData[iBottle].PushTo(nextData[jBottle]);

                    // record current root
                    _currentRoot.Push(
                        new RootSelection
                        { 
                            From = iBottle, 
                            To = jBottle 
                        }
                    );

                    // check if the analyzing is fin
                    if(this.CheckIfFin(nextData))
                    {
                        // this.PrintCurrentBottles(nextData);
                        // this.PrintCurrentRoot();
                        while(_currentRoot.Count > 0)
                        {
                            _resultRoot.Insert(0, _currentRoot.Pop());
                        }

                        return;
                    }

                    // analyze next generation data
                    this.AnalyzeRecursively(nextData);

                    if(_currentRoot.Count > 0) _currentRoot.Pop();
                }
            }
        }

        private List<BottleStack> CopyData(List<BottleStack> data)
        {
            List<BottleStack> output = new List<BottleStack>();
            foreach (BottleStack bottle in data)
            {
                output.Add(bottle.Copy);
            }
            return output;
        }

        private bool CheckIfFin(List<BottleStack> data)
        {
            foreach (BottleStack bottle in data)
            {
                if(!bottle.IsCompleted && bottle.Count != 0) return false;
            }

            return true;
        }

        private void PrintCurrentRoot()
        {
            string buf = "";
            foreach (RootSelection selection in _currentRoot)
            {
                buf = $"{selection.From} to {selection.To} | {buf}";
            }
            Console.WriteLine(buf);
        }

        private void PrintCurrentBottles(List<BottleStack> bottles)
        {
            foreach (BottleStack bottle in bottles)
            {
                foreach (string item in bottle.List)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-----");
        }

        private Stack<RootSelection> _currentRoot = new Stack<RootSelection>();
        private List<RootSelection> _resultRoot = new List<RootSelection>();
        private long _analyzingCount = 0L;
        private const long MAX_ANALYZING_COUNT = 100000L;
    }
}