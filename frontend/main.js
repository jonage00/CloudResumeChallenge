window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
})

const functionApi = 'https://resumecountercrc-python.azurewebsites.net/api/VisitorCounter?';

const getVisitCount = () => {
    let count = 0; 
    fetch(functionApi).
    then(response => {
        return response.text()
    })
    .then(text =>{
        console.log("Website called function API.");
        count = text;
        document.getElementById("counter").innerText = count;
    }).catch(function(error) {
        console.log(error);
    });
    return count;
}