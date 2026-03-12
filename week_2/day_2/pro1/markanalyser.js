const studentMarks = [75, 82, 48, 90, 67];

const calculateTotal = (marksArray) =>
    marksArray.reduce((total, mark) => total + mark, 0);

const calculateAverage = (marksArray) => {
    const total = calculateTotal(marksArray);
    return total / marksArray.length;
};

const getResult = (average) =>
    average >= 50 ? "Pass ✅" : "Fail ❌";

const displayMarks = (marksArray) =>
    marksArray
        .map((mark, index) => `Student ${index + 1}: ${mark} marks`)
        .join("\n");

const analyzeMarks = () => {
    const total = calculateTotal(studentMarks);
    const average = calculateAverage(studentMarks);
    const result = getResult(average);

    return `
===== Student Marks Report =====
${displayMarks(studentMarks)}

Total Marks   : ${total}
Average Marks : ${average.toFixed(2)}
Result        : ${result}
=================================
`;
};


const button = document.getElementById("analyzeBtn");
const output = document.getElementById("output");

button.addEventListener("click", () => {
    output.textContent = analyzeMarks();
});
