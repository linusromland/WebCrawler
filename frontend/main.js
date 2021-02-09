
let out = document.getElementById("out");

let input = document.getElementById("search");

input.oninput = function () {
  if (input.value) {
    fetch("/search/?search=" + input.value)
      .then((response) => response.json())
      .then((value) => {
        //out.innerText = JSON.stringify(value);
        // expected output: "Success!"

        if (!(value == "[]")) {
          generateList(value);
        } else {
          out.innerHTML = "";
        }
      });
  } else {
    out.innerHTML = "";
  }
};

function generateList(json) {
  out.innerHTML = "";
  json.forEach((element) => {
    var div = document.createElement("DIV");

    var result = document.createElement("A");
    if (element.url.length >= 50) {
      result.innerText = element.url.substring(0, 50) + "...";
    } else {
      result.innerText = element.url;
    }

    result.href = element.url;
    div.appendChild(result);
    div.appendChild(document.createElement("BR"));

    var result = document.createElement("A");
    if (element.origin.length >= 50) {
      result.innerText = element.origin.substring(0, 50) + "...";
    } else {
      result.innerText = element.origin;
    }

    result.href = element.origin;
    div.appendChild(result);
    div.classList.add("block");

    out.appendChild(div);
    out.appendChild(document.createElement("BR"));
    out.appendChild(document.createElement("BR"));
  });
}
