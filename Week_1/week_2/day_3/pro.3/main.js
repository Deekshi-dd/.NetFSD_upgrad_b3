let toggleBtn = document.getElementById('toggle-btn');

toggleBtn.addEventListener('click', () => {
  if (document.body.classList.contains('dark-mode')) {
    document.body.classList.remove('dark-mode');
    toggleBtn.textContent = "Dark";
    localStorage.setItem('theme', 'light');
  } else {
    document.body.classList.add('dark-mode');
    toggleBtn.textContent = "Light";
    localStorage.setItem('theme', 'dark');
  }
});

let loadFunction = () => {

  let theme = localStorage.getItem("theme");

  if (theme === 'dark') {
    document.body.classList.add('dark-mode');
    toggleBtn.textContent = "Light";
  } else {
    document.body.classList.remove('dark-mode');
    toggleBtn.textContent = "Dark";
  }

  fetch("https://jsonplaceholder.typicode.com/posts")
    .then(data => data.json())
    .then(res => {
      let finalList = res.slice(0, 9);
      finalList.forEach(blog => {
        let div = document.createElement("div");
        div.classList.add("blog");
        div.innerHTML = `
          <h3>${blog.title}</h3>
          <p>${blog.body}</p>
        `;
        document.getElementById('container').appendChild(div);
      });
    });
}

document.addEventListener("DOMContentLoaded", loadFunction);
