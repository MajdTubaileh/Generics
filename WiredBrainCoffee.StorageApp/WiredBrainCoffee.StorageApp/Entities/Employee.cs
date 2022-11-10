
using System.Reflection.Metadata;

namespace WiredBrainCoffee.StorageApp.Entities
{
    public class Employee : EntityBase
    {
       
        public string? FirstName { get; set; }

        public override string ToString() => $"Id ={Id}, FirstName = {FirstName}";

    }
    public class Manager : Employee
    {
        public override string ToString() => base.ToString() + " (Manager) "; 
        
         
        
    }



}
