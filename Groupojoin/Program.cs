using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Groupojoin
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("_____ Join Ops _____");


            var resultGroupBy = Employee2.GetAllEmployees().GroupBy(
                x => (x.DepartmentId/10)%2, // Key
                y => y);                    // Element

            foreach (var item in resultGroupBy)
            {
                Console.WriteLine(item.Key);
                foreach (var itemInner in item)
                {
                    Console.WriteLine("\tWorker: " + itemInner.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            #region InnerJoin
            Console.WriteLine();
            Console.WriteLine("*********  InnerJoin *********");
            var department = new Department();
            var employee2 = new Employee2();

            var result2MS = Department.GetAllDepartments().Join(
                Employee2.GetAllEmployees(),
                d => d.ID,
                e => e.DepartmentId,
                (d, e) => new { d.ID, d.Name, e.DepartmentId, EName = e.Name}
                );

            foreach (var item in result2MS)
            {
                Console.WriteLine($"MS | InnerJoin: {item.Name},    EDI | DID: {item.DepartmentId}|{item.ID},  EName: {item.EName}");
            }

            #endregion


            #region GroupJoin
            Console.WriteLine();
            Console.WriteLine("******* GroupJoin *******");

            var result = Department.GetAllDepartments()
                            .GroupJoin(
                                Employee2.GetAllEmployees(),           // Innere Sammlung
                                dept => dept.ID,                      // Äußerer Schlüssel
                                emp => emp.DepartmentId,              // Innerer Schlüssel
                                (dept, empGroup) => new               // Ergebnisprojektion
                                {
                                    DepartmentName = dept.Name,
                                    Employees = empGroup
                                });

            // Ausgabe
            foreach (var group in result)
            {
                Console.WriteLine($"Abteilung: {group.DepartmentName}");
                foreach (var emp in group.Employees)
                {
                    Console.WriteLine($"  - {emp.Name} (ID: {emp.ID})");
                }
            }

            #endregion

            #region GroupJoin2 mit Fehler
            Console.WriteLine();
            Console.WriteLine("### GroupJoin mit Select");
            var test = Department.GetAllDepartments().GroupJoin(
                Employee2.GetAllEmployees(),
                depart => depart.ID,
                empl => empl.DepartmentId,
                (gruppe, gruppierteListe) => new
                {
                    Gruppe = gruppe.Name,
                    GruppierteListe = gruppierteListe.Select(x => new Employee2()
                    {
                        DepartmentId = x.DepartmentId,
                        Name = x.Name,
                        ID = x.ID
                    })
                });

            foreach (var item in test)
            {
                Console.WriteLine("Abt.: " + item.Gruppe);
                foreach (var itemInner in item.GruppierteListe)
                {
                    Console.Write("\t"+ itemInner.Name + "|" + itemInner.ID + ", ");
                }
                Console.WriteLine();
            }

            #endregion


            #region LeftJoin
            Console.WriteLine();
            Console.WriteLine("**********  LeftJoin  ********");
            var leftJoin = Employee2.GetAllEmployees().GroupJoin(
                    Department.GetAllDepartments(),
                    emp => emp.DepartmentId,
                    dept => dept.ID,
                    (emp, deptGroup) => new { emp, deptGroup }
                    )
                    .SelectMany(
                        x => x.deptGroup.DefaultIfEmpty(),
                        (empl, dept) => new {
                            EmployeeName = empl.emp.Name,
                            DepartmentName = dept?.Name ?? "No Department"
                        }
                    ).GroupBy(e => e.DepartmentName);

            var leftJoin2 = Employee2.GetAllEmployees().GroupJoin(
                    Department.GetAllDepartments(),
                    emp => emp.DepartmentId,
                    dept => dept.ID,
                    (emp, deptGroup) => new { emp, deptGroup }
                    )
                    .SelectMany(
                        x => x.deptGroup.DefaultIfEmpty(),
                        (emp, dept) => new {
                            EmployeeName = emp.emp.Name,
                            DepartmentName = dept?.Name ?? "No Department"
                        }
                    );

            var leftJoin3 = Employee2.GetAllEmployees().GroupJoin(
                    Department.GetAllDepartments(),
                    emp => emp.DepartmentId,
                    dept => dept.ID,
                    (emp, deptGroup) => new { emp, deptGroup }
                    );
            //.SelectMany(
            //    x => x.deptGroup.DefaultIfEmpty(),
            //    (emp, dept) => new {
            //        EmployeeName = emp.emp.Name,
            //        DepartmentName = dept?.Name ?? "No Department"
            //    }
            //);

            var leftJoin4 = Employee2.GetAllEmployees().GroupJoin(
                    Department.GetAllDepartments(),
                    emp => emp.DepartmentId,
                    dept => dept.ID,
                    (emp, deptGroup) => new { emp, deptGroup }
                    )
                    .SelectMany(x => x.deptGroup.DefaultIfEmpty());

            var leftJoin5 = Department.GetAllDepartments()
                    .GroupJoin(
                        Employee2.GetAllEmployees(),
                        d => d.ID,
                        e => e.DepartmentId,
                        (dept, empGroup) => new { dept, empGroup }
                        )
                        .SelectMany(
                                x => x.empGroup.DefaultIfEmpty(),
                                (dept, emp) => new {
                                    DepartmentName = dept.dept?.Name ?? "No Department",
                                    EmployeeName = emp?.Name
                                }
                            )
                            .GroupBy(x => x.DepartmentName).Select(e => new { Key = e.Key, Value = e });

            //var grouped = Employee2.GetAllEmployees().GroupBy(
            //                    e => e.Department?.Name ?? "No Department"
            //                );

            //foreach (var item in leftJoin)
            //{
            //    Console.WriteLine(item.DepartmentName + " --- " + item.EmployeeName);
            //}

            foreach (var item in leftJoin)
            {
                Console.WriteLine(item.Key);
                foreach (var wert in item)
                {
                    Console.WriteLine("\t" + wert.EmployeeName);
                }
            }

            Console.WriteLine();

            foreach (var item in leftJoin2)
            {
                Console.WriteLine(item.DepartmentName + " <--> " + item.EmployeeName);
            }

            Console.WriteLine();

            foreach (var item in leftJoin3)
            {
                
                Console.WriteLine(item.emp.Name + " <--> " + item.deptGroup);
            }

            Console.WriteLine();
            Console.WriteLine("############################");
            foreach (var item in leftJoin4)
            {
                //Console.WriteLine(item.ID + " <--> " + item.Name);
            }

            Console.WriteLine("#############################");

            foreach (var item in leftJoin5)
            {
                Console.WriteLine(item.Key);
                foreach (var itemInner in item.Value)
                {
                    Console.WriteLine("\t"+ itemInner);
                }
            }

            #endregion
        }
    }

    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static List<Department> GetAllDepartments()
        {
            return new List<Department>
        {
            new Department { ID = 10, Name = "IT" },
            new Department { ID = 20, Name = "HR" },
            new Department { ID = 30, Name = "Sales" },
            new Department { ID = 40, Name = "Marketing" },
            // new Department {ID=0,Name="Keine Abteilung"}
        };
        }
    }

    public class Employee2
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }

        public static List<Employee2> GetAllEmployees()
        {
            return new List<Employee2>
        {
            new Employee2 { ID = 1, Name = "Preety", DepartmentId = 10 },
            new Employee2 { ID = 2, Name = "Priyanka", DepartmentId = 20 },
            new Employee2 { ID = 3, Name = "Anurag", DepartmentId = 30 },
            new Employee2 { ID = 4, Name = "Pranaya", DepartmentId = 30 },
            new Employee2 { ID = 5, Name = "Hina", DepartmentId = 20 },
            new Employee2 { ID = 5, Name = "Hina", DepartmentId = 0 },
            new Employee2 { ID = 8, Name = "Tarun", DepartmentId = 0 } // Keine Abteilung
        };
        }
    }
}
