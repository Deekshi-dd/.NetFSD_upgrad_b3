export const calculateTotal = (cartItems) =>
    cartItems.reduce(
        (total, item) => total + item.price * item.quantity,
        0
    );


export const generateInvoice = (cartItems) => {

    const itemLines = cartItems
        .map(
            (item, index) =>
                `${index + 1}. ${item.name}
   Price: ₹${item.price}
   Quantity: ${item.quantity}
   Subtotal: ₹${item.price * item.quantity}
`
        )
        .join("\n");

    const total = calculateTotal(cartItems);

    return `
========= SHOPPING CART INVOICE =========

${itemLines}
-----------------------------------------
Total Cart Value: ₹${total.toFixed(2)}
=========================================
`};
