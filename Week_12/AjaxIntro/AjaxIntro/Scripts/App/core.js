
// Works with the UseInfo action method and views
function addRegions(value) {

    // Get a reference to the DOM element
    var content = document.querySelector('#Regions');

    // Create an xhr object
    var xhr = new XMLHttpRequest();

    // Configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // Request-response cycle has completed, so continue

            if (xhr.status === 200) {
                // Request-response was successful, so continue

                // Update the user interface
                content.innerHTML = xhr.responseText;

                // Enable the button
                document.querySelector('#createButton').disabled = false;

            } else {
                // Request-response was not successful
                content.innerHTML = '<p class="col-md-offset-2">Error: ' + xhr.statusText + '</p>';
            }
        }

        // Show the content
        content.style.display = 'block';
    }

    // Configure the xhr object and fetch the content
    xhr.open('get', '/world/FetchRegions/' + value, true);
    xhr.send();
}
