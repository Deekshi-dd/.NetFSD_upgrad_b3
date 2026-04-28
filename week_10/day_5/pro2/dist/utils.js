"use strict";
// utils.ts
Object.defineProperty(exports, "__esModule", { value: true });
exports.formatName = formatName;
exports.calculateAverage = calculateAverage;
function formatName(name) {
    return name
        .trim()
        .split(" ")
        .map((word) => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
        .join(" ");
}
function calculateAverage(students) {
    if (students.length === 0)
        return 0;
    const total = students.reduce((sum, s) => sum + s.marks, 0);
    return parseFloat((total / students.length).toFixed(2));
}
