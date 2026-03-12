let tasks =[];

let addTask = (task) =>{
  return new Promise((resolve,reject)=>{
    setTimeout(()=>{
      if(!task){
        reject("task must be entered")
      }else{
        tasks.push(task)
        resolve(`Task ${task} added successfully`)
      }
     
    },1000)
  })
}

let deleteTask = (task) =>{
  return new Promise((resolve,reject)=>{
    setTimeout(()=>{
      let index=tasks.indexOf(task);

      if (index === -1){
        reject("There is No task")
      }
      else{
        tasks.splice(index,1)
        resolve(`${task} deleted successfully`)
      }

    
 
    },1000)
  })
}

let listTasks = ()=>{
  return new Promise((resolve,reject)=>{
    setTimeout(()=>{
      resolve(tasks)

    },1000)
  })
}

let handleAddTask = async()=>{
  try{
    let message = await addTask(document.getElementById("task").value);
    alert(message);
    document.getElementById("task").value=""
   
  }catch(error){
    alert(error)
  }
  
}

let handleDeleteTask = async (task)=>{
  try{
    let message = await deleteTask(task);
    alert(message)
    handleListTasks()
  }
  catch(error){
    alert(error)
  }

}

let handleListTasks = async ()=>{
  let tasks = await listTasks();
  let res =document.getElementById("res");

  res.innerHTML = "";

  tasks.forEach(task =>{
    let li= document.createElement('li');
   li.innerHTML=`${task} <button class="delete-btn" onclick="handleDeleteTask('${task}')">Delete</button>`;
    res.appendChild(li);
  })

}
