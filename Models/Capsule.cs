using System;
using System.Collections.Generic;

namespace app.Models {
    public class Capsule {
        public Capsule(string[] initialData) {
            foreach (string initialItem in initialData)
            {
                _data.Add(initialItem);
            }
        }

        private List<string> _data = new List<string>();
    }
}