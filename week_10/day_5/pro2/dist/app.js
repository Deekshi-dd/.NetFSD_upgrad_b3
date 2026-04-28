"use strict";
// app.ts
Object.defineProperty(exports, "__esModule", { value: true });
const student_service_1 = require("./student.service");
const utils_1 = require("./utils");
const constants_1 = require("./constants");
const students = [
    { id: 1, name: "alice johnson", marks: 92 },
    { id: 2, name: "bob smith", marks: 38 },
    { id: 3, name: "charlie brown", marks: 75 },
    { id: 4, name: "diana prince", marks: 61 },
    { id: 5, name: "edward norton", marks: 85 },
];
console.log("==================================================");
console.log("       STUDENT MANAGEMENT UTILITY");
console.log("==================================================");
console.log("\nPass Marks Threshold:", constants_1.PASS_MARKS);
console.log("\nStudent Report:");
console.log("--------------------------------------------------");
students.forEach((student) => {
    const formattedName = (0, utils_1.formatName)(student.name);
    const grade = (0, student_service_1.getGrade)(student.marks);
    const status = student.marks >= constants_1.PASS_MARKS ? "Pass" : "Fail";
    console.log(`ID: ${student.id} | Name: ${formattedName.padEnd(18)} | Marks: ${String(student.marks).padStart(3)} | Grade: ${grade} | ${status}`);
});
console.log("\nClass Average Marks:", (0, utils_1.calculateAverage)(students));
const topper = (0, student_service_1.getTopper)(students);
console.log("Class Topper       :", (0, utils_1.formatName)(topper.name), `(Marks: ${topper.marks})`);
console.log("==================================================");
