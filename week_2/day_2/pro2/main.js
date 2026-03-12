import { generateInvoice } from './cartUtils.js';

const cartItems = [
    { name: "Laptop", price: 55000, quantity: 1 },
    { name: "Mouse", price: 800, quantity: 2 },
    { name: "Keyboard", price: 1500, quantity: 1 }
];


const invoice = generateInvoice(cartItems);

document.getElementById("output").textContent = invoice;
