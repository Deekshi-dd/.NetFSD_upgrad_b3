
class Vehicle {
    constructor(brand, speed) {
        this.brand = brand;
        this.speed = speed;
    }

    displayInfo() {
        return `Brand: ${this.brand}, Speed: ${this.speed} km/h`;
    }
}


class Car extends Vehicle {
    constructor(brand, speed, fuelType) {
        super(brand, speed); 
        this.fuelType = fuelType;
    }

    showCarDetails() {
        return `${this.displayInfo()}, Fuel Type: ${this.fuelType}`;
    }
}

const car1 = new Car("Toyota", 180, "Petrol");

console.log(car1.displayInfo());
console.log(car1.showCarDetails());
