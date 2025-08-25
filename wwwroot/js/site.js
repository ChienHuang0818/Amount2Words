// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// toggle theme
function toggleTheme() {
  const body = document.body;
  const isDark = body.classList.contains("dark-mode");
  body.classList.remove(isDark ? "dark-mode" : "light-mode");
  body.classList.add(isDark ? "light-mode" : "dark-mode");
  localStorage.setItem("theme", isDark ? "light" : "dark");
}

// copy result
function copyResult() {
  const text = document.querySelector(".result-text")?.innerText;
  if (text) {
    navigator.clipboard.writeText(text);
    alert("Copied!");
  }
}

// init theme
(function () {
  const savedTheme = localStorage.getItem("theme") || "light";
  document.body.classList.add(savedTheme + "-mode");
})();
