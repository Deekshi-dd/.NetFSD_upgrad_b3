interface User {
  id: number;
  name: string;
}

interface Product {
  id: number;
  title: string;
}

// ── 2. Generic Interface ────────────────────────────────────────────────────

interface Repository<T> {
  add(item: T): void;
  getAll(): T[];
}

// ── 3. Generic Class ────────────────────────────────────────────────────────

class DataManager<T> implements Repository<T> {
  private items: T[] = [];

  add(item: T): void {
    this.items.push(item);
    console.log("Item added:", item);
  }

  getAll(): T[] {
    return this.items;
  }
}

// ── 4. Generic Function ─────────────────────────────────────────────────────

function getFirstElement<T>(items: T[]): T {
  if (items.length === 0) {
    throw new Error("Array is empty - cannot get first element.");
  }
  return items[0];
}

// ── 5. Use Case: Users ──────────────────────────────────────────────────────

const userManager = new DataManager<User>();

userManager.add({ id: 1, name: "Alice Johnson" });
userManager.add({ id: 2, name: "Bob Smith" });
userManager.add({ id: 3, name: "Charlie Brown" });

console.log("\nAll Users:");
console.log(userManager.getAll());

const firstUser = getFirstElement<User>(userManager.getAll());
console.log("\nFirst User:", firstUser);

// ── 6. Use Case: Products ────────────────────────────────────────────────────

const productManager = new DataManager<Product>();

productManager.add({ id: 101, title: "Laptop Pro" });
productManager.add({ id: 102, title: "Wireless Mouse" });
productManager.add({ id: 103, title: "Mechanical Keyboard" });

console.log("\nAll Products:");
console.log(productManager.getAll());

const firstProduct = getFirstElement<Product>(productManager.getAll());
console.log("\nFirst Product:", firstProduct);
