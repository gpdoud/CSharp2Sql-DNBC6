using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp2SqlProjevct {

    public class Customer {

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
        public string Code { get; set; }

        public Customer(int id, string name, string city, string state,
                            bool active, string code) {
            this.Id = id;
            this.Name = name;
            this.City = city;
            this.State = state;
            this.Active = active;
            this.Code = code;
        }
    }
}
