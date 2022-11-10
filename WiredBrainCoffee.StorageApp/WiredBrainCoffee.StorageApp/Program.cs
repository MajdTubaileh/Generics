using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;


namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main()
        {
            
            ItemAdded<Employee> itemAdded = new ItemAdded<Employee>/*i can remove the before till assign because it's already casted*/(EmployeeAdded);
            var sqlrep = new StorageAppDbContext();
            var employeeRepository = new SqlRepository<Employee>(sqlrep,itemAdded);
           
            
            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            GetEmployeeById(employeeRepository);
            WriteAllToConsole(employeeRepository);


            var organizationRepository = new ListRepository<Organization>();
            AddOrganizations(organizationRepository);
            WriteAllToConsole(organizationRepository);



            //organizationRepository.Save();
        }

        private static void EmployeeAdded(Employee item)
        {
          
            Console.WriteLine($"Employee Added => {item.FirstName}");
        }

        private static void AddManagers(IWriteRepository<Manager> ManagerRepository)
        {
            var ManJ = new Manager { FirstName = "Jihad" };
           var  ManJC=ManJ.Copy();
            ManagerRepository.Add(ManJ);
            if(ManJC is not null)
            {
                ManJC.FirstName += "_Copy";
                ManagerRepository.Add(ManJC);
            }
            
            ManagerRepository.Add(new Manager { FirstName = "Sameh" });
            ManagerRepository.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> Repository)
        {

            Console.WriteLine("------------");

            /*
            var employee = employeeRepository.GetById(1);
            var employee1 = employeeRepository.GetById(2);
            var employee2 = employeeRepository.GetById(3);
            Console.WriteLine(employee.ToString());
            Console.WriteLine(employee1.ToString());
            Console.WriteLine(employee2.ToString());
            */
            var items = Repository.GetAll();

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------");
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with ID : 2 {employee.FirstName}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[] {
                new Employee { FirstName = "Majd" },
                new Employee { FirstName = "AbuSalah" },
                new Employee { FirstName = "Murad" }
            };
            employeeRepository.AddBatch(employees);

        }

        private static void AddOrganizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {

            new Organization { Name = "Randa" },
           new Organization { Name = "Karam" },
            new Organization { Name = "Sabaa'na" }
        };
            organizationRepository.AddBatch(organizations);


        }


    }
}
