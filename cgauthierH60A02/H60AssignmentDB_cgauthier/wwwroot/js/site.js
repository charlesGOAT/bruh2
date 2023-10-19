let role = document.querySelector("#role")
let customerForm = document.querySelector("#customer-form")
document.querySelector("#register-form").style.height = 800 + "px"
role.addEventListener("change", () => {
    if (role.value == "Customer" || role.value == "--Select a Role--") {
        document.querySelector("#register-form").style.height = 800+"px"
        customerForm.classList.remove("customer-container")
    } else {
        document.querySelector("#register-form").style.height = 500 + "px"
        customerForm.classList.add("customer-container")
    }

})