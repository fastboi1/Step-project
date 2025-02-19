using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.models
{
    internal class Author
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Author(string name, string lastname, int age)
        {
            Name = name;
            LastName = lastname;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Name} {LastName}, Age: {Age}";


        }
    }
}
