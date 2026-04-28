// 1. Base Class: Employee
class Employee {
  public id: number;
  protected name: string;
  private salary: number;

  constructor(id: number, name: string, salary: number) {
    this.id = id;
    this.name = name;
    this.salary = salary;
  }

  // 2. Getters and Setters
  public getSalary(): number {
    return this.salary;
  }

  public setSalary(value: number): void {
    if (value > 0) {
      this.salary = value;
    } else {
      console.log("Salary must be greater than 0");
    }
  }

  // 3. Method
  public displayDetails(): void {
    console.log(`ID: ${this.id}`);
    console.log(`Name: ${this.name}`);
    console.log(`Salary: ${this.salary}`);
  }
}

// 4. Derived Class: Manager
class Manager extends Employee {
  public teamSize: number;

  constructor(id: number, name: string, salary: number, teamSize: number) {
    super(id, name, salary); // call base constructor
    this.teamSize = teamSize;
  }

  // 5. Method Overriding
  public displayDetails(): void {
    super.displayDetails(); // reuse parent logic
    console.log(`Team Size: ${this.teamSize}`);
  }
}

// 6. Object Creation

// Employee object
const emp = new Employee(1, "arjun", 30000);
emp.displayDetails();

emp.setSalary(35000);
console.log("Updated Salary:", emp.getSalary());

console.log("--------------");

// Manager object
const mgr = new Manager(2, "ayaan", 50000, 5);
mgr.displayDetails();
