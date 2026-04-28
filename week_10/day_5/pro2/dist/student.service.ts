"use strict";
// student.service.ts
Object.defineProperty(exports, "__esModule", { value: true });
exports.getGrade = getGrade;
exports.getTopper = getTopper;
const constants_1 = require("./constants");
function getGrade(marks) {
    if (marks >= 90)
        return "A";
    if (marks >= 75)
        return "B";
    if (marks >= 60)
        return "C";
    if (marks >= constants_1.PASS_MARKS)
        return "D";
    return "F";
}
function getTopper(students) {
    if (students.length === 0) {
        throw new Error("No students provided.");
    }
    return students.reduce((top, s) => (s.marks > top.marks ? s : top));
}
