using System;
using System.Collections.Generic;

namespace app.Models {
    public class BottleStack {

        public BottleStack(int maxCount)
        {
            if(maxCount < 1) throw new ArgumentOutOfRangeException("maxCount");
            MaxCount = maxCount;
        }

        public BottleStack(List<string> data, int maxCount)
        {
            if(data == null) throw new ArgumentNullException("data");
            foreach (string item in data)
            {
                _data.Add(item);
            }
            MaxCount = maxCount;
        }

        public BottleStack Copy
        {
            get
            {
                BottleStack output = new BottleStack(_data, MaxCount);
                return output;
            }
        }

        public List<string> List
        {
            get
            {
                List<string> output = new List<string>();
                foreach (string item in _data)
                {
                    output.Add(item);
                }
                return output;
            }
        }

        public int Count
        {
            get
            {
                int output = 0;
                for(int i = 0; i < MaxCount && i < _data.Count; i++)
                {
                    if(_data[i] == "") break;
                    output++;
                }
                return output;
            }
        }

        public int TypeCount
        {
            get
            {
                if(Count == 0) return 0;
                int output = 0;
                string currentItemValue = "";
                for(int i = 0; i < Count; i++)
                {
                    if(_data[i] != "" && _data[i] != currentItemValue)
                    {
                        currentItemValue = _data[i];
                        output++;
                    }
                }

                return output;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return (
                    this.Count == this.MaxCount &&
                    this.TypeCount == 1
                );
            }
        }

        public int MaxCount { get; set; } = 0;

        public PopState NextPopState
        {
            get
            {
                if(_data.Count == 0) return new PopState{ Item = "", Count = 0 };

                string topItem = _data[_data.Count - 1];
                int outputCount = 1;
                for(int i = _data.Count - 2; i >= 0; i--)
                {
                    if(_data[i] == topItem) outputCount++;
                    else break;
                }

                return new PopState{ Item = topItem, Count = outputCount };
            }
        }

        public bool CanPush(PopState s)
        {
            return (
                (this.Count == 0 && s.Count != this.MaxCount) ||
                (
                    s.Count > 0 &&
                    s.Item == this.NextPopState.Item &&
                    s.Count <= this.MaxCount - this.Count
                )
            );
        }

        public bool CanPushTo(BottleStack bottle)
        {
            return (
                (
                    bottle.Count == 0 && 
                    0 < this.Count &&
                    1 < this.TypeCount &&
                    this.Count <= this.MaxCount &&
                    0 < this.NextPopState.Count &&
                    this.NextPopState.Count <= bottle.MaxCount
                ) ||
                (
                    0 < bottle.Count &&
                    0 < this.Count &&
                    this.NextPopState.Item == bottle.NextPopState.Item &&
                    this.NextPopState.Count <= bottle.MaxCount - bottle.Count
                )
            );
        }

        public PopState Pop()
        {
            PopState output = this.NextPopState;
            if(output.Count > 0)
            {
                _data.RemoveRange(_data.Count - output.Count, output.Count);
            }
            return output;
        }

        public void Push(PopState pushItem)
        {
            if(!this.CanPush(pushItem)) return;

            for(int i = 0; i < pushItem.Count; i++)
            {
                _data.Insert(_data.Count >= 0 ? _data.Count : 0, pushItem.Item);
            }
        }

        public void PushTo(BottleStack bottle)
        {
            if(!this.CanPushTo(bottle)) return;

            PopState popData = this.Pop();
            bottle.Push(popData);
        }

        public void Print()
        {
            if(_data.Count == 0)
            {
                Console.WriteLine("NO DATA");
                return;
            }
            foreach (string item in _data)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine("");
        }

        private List<string> _data = new List<string>();
    }
}