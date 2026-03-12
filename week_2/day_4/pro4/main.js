let tasks = [];

const btn = document.getElementById("add-task");
const res = document.getElementById("res");
const input = document.getElementById("task");

function showTasks() {
  res.innerHTML = "";

  tasks.forEach((task, index) => {
    const li = document.createElement("li");

    li.innerHTML = `
      <span class="${task.completed ? "completed" : ""}">
        ${task.text}
      </span>

      <div>



      <button class="complete-btn" data-index="${index}">
        ${task.completed ? "Undo" : "Complete"}
      </button>

      <button class="delete-btn" data-index="${index}">
        Delete
      </button>
      </div>
    `;

    res.appendChild(li);
  });
}
showTasks()

btn.addEventListener("click", () => {
  const taskText = input.value.trim();

  if (!taskText) {
    alert("Enter task");
    return;
  }

  tasks.push({
    text: taskText,
    completed: false,
  });

  input.value = "";
  showTasks();
});

res.addEventListener("click", (e) => {
  let index = e.target.getAttribute("data-index");

  if (e.target.classList.contains("complete-btn")) {
    tasks[index].completed = !tasks[index].completed;
    showTasks();
  }

  if (e.target.classList.contains("delete-btn")) {
    tasks.splice(index, 1);
    showTasks();
  }
});
