class DataManager {
    constructor() {
        this.items = [];
    }
    add(item) {
        this.items.push(item);
        console.log("Item added:", item);
    }
    getAll() {
        return this.items;
    }
}
// Generic Function ─────────────────────────────────────────────────────
function getFirstElement(items) {
    if (items.length === 0) {
        throw new Error("Array is empty - cannot get first element.");
    }
    return items[0];
}
//Use Case: Users ──────────────────────────────────────────────────────
const userManager = new DataManager();
userManager.add({ id: 1, name: "Alice Johnson" });
userManager.add({ id: 2, name: "Bob Smith" });
userManager.add({ id: 3, name: "Charlie Brown" });
console.log("\nAll Users:");
console.log(userManager.getAll());
const firstUser = getFirstElement(userManager.getAll());
console.log("\nFirst User:", firstUser);
// Use Case: Products ────────────────────────────────────────────────────
const productManager = new DataManager();
productManager.add({ id: 101, title: "Laptop Pro" });
productManager.add({ id: 102, title: "Wireless Mouse" });
productManager.add({ id: 103, title: "Mechanical Keyboard" });
console.log("\nAll Products:");
console.log(productManager.getAll());
const firstProduct = getFirstElement(productManager.getAll());
console.log("\nFirst Product:", firstProduct);
