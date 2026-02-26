let getBtn = document.getElementById("btn");

getBtn.addEventListener('click', () => {

  let city = document.getElementById("city").value.trim();
  let resultBox = document.getElementById('res');

  if (!city) {
    resultBox.innerHTML = `<p class="error">Please enter a city name</p>`;
    return;
  }

  resultBox.innerHTML = "Loading...";

  fetch(`https://api.weatherapi.com/v1/current.json?key=7d31ccd07f60464caa693636262502&q=${city}`)
    .then(response => response.json())
    .then(data => {

      if (data.error) {
        resultBox.innerHTML = `<p class="error">${data.error.message}</p>`;
        return;
      }

      resultBox.innerHTML = `
        <p><strong>Location:</strong> ${data.location.name}, ${data.location.region}</p>
        <p><strong>Temperature:</strong> ${data.current.temp_c}°C</p>
        <p><strong>Humidity:</strong> ${data.current.humidity}%</p>
        <p><strong>Wind Speed:</strong> ${data.current.wind_kph} kph</p>
      `;
    })
    .catch(() => {
      resultBox.innerHTML = `<p class="error">Something went wrong</p>`;
    });
});
